using System.Collections;
using CursedCemetery.Scripts.Interfaces;
using CursedCemetery.Scripts.Systens;
using CursedCemetery.Scripts.Utilities;
using UnityEngine;
using UnityEngine.AI;

namespace CursedCemetery.Scripts.Npc
{
    public class EnemyAI : MonoBehaviour, IDamage
    {
        [Header("Settings")]
        [SerializeField] private float _distanceTarget;
        [SerializeField] private float _distanceAttack;
        [SerializeField] private float _damage;
        [SerializeField] private float _speedDamage;
        [SerializeField] private float _life;
        [SerializeField] private float _probabilityDropItem;
        [SerializeField] private float _tempDissolveBody;
        [SerializeField] private bool _isAlive;
        [SerializeField] private bool _lookAtTarget;
        [SerializeField] private bool _shootProjectile;
        [SerializeField] private bool _isAttack;
    
        [Header("Color Settings")]
        [SerializeField] private int _numberColor;
        [SerializeField] private bool _colorRandon;
    
        [Header("Objects")]
        [SerializeField] private Enemy _enemy;
        [SerializeField] private GameObject _armor;
        
        private bool _isPause;
        private NavMeshAgent _agent;
        private Transform _target;
        private Pooler _pool;
        private Pooler _poolItens;
        private Animator _animator;
        private SystemShootProjectile _projectile;
        
        private readonly string AnimationDeath = "Death";
        private readonly string AnimationAttack= "Attack";

        // Use this for initialization
        private void Awake()
        {
            InitializeParameters();
        }
        //initialization of AI parameters
        private void InitializeParameters()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _pool = transform.parent.GetComponent<Pooler>();
            _isAlive = true;
            _isAttack = true;
            _speedDamage = _enemy.SpeedDamage;
            _agent.speed = _enemy.Speed;
            _life = _enemy.LiveMax;
            _distanceAttack = _enemy.DistanceAttack;
            _lookAtTarget = _enemy.LookAtTarget;
            _shootProjectile = _enemy.ShootProjectile;
            _probabilityDropItem = _enemy.ProbabilityDropItem;
            Events.Pause+= SetPause;
        
            if (_shootProjectile)
            {
                _projectile = GetComponent<SystemShootProjectile>();
            }
            else
            {
                _damage = _enemy.Damage;
            }

            if (_colorRandon)
            {
                _armor.GetComponent<SkinnedMeshRenderer>().material.SetTexture("_MainTex",
                    _enemy.TextureArmor[Random.Range(0, _enemy.TextureArmor.Length)]);
            }
            else
            {
                _armor.GetComponent<SkinnedMeshRenderer>().material
                    .SetTexture("_MainTex", _enemy.TextureArmor[_numberColor]);
            }
        }
        //Resetting the parameters
        public void ResetParameters()
        {
            _life = _enemy.LiveMax;
            _isAlive = true;
            _isAttack = true;
            if (_shootProjectile)
            {
                _projectile.SetCanShoot();
            }
        }
        // Update is called once per frame
        void Update()
        {
            if (_isPause)
            {
                return;
            }
            Move();
            CheckDistance();
            LookAt();
        }
        // AI movement
        private void Move()
        {
            if (_isAlive)
            {
                _agent.SetDestination(_target.position);

                if (_distanceTarget < _distanceAttack)
                {
                    _agent.speed = 0;
                    _animator.SetBool(AnimationAttack, true);
                    if (_shootProjectile)
                    {
                        _projectile.Shoot();
                    }
                }
                else
                {
                    _agent.speed = _enemy.Speed;
                    _animator.SetBool(AnimationAttack, false);
                }
            }
        }
        // checks the distance between AI and Target
        private void CheckDistance()
        {
            _distanceTarget = Vector3.Distance(transform.position, _target.transform.position);
        }
        // Set target
        public void SetTarget(Transform setTarget)
        {
            _target = setTarget;
        }
        // Returns the Target
        public Transform GetTarget()
        {
            return _target;
        }
        // damage in AI
        public void ApplyDamage(float damage)
        {
            _life -= damage;
            if (_life <= 0)
            {
                _agent.speed = 0;
                _isAlive = false;

                if (_shootProjectile)
                {
                    Events.DeathEnemyArcher();
                }
                else
                {
                    Events.DeathEnemyWarrior();
                }
                _animator.SetBool(AnimationDeath , true);
                StartCoroutine(DissolveBody());
            }
        }
        // time to dissolve the AI ​​body
        IEnumerator DissolveBody()
        {
            yield return new WaitForSeconds(_tempDissolveBody);
            if (Random.Range(0,100)< _probabilityDropItem)
            {
                SpawnItens();
            }
            _pool.ReturnObject(gameObject);
        }
        // Look at Target
        private void LookAt()
        {
            if (_lookAtTarget)
            {
                transform.LookAt(_target);
            }
        }
        // Checks the collision with the target
        private void OnTriggerStay(Collider other)
        {
            IStatus status = other.GetComponent<IStatus>();

            if (status != null)
            {
                if (_isAttack)
                {
                    status.DecreaseLife(_damage);
                    _isAttack = false;
                    StartCoroutine(CanDamage());
                }
            }
        }
        // Define the time between attacks
        IEnumerator CanDamage()
        {
            yield return new WaitForSeconds(_speedDamage);
            _isAttack = true;
        }
        // Spawn of items
        private void SpawnItens()
        {
            GameObject obj = _poolItens.GetObject();
            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
            obj.GetComponent<Item>().SetTarget(_target);
            obj.SetActive(true);
        }
        // Set pooler Item
        public void SetItem(Pooler item)
        {
            _poolItens = item;
        }
        // Set State Pause
        private void SetPause()
        {
            _isPause = !_isPause;
        }
    }
}
