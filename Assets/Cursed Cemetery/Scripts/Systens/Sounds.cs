using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace CursedCemetery.Scripts.Systens
{

	public class Sounds : MonoBehaviour
	{
		[Header("AudioMixers")] [SerializeField]
		private AudioMixer _audioAmbience;

		[SerializeField] private AudioMixer _audioMusic;
		[SerializeField] private AudioMixer _audioEffects;

		[Header("Sliders")] [SerializeField] private Slider _sliderAmbience;
		[SerializeField] private Slider _sliderMusics;
		[SerializeField] private Slider _sliderEffects;

		private void Start()
		{
			InitializaParameters();
		}

		public void SetVolumeAmbience(float sliderValue)
		{
			_audioAmbience.SetFloat("Ambience", Mathf.Log10(sliderValue) * 20);
			PlayerPrefs.SetFloat("AudioAmbience", sliderValue);
		}

		public void SetVolumeMusics(float sliderValue)
		{
			_audioMusic.SetFloat("Musics", Mathf.Log10(sliderValue) * 20);
			PlayerPrefs.SetFloat("AudioMusics", sliderValue);
		}

		public void SetVolumeEffects(float sliderValue)
		{
			_audioEffects.SetFloat("Effects", Mathf.Log10(sliderValue) * 20);
			PlayerPrefs.SetFloat("AudioEffects", sliderValue);
		}

		private void InitializaParameters()
		{
			if (PlayerPrefs.GetFloat("AudioAmbience") <= 0)
			{
				_sliderAmbience.value = 0.5f;
				_audioAmbience.SetFloat("Ambience", Mathf.Log10(_sliderAmbience.value) * 20);
				PlayerPrefs.SetFloat("AudioAmbience", _sliderAmbience.value);
			}
			else
			{
				_sliderAmbience.value = PlayerPrefs.GetFloat("AudioAmbience");
				_audioAmbience.SetFloat("Ambience", Mathf.Log10(_sliderAmbience.value) * 20);
			}

			if (PlayerPrefs.GetFloat("AudioMusics") <= 0)
			{
				_sliderMusics.value = 0.5f;
				_audioMusic.SetFloat("Musics", Mathf.Log10(_sliderMusics.value) * 20);
				PlayerPrefs.SetFloat("AudioMusics", _sliderMusics.value);
			}
			else
			{
				_sliderMusics.value = PlayerPrefs.GetFloat("AudioMusics");
				_audioMusic.SetFloat("Musics", Mathf.Log10(_sliderMusics.value) * 20);
			}

			if (PlayerPrefs.GetFloat("AudioEffects") <= 0)
			{
				_sliderEffects.value = 0.5f;
				_audioEffects.SetFloat("Effects", Mathf.Log10(_sliderEffects.value) * 20);
				PlayerPrefs.SetFloat("AudioEffects", _sliderEffects.value);
			}
			else
			{
				_sliderEffects.value = PlayerPrefs.GetFloat("AudioEffects");
				_audioEffects.SetFloat("Effects", Mathf.Log10(_sliderEffects.value) * 20);
			}
		}
	}
}
