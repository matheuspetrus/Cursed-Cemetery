using UnityEngine;

namespace CursedCemetery.Scripts.Player
{
    public class PlayerAnimation : MonoBehaviour {
    
        [SerializeField] private Animator _animator;
        
        private readonly string AnimationRun = "Run";
        private void Awake()
        {
            InitializeParameters();
        }

        // Set animations
        public void SetAnimationsRun(bool play)
        {
            _animator.SetBool(AnimationRun, play);
        }
    
        private void InitializeParameters()
        {
            _animator =GetComponentInChildren<Animator>();
        }
    }
}
