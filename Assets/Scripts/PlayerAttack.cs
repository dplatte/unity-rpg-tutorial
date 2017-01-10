﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

	public GameObject target;
	public float attackTimer;
	public float coolDown;

	// Use this for initialization
	void Start () {
		attackTimer = 0;
		coolDown = 2.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (attackTimer > 0) {
			attackTimer -= Time.deltaTime;
		} else {
			attackTimer = 0;
		}

		if (attackTimer == 0 && Input.GetKeyUp (KeyCode.F)) {
			Attack ();
		}
	}

	private void Attack() {
		float distance = Vector3.Distance(target.transform.position, transform.position);

		Vector3 dir = (target.transform.position - transform.position).normalized;

		float direction = Vector3.Dot (dir, transform.forward);
		if (distance < 2.5f && direction > 0) {
			EnemyHealth eh = (EnemyHealth)target.GetComponent ("EnemyHealth");
			eh.AdjustCurrentHealth (-10);
			attackTimer = coolDown;
		}
	}
}
