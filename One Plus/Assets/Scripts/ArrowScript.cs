using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript: MonoBehaviour {
	public float speed = 1;
	public float rotationDegree = 0;
//	Collider2D player = GetComponent(Collider2D);

	// Use this for initialization
	void Start () {
		Vector3 startRotation = new Vector3 (0, 0, rotationDegree);

		transform.Rotate (startRotation);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.right * Time.deltaTime * speed);
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Ground") {
			Destroy (this.gameObject);
		} else if (other.gameObject.tag == "Player") {
			Destroy (this.gameObject);
		}
	}
}
