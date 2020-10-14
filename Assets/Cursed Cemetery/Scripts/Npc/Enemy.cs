using UnityEngine;

namespace CursedCemetery.Scripts.Npc
{
    [CreateAssetMenu(fileName = "New Enemy", menuName ="Enemy")]
    public class Enemy : ScriptableObject {
   
        [Header("Settings")]
        public float DistanceAttack;
        public float Speed;
        public float Damage;
        public float SpeedDamage;
        public float LiveMax;
        [Range(0,100)]
        public float ProbabilityDropItem;
    
        public bool LookAtTarget;
        public bool ShootProjectile;

        public Texture []TextureArmor;
    }
}
