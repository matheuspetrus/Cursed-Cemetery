﻿using System.Collections;
using CursedCemetery.Scripts.Player;
using CursedCemetery.Scripts.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CursedCemetery.Scripts.Systens
{
	public class ControllerGameStatus : MonoBehaviour
	{
		[Header("Set Player")] [SerializeField]
		private GameObject _player;

		[Header("UI Player Objects")] [SerializeField]
		private TextMeshProUGUI _life;

		[SerializeField] private TextMeshProUGUI _arrow;
		[SerializeField] private TextMeshProUGUI _score;
		[SerializeField] private TextMeshProUGUI _time;

		[Header("Timer Settings")] [SerializeField]
		private float _timerStart;

		[SerializeField] private float _timerGame;

		[Header("Menu Objects")] [SerializeField]
		private GameObject _menuPause;

		[SerializeField] private GameObject _sliderForce;

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
			_sliderForce.GetComponent<Slider>().value = _player.GetComponent<SystemShootProjectile>().GetForce() / 100;
			if (_sliderForce.GetComponent<Slider>().value > 0)
			{
				_sliderForce.SetActive(true);
			}
			else
			{
				_sliderForce.SetActive(false);
			}

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
					Events.GameOverTime();
					Events.GameOver();
				}
			}

			string seconds = ((_timerSeconds) % 60).ToString("F0");
			string minutes = _timerGame.ToString("F0");

			if (_timerGame <= 0 && ((_timerSeconds) % 60) <= 30)
			{
				_time.fontSize = 100;
				_time.color = Color.red;
				_time.text = minutes + ":" + seconds;
			}
			else
			{
				_time.text = minutes + ":" + seconds;
			}

		}

		// Initializes the parameters
		private void InitializeParameters()
		{
			_time.text = _timerStart.ToString("F0");
			_timerActive = true;
			Events.GameOver += GameOver;
			Events.GameOverDied += SetGameOverDied;
			Events.GameOverTime += SetGameOverTimeOut;
			Events.DeathEnemyArcher += DeathEnemyArcher;
			Events.DeathEnemyWarrior += DeathEnemyWarrior;
			Cursor.visible = false;

			if (PlayerPrefs.GetFloat("PlayTime") <= 0)
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

		// Set game over parameters
		private void GameOver()
		{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			SetValuesPanelScore();
			SceneManager.LoadScene(2);
		}

		private void SetGameOverTimeOut()
		{
			PlayerPrefs.SetInt("GameOverType", 1);
		}

		private void SetGameOverDied()
		{
			PlayerPrefs.SetInt("GameOverType", 0);
		}

		// Set State Pause
		private void SetPause()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				if (_isPause)
				{
					ButtonContinue();
				}
				else
				{
					Pause();
				}
			}
		}

		// set pause status parameters
		private void Pause()
		{
			_menuPause.SetActive(true);
			Cursor.lockState = CursorLockMode.None;
			Time.timeScale = 0;
			_isPause = true;
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
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
			_menuPause.SetActive(false);
			_isPause = false;
			Time.timeScale = 1;
		}

		///////////////////////////////////////
		// Set score panel values
		private void SetValuesPanelScore()
		{
			PlayerPrefs.SetFloat("WarriorsKilled", _warriorsKilled);
			PlayerPrefs.SetFloat("ArchersKilled", _archersKilled);
		}

		private void Score()
		{
			_score.text = ((_warriorsKilled * 10) + (_archersKilled * 20)).ToString();
		}
	}
}
