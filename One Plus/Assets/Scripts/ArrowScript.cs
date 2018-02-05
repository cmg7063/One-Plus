using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript: MonoBehaviour {
	public float speed = 1;

	private Rigidbody2D rigidbody;
	// Use this for initialization
	void Start () {

		// ignore collisions with othe traps
		Physics2D.IgnoreLayerCollision (9, 9, true);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.right * Time.deltaTime * speed);
	}

	// collision detection logic
	void OnCollisionEnter2D(Collision2D collision) {
		// destroy self it collision with ground
		if (collision.gameObject.tag == "Ground") {
			Destroy (this.gameObject);
		} else if (collision.gameObject.tag == "Player") {
			Destroy(this.gameObject);
		}
	}
}
