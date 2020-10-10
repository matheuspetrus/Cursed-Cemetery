using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CursedCemetery.Scripts.Systens
{
    public class SystemMenus : MonoBehaviour
    {
        [Header("Settings")] [SerializeField] private int _playTime;
        [SerializeField] private float _mouseSensitivy;

        [Header("Sliders")] [SerializeField] private Slider _sliderTime;
        [SerializeField] private Slider _sliderMouse;

        [Header("Texts")] [SerializeField] private TextMeshProUGUI _textPlayTime;
        [SerializeField] private TextMeshProUGUI _textMouseSensitivy;
      
        
        private void Start()
        {
            InitializaParameters();
        }

        public void SetPlayTime(float sliderValue)
        {
            _playTime = (int) sliderValue;
            _textPlayTime.text = _playTime.ToString("F0") + "min";
            PlayerPrefs.SetFloat("PlayTime", _playTime);
        }

        public void SetMouseSensitivy(float sliderValue)
        {
            _mouseSensitivy = sliderValue;
            _textMouseSensitivy.text = (_mouseSensitivy / 100).ToString("F1");
            PlayerPrefs.SetFloat("MouseSensitivy", _mouseSensitivy);
        }

        public void ButtonExit()
        {
            Application.Quit();
        }

        private void InitializaParameters()
        {
            Cursor.visible =true;
            Cursor.lockState = CursorLockMode.None;
            
            if (PlayerPrefs.GetFloat("PlayTime") <= 0)
            {
                _playTime = 3;
                _textPlayTime.text = _playTime.ToString("F0") + "min";
                _sliderTime.value = _playTime;
                PlayerPrefs.SetFloat("PlayTime", _playTime);
            }
            else
            {
                _playTime = (int) PlayerPrefs.GetFloat("PlayTime");
                _textPlayTime.text = _playTime.ToString("F0") + "min";
                _sliderTime.value = _playTime;
            }

            if (PlayerPrefs.GetFloat("MouseSensitivy") <= 0)
            {
                _mouseSensitivy = 100;
                _textMouseSensitivy.text = (_mouseSensitivy / 100).ToString("F1");
                _sliderMouse.value = _mouseSensitivy;
                PlayerPrefs.SetFloat("MouseSensitivy", _mouseSensitivy);
            }
            else
            {
                _mouseSensitivy = (int) PlayerPrefs.GetFloat("MouseSensitivy");
                _textMouseSensitivy.text = (_mouseSensitivy / 100).ToString("F1");
                _sliderMouse.value = _mouseSensitivy;
            }
        }
    }
}
