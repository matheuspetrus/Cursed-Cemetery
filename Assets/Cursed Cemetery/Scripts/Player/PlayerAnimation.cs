using UnityEngine;

namespace CursedCemetery.Scripts.Player
{
    public class PlayerAnimation : MonoBehaviour {
    
        [SerializeField] private Animator _animator;
        
        private readonly string AnimationRun = "Run";
        private readonly string AnimationDeath = "Death";
        private void Awake()
        {
            InitializeParameters();
        }

        // Set animations
        public void SetAnimationsRun(bool play)
        {
            _animator.SetBool(AnimationRun, play);
        }
        
        public void SetAnimationsDeath(bool play)
        {
            _animator.SetBool(AnimationDeath, play);
        }
    
        private void InitializeParameters()
        {
            _animator =GetComponentInChildren<Animator>();
        }
    }
}
