using CursedCemetery.Scripts.Systens;
using UnityEngine;

namespace CursedCemetery.Scripts.Player
{
    public class PlayerMove : MonoBehaviour
    {
        [Header("Settings")] [SerializeField] private float _moveSpeed;
        [SerializeField] private float _gravity;
        [SerializeField] private float _jumpHeight;
        [SerializeField] public float groundDistance;
        [SerializeField] public bool _isGrounded;

        [Header("Layers Settings")] [SerializeField]
        private Transform _groundCheck;
        [SerializeField] private LayerMask _groundMask;

        private PlayerAnimation _animator;
        private float _moveX;
        private float _moveZ;

        private AudioSource _audioSource;
        private CharacterController _controller;
        private Vector3 _velocity;
        [SerializeField]  private bool _isAlive;
        
        
        private void Awake()
        {
            InitializeParameters();
        }
        private void SetAlive()
        {
            _isAlive = !_isAlive;
        }

        private void Update()
        {
            Move();
            Animations();
        }
        // Move Player
        private void Move()
        {
            if (!_isAlive)
            {
                _animator.SetAnimationsRun(false);
                return;
            }
            _isGrounded = Physics.CheckSphere(_groundCheck.position, groundDistance, _groundMask);

            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2;
            }

            _moveX = Input.GetAxis("Horizontal");
            _moveZ = Input.GetAxis("Vertical");

            Vector3 move = transform.right * _moveX + transform.forward * _moveZ;

            _controller.Move(move * _moveSpeed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
                _velocity.y = Mathf.Sqrt(_jumpHeight * -2 * _gravity);
            }
        
            _velocity.y += _gravity * Time.deltaTime;
            _controller.Move(_velocity * Time.deltaTime);
        }
        // parameter initialization
        private void InitializeParameters()
        {
            _isAlive = true;
            Events.GameOver += SetAlive;
            _animator = GetComponent<PlayerAnimation>();
            _controller = GetComponent<CharacterController>();
            _audioSource = GetComponent<AudioSource>();
        }
        // Set player's camera animations
        private void Animations()
        {
            if (_moveX != 0 || _moveZ != 0 && _isGrounded)
            {
                _animator.SetAnimationsRun(true);
                if (!_audioSource.isPlaying)
                {
                    _audioSource.Play();
                }
            }
            else
            {
                _animator.SetAnimationsRun(false);
                _audioSource.Pause();
            }

        }
    }
}

