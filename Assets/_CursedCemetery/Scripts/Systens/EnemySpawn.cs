using System.Collections;
using CursedCemetery.Scripts.Npc;
using CursedCemetery.Scripts.Utilities;
using UnityEngine;

namespace CursedCemetery.Scripts.Systens
{
    public class EnemySpawn : MonoBehaviour
    {

        [Header("Settings")] [SerializeField] private Transform[] _spawns;
        [SerializeField] private Transform _target;
        [SerializeField] private Pooler _enemy;
        [SerializeField] private Pooler _projectile;
        [SerializeField] private Pooler _item;
        [SerializeField] private float _tempSpawn;
        [SerializeField] private bool _haveProjectile;

        private void Start()
        {
            StartCoroutine(CanSpawn());
        }
    
        // Spawn of enemies
        private void SpawnEnemy()
        {
            int i = Random.Range(0, _spawns.Length);
            GameObject obj = _enemy.GetObject();
            obj.transform.position = _spawns[i].transform.position;
            obj.transform.rotation = _spawns[i].transform.rotation;
            obj.GetComponent<EnemyAI>().SetTarget(_target);
            obj.GetComponent<EnemyAI>().ResetParameters();
            obj.GetComponent<EnemyAI>().SetItem(_item);

            if (_haveProjectile)
            {
                obj.GetComponent<SystemShootProjectile>().SetProjectile(_projectile);
            }

            obj.SetActive(true);

            StartCoroutine(CanSpawn());
        }

        // Defines the time between spawns
        IEnumerator CanSpawn()
        {
            yield return new WaitForSeconds(_tempSpawn);
            SpawnEnemy();
        }
    }
}
