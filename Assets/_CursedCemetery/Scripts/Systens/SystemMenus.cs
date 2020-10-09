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

        public void Awake()
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
            if (PlayerPrefs.GetFloat("PlayTime") == null)
            {
                _playTime = 3;
                _textPlayTime.text = _playTime.ToString("F0") + "min";
                _sliderTime.value = _playTime;
            }
            else
            {
                _playTime = (int) PlayerPrefs.GetFloat("PlayTime");
                _textPlayTime.text = _playTime.ToString("F0") + "min";
                _sliderTime.value = _playTime;
            }

            if (PlayerPrefs.GetFloat("MouseSensitivy") == null)
            {
                _mouseSensitivy = 100;
                _textMouseSensitivy.text = (_mouseSensitivy / 100).ToString("F1");
                _sliderMouse.value = _mouseSensitivy;
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
