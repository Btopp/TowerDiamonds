﻿//by Niklas Bachmann
//19.08.2017

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	[HideInInspector]
	public bool pauseButtonPressed = false;
	[HideInInspector]
	public bool speedUpButtonPressed = false;

	private UIManager uIManager;

	void Start () {
		uIManager = (UIManager) GameObject.Find("UIManager").GetComponent<UIManager> ();
	}

	public void BackToMainMenu () {
		SceneManager.LoadScene (0);
	}

	public void PauseGame () {
		Time.timeScale = 0f;
	}

	public void StartGame () {
		Time.timeScale = 1f;
	}

	public void PressPauseButton () {
		uIManager.DisableSpeedUpAnimation ();
		speedUpButtonPressed = false;
		if (pauseButtonPressed) {
			StartGame ();
			pauseButtonPressed = false;
		} else {
			PauseGame ();
			pauseButtonPressed = true;
		}	
	}

	public void SpeeUp () {	
		Time.timeScale = 1.5f;	
	}

	public void PressSpeedUpButton () {
		if (Time.timeScale == 0f) {
			return;
		}
		if (speedUpButtonPressed) {
			uIManager.DisableSpeedUpAnimation ();
			StartGame ();
			speedUpButtonPressed = false;
		} else {
			uIManager.EnableSpeedUpAnimation ();
			SpeeUp ();
			speedUpButtonPressed = true;
		}	
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			BackToMainMenu ();
		}			
	}
}