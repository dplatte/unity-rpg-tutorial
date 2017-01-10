using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class CharacterGenerator : MonoBehaviour {

	private PlayerCharacter _toon;
	private const int STARTING_POINTS = 350;
	private const int MIN_STARTING_ATTRIBUTE_VALUE = 10;
	private const int STARTING_ATTRIBUTE_VALUE = 50;
	private const int OFFSET = 5;
	private const int LINE_HEIGHT = 20;
	private const int STAT_LABEL_WIDTH = 100;
	private const int STAT_VALUE_WIDTH = 30;
	private const int BUTTON_WIDTH = 25;
	private const int STAT_STARTING_POS = 40;

	private int pointsLeft;

	public GUISkin mySkin;

	public GameObject playerPrefab;

	// Use this for initialization
	void Start () {
		GameObject pc = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity) as GameObject;

		pc.name = "PC";

		_toon = pc.GetComponent<PlayerCharacter>();
		_toon.Awake();

		pointsLeft = STARTING_POINTS;

		for(int i = 0; i < Enum.GetValues(typeof(AttributeName)).Length; i++) {
			_toon.GetPrimaryAttribute(i).BaseValue = STARTING_ATTRIBUTE_VALUE;
			pointsLeft -= (STARTING_ATTRIBUTE_VALUE - MIN_STARTING_ATTRIBUTE_VALUE);
		}

		_toon.StatUpdate();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI() {
		GUI.skin = mySkin;
		DisplayName();
		DisplayPointsLeft();
		DisplayAttributes();
		DisplayVitals();
		DisplaySkills();
		DisplayCreateButton();
	}

	private void DisplayName() {
		GUI.Label(new Rect(
			10, 
			10, 
			50, 
			25), 
			"Name:");
		_toon.Name = GUI.TextField(new Rect(65, 10, 100, 25), _toon.Name);
	}

	private void DisplayAttributes() {
		for(int i = 0; i < Enum.GetValues(typeof(AttributeName)).Length; i++) {
			GUI.Label(new Rect(
					OFFSET, 
					STAT_STARTING_POS + (i * LINE_HEIGHT), 
					STAT_LABEL_WIDTH, 
					LINE_HEIGHT
				), ((AttributeName) i).ToString());
			GUI.Label(new Rect(
					STAT_LABEL_WIDTH + OFFSET, 
					STAT_STARTING_POS + (i * LINE_HEIGHT), 
					STAT_VALUE_WIDTH, LINE_HEIGHT
				), _toon.GetPrimaryAttribute(i).AdjustedBaseValue.ToString());
			if(GUI.Button(new Rect(
					OFFSET + STAT_LABEL_WIDTH + OFFSET + STAT_VALUE_WIDTH + OFFSET, 
					STAT_STARTING_POS + (i * LINE_HEIGHT), 
					BUTTON_WIDTH, 
					LINE_HEIGHT
				), "-")) {
				if(_toon.GetPrimaryAttribute(i).BaseValue > MIN_STARTING_ATTRIBUTE_VALUE) {
					_toon.GetPrimaryAttribute(i).BaseValue--;
					pointsLeft++;
					_toon.StatUpdate();
				}
			}
			if(GUI.Button(new Rect(
					OFFSET + STAT_LABEL_WIDTH + OFFSET + STAT_VALUE_WIDTH + OFFSET + BUTTON_WIDTH + OFFSET, 
					STAT_STARTING_POS + (i * LINE_HEIGHT), 
					BUTTON_WIDTH, 
					LINE_HEIGHT
				), "+")) {
				if(pointsLeft > 0) {
					_toon.GetPrimaryAttribute(i).BaseValue++;
					pointsLeft--;
					_toon.StatUpdate();
				}
			}
		}
	}

	private void DisplayVitals() {
		for(int i = 0; i < Enum.GetValues(typeof(VitalName)).Length; i++) {
			GUI.Label(new Rect(
					OFFSET, 
					STAT_STARTING_POS + ((i + 7) * LINE_HEIGHT), 
					STAT_LABEL_WIDTH, 
					LINE_HEIGHT
				), ((VitalName) i).ToString());
			GUI.Label(new Rect(
					STAT_LABEL_WIDTH + OFFSET, 
					STAT_STARTING_POS + ((i + 7) * LINE_HEIGHT), 
					STAT_VALUE_WIDTH, 
					LINE_HEIGHT
				), _toon.GetVital(i).AdjustedBaseValue.ToString());
		}
	}

	private void DisplaySkills() {
		for(int i = 0; i < Enum.GetValues(typeof(SkillName)).Length; i++) {
			GUI.Label(new Rect(
					OFFSET + STAT_LABEL_WIDTH + OFFSET + STAT_VALUE_WIDTH + OFFSET + BUTTON_WIDTH + OFFSET + BUTTON_WIDTH + OFFSET + OFFSET, 
					STAT_STARTING_POS + (i * LINE_HEIGHT), 
					STAT_LABEL_WIDTH, 
					LINE_HEIGHT
				), ((SkillName) i).ToString());
			GUI.Label(new Rect(
				OFFSET + STAT_LABEL_WIDTH + OFFSET + STAT_VALUE_WIDTH + OFFSET + BUTTON_WIDTH + OFFSET + BUTTON_WIDTH + OFFSET + OFFSET + STAT_LABEL_WIDTH + OFFSET, 
					STAT_STARTING_POS + (i * LINE_HEIGHT), 
					STAT_VALUE_WIDTH, 
					LINE_HEIGHT
				), _toon.GetSkill(i).AdjustedBaseValue.ToString());
		}
	}

	private void DisplayPointsLeft() {
		GUI.Label(new Rect(
				180, 
				10, 
				STAT_LABEL_WIDTH, 
				LINE_HEIGHT
			), "Points Left:" + pointsLeft);
	}

	private void DisplayCreateButton() {
		if(GUI.Button(new Rect(Screen.width / 2 - 50, STAT_STARTING_POS + (10 * LINE_HEIGHT), 100, LINE_HEIGHT), "Create")) {
			Debug.Log("Getting game settings.");
			GameSettings gsScript = GameObject.Find("__GameSettings").GetComponent<GameSettings>();
			gsScript.SaveCharacterData();
			SceneManager.LoadScene("Targeting Example");
		}
	}
}
