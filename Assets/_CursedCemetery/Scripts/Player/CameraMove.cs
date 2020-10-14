using CursedCemetery.Scripts.Systens;
using UnityEngine;

namespace CursedCemetery.Scripts.Player
{
    public class CameraMove : MonoBehaviour
    {
        [Header("Settings")] [SerializeField] private float _mouseSensitivity;
        [SerializeField] private Transform _playerBody;
        [SerializeField] private float _xRotation = 0f;
        [SerializeField] private bool _isAlive;

        //Set mouse sensitivity
        private void Awake()
        {
            _isAlive = true;
            Events.GameOver += SetAlive;
            if (PlayerPrefs.GetFloat("MouseSensitivy") <=0 )
            {
                _mouseSensitivity = 100;
            }
            else
            {
                _mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivy");
            }
        }
        private void SetAlive()
        {
            _isAlive = !_isAlive;
        }

        //locks the cursor on start
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            if (!_isAlive)
            {
               return;
            }
            Move();
        }

        //moving the player's camera
        private void Move()
        {
            float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            _playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
