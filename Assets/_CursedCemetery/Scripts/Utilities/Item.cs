using System;
using System.Collections;
using System.Collections.Generic;
using CursedCemetery.Scripts.Interfaces;
using CursedCemetery.Scripts.Systens;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CursedCemetery.Scripts.Utilities
{
    public class Item : MonoBehaviour
    {
        [Header("Settings")] [SerializeField] private float _valueLife;
        [SerializeField] private float _valueArrow;
        [SerializeField] private float _numberValue;

        [Header("Objects")] [SerializeField] private GameObject _lifeObj;
        [SerializeField] private GameObject _arrowObj;

        [SerializeField] private AudioSource _audioSource;
        private Pooler pool;
        private Transform _target;

        
        private void Start()
        {
            pool = transform.parent.GetComponent<Pooler>();
           
        }

        private void Update()
        {
            LookAt();
        }

        private void OnEnable()
        {
            RandomSelect();
        }
    
        // select an item type at random
        private void RandomSelect()
        {
            _numberValue = Random.Range(0, 100);

            if (_numberValue <= 50)
            {
                _lifeObj.SetActive(true);
                _arrowObj.SetActive(false);
            }
            else
            {
                _lifeObj.SetActive(false);
                _arrowObj.SetActive(true);
            }
        }
    
        // The object looks at the target
        private void LookAt()
        {
            transform.LookAt(_target);
        }
        // set the target
        public void SetTarget(Transform target)
        {
            _target = target;
        }
    
        // check object collision
        private void OnTriggerEnter(Collider other)
        {
            IStatus status = other.GetComponent<IStatus>();

            if (status != null)
            {
                
                if (_numberValue <= 50)
                {
                    other.GetComponent<IStatus>().IncreaseLife(_valueLife);
                    StartCoroutine(ReturnObject());
                }
                else
                {
                    other.GetComponent<IStatus>().IncreaseArrows(_valueArrow);
                    StartCoroutine(ReturnObject());
                }
            }
        }

        IEnumerator ReturnObject()
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
            yield return new WaitForSeconds(0.5f);
            pool.ReturnObject(gameObject);
        }
    }
}
