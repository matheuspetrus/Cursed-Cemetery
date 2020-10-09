using System.Collections.Generic;
using UnityEngine;

namespace CursedCemetery.Scripts.Systens
{
    public class Pooler : MonoBehaviour
    {

        [Header("Objects")] [SerializeField] private GameObject _prefab;
        [Header("Settings")] [SerializeField] private int _poolSize;
        [SerializeField] private bool _expandable;

        private List<GameObject> _freeList;
        private List<GameObject> _usedList;

        private void Awake()
        {
            _freeList = new List<GameObject>();
            _usedList = new List<GameObject>();

            for (int i = 0; i < _poolSize; i++)
            {
                GenerateNewObject();
            }
        }

        // remove the object from the free list and return the same
        public GameObject GetObject()
        {
            int totalFree = _freeList.Count;
            if (_freeList.Count == 0 && !_expandable)
            {
                return null;
            }
            else if (totalFree == 0)
            {
                GenerateNewObject();
            }

            GameObject obj = _freeList[totalFree - 1];

            _freeList.RemoveAt(totalFree - 1);
            _usedList.Add(obj);
            return obj;
        }
        // remove the object from the used list and return to the free object list
        public void ReturnObject(GameObject obj)
        {
            Debug.Assert(_usedList.Contains(obj));
            obj.SetActive(false);
            _usedList.Remove(obj);
            _freeList.Add(obj);
        }
    
        // Gera um novo objeto
        private void GenerateNewObject()
        {
            GameObject obj = Instantiate(_prefab);
            obj.transform.parent = transform;
            obj.SetActive(false);
            _freeList.Add(obj);
        }
    }
}
