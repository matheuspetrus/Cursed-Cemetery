using CursedCemetery.Scripts.Player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CursedCemetery.Scripts.Systens
{
	public class ControllerGameStatus : MonoBehaviour
	{
		[Header("Set Player")]
		[SerializeField] private GameObject _player;
	
		[Header("UI Player Objects")]
		[SerializeField] private TextMeshProUGUI _life;
		[SerializeField] private TextMeshProUGUI _arrow;
		[SerializeField] private TextMeshProUGUI _score;
		[SerializeField] private TextMeshProUGUI _time;
	
		[Header("UI Game Over Objects")]
		[SerializeField] private TextMeshProUGUI _numberWarriorsKilled;
		[SerializeField] private TextMeshProUGUI _numberArchersKilled;
		[SerializeField] private TextMeshProUGUI _totalScore;
		[SerializeField] private TextMeshProUGUI _bestScore;
	
		[Header("Timer Settings")]
		[SerializeField] private float _timerStart;
		[SerializeField] private float _timerGame;

		[Header("Menu Objects")]
		[SerializeField] private GameObject _menuPause;
		[SerializeField] private GameObject _menuScore;

		private float _warriorsKilled;
		private float _archersKilled;
		private float _timerSeconds = 59;
		private bool _timerActive;
		private bool _isPause;

		private void Start()
		{
			InitializeParameters();
		}

		private void Update()
		{
			Score();
			StatusPlayer();
			TimerStart();
			SetPause();
		}
		// Set player variables
		private void StatusPlayer()
		{
			_life.text = _player.GetComponent<PlayerStatus>().GetLife().ToString();
			_arrow.text = _player.GetComponent<PlayerStatus>().GetArrows().ToString();
		}
		// Time to start the game
		private void TimerStart()
		{
			if (_timerStart >= 0)
			{
				_timerStart -= Time.deltaTime;
				_time.text = _timerStart.ToString("F0");
			}
			else
			{
				TimerGame();
			}
		}
		// playing time
		private void TimerGame()
		{
			if (_timerActive)
			{
				_timerSeconds -= Time.deltaTime;
				if (_timerSeconds % 60 < 0)
				{
					_timerSeconds = 59;
					_timerGame--;
				}

				if (_timerGame < 0)
				{
					_timerGame = 0;
					_timerSeconds = 0;
					_timerActive = false;
					Events.GameOver();
				}
			}

			string seconds = ((_timerSeconds) % 60).ToString("F0");
			string minutes = _timerGame.ToString("F0");

			_time.text = minutes + ":" + seconds;
		}
		// Set game over parameters
		private void GameOver()
		{
			Cursor.lockState = CursorLockMode.None;
			Time.timeScale = 0;
			_menuScore.SetActive(true);
			SetValuesPanelScore();
		}
		// Initializes the parameters
		private void InitializeParameters()
		{
			_time.text = _timerStart.ToString("F0");
			_timerActive = true;
			Events.GameOver += GameOver;
			Events.DeathEnemyArcher += DeathEnemyArcher;
			Events.DeathEnemyWarrior += DeathEnemyWarrior;

			if (PlayerPrefs.GetFloat("PlayTime") == null)
			{
				_timerGame = 2;
			}
			else
			{
				_timerGame = PlayerPrefs.GetFloat("PlayTime");
				_timerGame--;
			}
		}
		// Account of the death of warriors
		private void DeathEnemyWarrior()
		{
			_warriorsKilled++;
		} 
		// Account the death of Archers
		private void DeathEnemyArcher()
		{
			_archersKilled++;
		}
		// Set State Pause
		private void SetPause()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				_menuPause.SetActive(true);
				Pause();
			}
		}
		// set pause status parameters
		private void Pause()
		{
			Events.Pause();
			Cursor.lockState = CursorLockMode.None;
			Time.timeScale = 0;
		}
		///////////// menu buttons ///////////////
		public void ButtonExit()
		{
			Application.Quit();
		}

		public void ButtonMainMenu()
		{
			Time.timeScale = 1;

			SceneManager.LoadScene(0);
		}

		public void ButtonContinue()
		{
			Events.Pause();
			Cursor.lockState = CursorLockMode.Locked;
			_menuPause.SetActive(false);
			Time.timeScale = 1;
		}

		public void ButtonRestart()
		{
			Cursor.lockState = CursorLockMode.Locked;
			SceneManager.LoadScene(1);
			Time.timeScale = 1;
		}
		///////////////////////////////////////
		// Set score panel values
		private void SetValuesPanelScore()
		{
			_numberWarriorsKilled.text = _warriorsKilled.ToString() + " = " + (_warriorsKilled * 10).ToString();
			_numberArchersKilled.text = _archersKilled.ToString() + " = " + (_archersKilled * 20).ToString();
			_totalScore.text = "Total = " + ((_warriorsKilled * 10) + (_archersKilled * 20)).ToString();
			if (PlayerPrefs.GetFloat("BestScore") == null)
			{
				_bestScore.text = _totalScore.text;
			}
			else if (((_warriorsKilled * 10) + (_archersKilled * 20)) > PlayerPrefs.GetFloat("BestScore"))
			{
				float bestScore = (_warriorsKilled * 10) + (_archersKilled * 20);
				_bestScore.text = _totalScore.text;

				PlayerPrefs.SetFloat("BestScore", bestScore);
			}
			else
			{
				_bestScore.text = PlayerPrefs.GetFloat("BestScore").ToString();
			}

		}

		private void Score()
		{
			_score.text = ((_warriorsKilled * 10) + (_archersKilled * 20)).ToString();
		}
	}
}
