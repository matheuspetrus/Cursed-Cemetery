using UnityEngine;
using UnityEngine.Audio;

namespace CursedCemetery.Scripts.Systens
{
	public class Sounds : MonoBehaviour
	{

		[Header("AudioMixers")] [SerializeField] private AudioMixer _audioAmbience;
		[SerializeField] private AudioMixer _audioMusic;
		[SerializeField] private AudioMixer _audioEffects;

		public void SetVolumeAmbience(float sliderValue)
		{
			_audioAmbience.SetFloat("Ambience", Mathf.Log10(sliderValue) * 20);
			Debug.Log("Test Mixer Ambience" + (Mathf.Log10(sliderValue) * 20).ToString());
		}

		public void SetVolumeMusics(float sliderValue)
		{
			_audioMusic.SetFloat("Musics", Mathf.Log10(sliderValue) * 20);
			Debug.Log("Test Mixer Musics" + (Mathf.Log10(sliderValue) * 20).ToString());
		}

		public void SetVolumeEffects(float sliderValue)
		{
			_audioEffects.SetFloat("Effects", Mathf.Log10(sliderValue) * 20);
			Debug.Log("Test Mixer Effects" + (Mathf.Log10(sliderValue) * 20).ToString());
		}
	}
}
