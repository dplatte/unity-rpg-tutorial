using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameSettings : MonoBehaviour {

	void Awake() {
		//Prevents destroying this when moving scenes
		DontDestroyOnLoad(this);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SaveCharacterData() {
		GameObject pc = GameObject.Find("PC");
		PlayerCharacter pcClass = pc.GetComponent<PlayerCharacter>();
		PlayerPrefs.SetString("Player Name", pcClass.Name);

		for(int i = 0; i < Enum.GetValues(typeof(AttributeName)).Length; i++) {
			PlayerPrefs.SetInt(((AttributeName) i).ToString() + " - Base Value", pcClass.GetPrimaryAttribute(i).BaseValue);
			PlayerPrefs.SetInt(((AttributeName) i).ToString() + " - Exp To Level", pcClass.GetPrimaryAttribute(i).ExpToLevel);
		}
	}

	public void LoadCharacterData() {

	}
}
