using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornScript : MonoBehaviour {

	// default values (change values using squirrel script)
	public float speed = 1;
	public Vector3 Velocity = Vector3.left;

	private Rigidbody2D rigidbody;

	// Use this for initialization
	void Start () {
		// ignore collisions with othe traps
		Physics2D.IgnoreLayerCollision (9, 9, true);
		Physics2D.IgnoreLayerCollision (9, 10, true);
	}

	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.forward * speed * 2, Space.Self);
		transform.Translate (Velocity * Time.deltaTime * speed, Space.World);
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
