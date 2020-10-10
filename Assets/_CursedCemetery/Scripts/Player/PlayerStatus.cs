using System.Collections;
using CursedCemetery.Scripts.Interfaces;
using CursedCemetery.Scripts.Systens;
using UnityEngine;

namespace CursedCemetery.Scripts.Player
{
    public class PlayerStatus : MonoBehaviour, IStatus
    {

        [Header("Settings")] [SerializeField] private float _life;
        [SerializeField] private float _lifeMax;
        [SerializeField] private float _arrows;

        [SerializeField] private GameObject _boodHit;
        private void Awake()
        {
            _life = _lifeMax;
        }
        //Remove arrows from the player's inventory
        public void DecreaseArrows(float arrow)
        {
            if (_arrows > 0)
            {
                _arrows -= arrow;
            }
            else
            {
                _arrows = 0;
            }
        }
        //decreases the player's life
        public void DecreaseLife(float life)
        {
            StartCoroutine(TimeBloodHit());
            if (_life > 0)
            {
                _life -= life;
                if (_life <= 0)
                {
                    _life = 0;
                    Events.GameOver();
                }
            }
        }

         IEnumerator TimeBloodHit()
        {
            _boodHit.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _boodHit.SetActive(false);
        }

        //Returns the number of arrows in the inventory
        public float GetArrows()
        {
            return _arrows;
        }
        //Returns the player's life
        public float GetLife()
        {
            return _life;
        }
        //adds arrows to the player inventory
        public void IncreaseArrows(float arrow)
        {
            _arrows += arrow;
        }
        //adds life to the player
        public void IncreaseLife(float life)
        {
            if ((_life + life) > _lifeMax)
            {
                _life = _lifeMax;
            }
            else
            {
                _life += life;
            }
        }
    
    }
}
