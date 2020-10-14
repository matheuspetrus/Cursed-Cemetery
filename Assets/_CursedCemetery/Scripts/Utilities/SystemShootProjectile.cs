using System.Collections;
using CursedCemetery.Scripts.Player;
using CursedCemetery.Scripts.Systens;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CursedCemetery.Scripts.Utilities
{
    public class SystemShootProjectile : MonoBehaviour
    {
        [Header("Settings")] [SerializeField] private float _force;
        [SerializeField] private float _forceCurrent;
        [SerializeField] private float _forceVariable;
        [SerializeField] private float _timeToShoot;
        [SerializeField] private float _amountProjectile;
        [SerializeField] private bool _isPlayer;
        [SerializeField] private bool canShoot = true;

        [Header("Objects")] [SerializeField] private Pooler _projectile;
        [SerializeField] private GameObject spawnArrow;

        private PlayerStatus _player;
        [SerializeField] private bool _isAlive;

        private void Awake()
        {
            _isAlive = true;
            Events.GameOver += SetAlive;
            if (_isPlayer)
            {
                _player = GetComponent<PlayerStatus>();
            }
        }

        // Set State Pause
        private void SetAlive()
        {
            _isAlive = !_isAlive;
        }
    
        // checks and launches the project
        public void Shoot()
        {
            if (!canShoot)
            {
                return;
            }

            GameObject obj = _projectile.GetObject();
            if (!_isPlayer)
            {
                _forceCurrent = Random.Range(_force - _forceVariable, _force + _forceVariable);
            }
            else
            {
                _forceCurrent = _force;
                _player.DecreaseArrows(1);
            }

            obj.GetComponent<Projectile>().SetForce(_forceCurrent);
            obj.transform.position = spawnArrow.transform.position;
            obj.transform.rotation = spawnArrow.transform.rotation;

            spawnArrow.SetActive(false);

            obj.SetActive(true);

            StartCoroutine(CanShoot());
        }
    
        // time for a new shot
        IEnumerator CanShoot()
        {
            canShoot = false;
            yield return new WaitForSeconds(_timeToShoot);
            canShoot = true;
            SetStateSpawn();
        }

        private void Update()
        {
            if (!_isAlive)
            {
                return;
            }
            if (Input.GetMouseButton(0) && _isPlayer && _player.GetArrows() > 0)
            {
                if (_force <= 100)
                {
                    _force += Time.deltaTime *60;
                }
            }

            if (Input.GetMouseButtonUp(0) && _isPlayer && _player.GetArrows() > 0)
            {
                Shoot();
                _force = 0;
            }
        }
    
        // set the pooler object
        public void SetProjectile(Pooler projectile)
        {
            _projectile = projectile;
        }

        public void SetCanShoot()
        {
            canShoot = true;
        }

        private void SetStateSpawn()
        {
            if (_isPlayer && _player.GetArrows() <= 0)
            {
                spawnArrow.SetActive(false);
            }
            else
            {
                spawnArrow.SetActive(true);
            }
        }

        public float GetForce()
        {
            return _force;
        }
    }
}
