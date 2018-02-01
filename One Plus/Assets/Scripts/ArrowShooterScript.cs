using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooterScript : MonoBehaviour {
	public GameObject arrowPrefab;
	public float timeLeft = 5;
    private float maxTime;

	// Use this for initialization
	void Start () {
		maxTime = timeLeft;

	}
	
	// Update is called once per frame
	void Update () {
		timeLeft -= Time.deltaTime;

		if (timeLeft <= 0) {
			FireArrow ();

			timeLeft = 5;
		}
	}

	public void FireArrow() {
		//Clone of the bullet
		GameObject Clone;

		//spawning the bullet at position
//		Vector3 pos = transform.position + (Vector3.right * 0.55f * transform.rotation);
		Clone = (Instantiate(arrowPrefab, transform.position, transform.rotation)) as GameObject;
		Clone.transform.Translate (Vector3.right * 0.55f);
		Debug.Log ("Arrow spawned is found");
	}
}
