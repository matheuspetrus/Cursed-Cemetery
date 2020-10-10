
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScoreGameOver : MonoBehaviour {

	
	[Header("UI Game Over Objects")]
	[SerializeField] private TextMeshProUGUI _numberWarriorsKilled;
	[SerializeField] private TextMeshProUGUI _numberArchersKilled;
	[SerializeField] private TextMeshProUGUI _totalScore;
	[SerializeField] private TextMeshProUGUI _bestScore;
	
	private float _warriorsKilled;
	private float _archersKilled;

	private void Start()
	{
		SetValuesPanelScore();
	}

	private void SetValuesPanelScore()
	{
		_warriorsKilled  = PlayerPrefs.GetFloat("WarriorsKilled");
		_archersKilled = PlayerPrefs.GetFloat("ArchersKilled");

		_numberWarriorsKilled.text = _warriorsKilled.ToString() + " = " + (_warriorsKilled * 10).ToString();
		_numberArchersKilled.text = _archersKilled.ToString() + " = " + (_archersKilled * 20).ToString();
		_totalScore.text =  ((_warriorsKilled * 10) + (_archersKilled * 20)).ToString();
		if (PlayerPrefs.GetFloat("BestScore") <=0)
		{
			float bestScore = (_warriorsKilled * 10) + (_archersKilled * 20);
			_bestScore.text = _totalScore.text + " New Record!!!";
			PlayerPrefs.SetFloat("BestScore", bestScore);
		}
		else if (((_warriorsKilled * 10) + (_archersKilled * 20)) > PlayerPrefs.GetFloat("BestScore"))
		{
			float bestScore = (_warriorsKilled * 10) + (_archersKilled * 20);
			_bestScore.text = _totalScore.text+ " New Record!!!!";

			PlayerPrefs.SetFloat("BestScore", bestScore);
		}
		else
		{
			_bestScore.text = PlayerPrefs.GetFloat("BestScore").ToString();
		}	
		
	}
	public void ButtonMainMenu()
	{
		SceneManager.LoadScene(0);
	}
	
	public void ButtonRestart()
	{
		SceneManager.LoadScene(1);
	}
	
}
