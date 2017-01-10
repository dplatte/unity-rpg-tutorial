using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : MonoBehaviour {

	public List<Transform> targets;
	public Transform selectedTarget;

	private Transform myTransform;

	// Use this for initialization
	void Start () {
		myTransform = transform;
		targets = new List<Transform> ();

		AddAllEnemies();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Tab)) {
			TargetEnemy ();
		}
	}

	public void AddAllEnemies() {
		GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject enemy in gos) {
			AddTarget(enemy.transform);
		}
	}

	public void AddTarget(Transform enemy) {
		targets.Add(enemy);
	}

	private void TargetEnemy() {
		if (selectedTarget == null) {
			SortTargetsByDistance ();
			selectedTarget = targets [0];
		} else {
			DeselectTarget ();
			int index = targets.IndexOf(selectedTarget);

			if (index < targets.Count - 1) {
				selectedTarget = targets [index + 1];
			} else {
				selectedTarget = targets [0];
			}
		}
		SelectTarget ();
	}

	private void SelectTarget() {
		selectedTarget.GetComponent<Renderer>().materials[0].color = Color.red;

		PlayerAttack pa = (PlayerAttack)GetComponent<PlayerAttack> ();

		pa.target = selectedTarget.gameObject;
	}

	private void DeselectTarget() {
		selectedTarget.GetComponent<Renderer>().materials[0].color = Color.white;
		selectedTarget = null;

		PlayerAttack pa = (PlayerAttack)GetComponent<PlayerAttack> ();

		pa.target = null;
	}

	private void SortTargetsByDistance(){
		targets.Sort (delegate(Transform t1, Transform t2) {
			return(Vector3.Distance (t1.position, myTransform.position).CompareTo(Vector3.Distance(t2.position, myTransform.position)));
		});
	}
}
