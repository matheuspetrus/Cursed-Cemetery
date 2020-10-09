using System.Collections;
using CursedCemetery.Scripts.Interfaces;
using CursedCemetery.Scripts.Systens;
using UnityEngine;

namespace CursedCemetery.Scripts.Utilities
{
    public class Projectile : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _force;
        [SerializeField] private float _damage;
        [SerializeField] private float _timeDestroy;
        [SerializeField] private Pooler _pool;
        [SerializeField] private GameObject _hitEffect;
        [SerializeField] private BoxCollider _boxCollider;
    
        // Start of components
        private void Start()
        {
            _boxCollider = GetComponent<BoxCollider>();
            _pool = transform.parent.GetComponent<Pooler>();
        }
        // set the launch force
        public void SetForce(float force)
        {
            _force = force;
        }
    
        // definition of object parameters when enabled
        private void OnEnable()
        {
            _boxCollider.enabled = true;
            GetComponent<Rigidbody>().isKinematic = false;
            StartCoroutine(DestroyBulletAfterTime());
        }
        // destroy the projectile after a while
        IEnumerator DestroyBulletAfterTime()
        {
            yield return new WaitForSeconds(_timeDestroy);
            GetComponent<Rigidbody>().isKinematic = true;
            _force = 0;
            _pool.ReturnObject(gameObject);
        }

        private void Update()
        {
            transform.Translate(-Vector3.up * _force * Time.deltaTime);
        }
        // Projectile crash test
        private void OnTriggerEnter(Collider other)
        {
            _force = 0;
            GetComponent<Rigidbody>().isKinematic = true;

            IDamage damage = other.GetComponent<IDamage>();
            IStatus status = other.GetComponent<IStatus>();

            if (status != null)
            {
                status.DecreaseLife(_damage);
            }

            if (damage != null)
            {
                _hitEffect.SetActive(true);
                damage.ApplyDamage(_damage);
            }
            _boxCollider.enabled = false;
            DissolveBody();

        }
    
        // time to dissolve body
        IEnumerator DissolveBody()
        {
            yield return new WaitForSeconds(1);
            _hitEffect.SetActive(false);
            _pool.ReturnObject(gameObject);
        }
    }
}
