using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelScript : MonoBehaviour {
	public bool inGround = true;

    private Animator animator;

    public GameObject throwingObject;
	public float throwSpeed = 1;
	public Vector3 ThrowDirection = Vector3.left;

	public float timeOffset = 5;
	public float maxTime = 5;
	private float timeLeft;

	// Use this for initialization
	void Start ()
    {
		timeLeft = timeOffset;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update ()
    {
		timeLeft -= Time.deltaTime;

		// fires an arrow and restarts the timer
		if (timeLeft <= 0)
        {
			FireObject ();
            animator.SetTrigger("throw");
            timeLeft = 5;
		}
	}

	// fires the throwing object
	public void FireObject()
    {
		GameObject Clone;
        animator.SetTrigger("throw");

        //spawning the object at position
        Clone = (Instantiate(throwingObject, transform.position, transform.rotation)) as GameObject;

		// if inGround spawn with larger offset (prevent object instantly destorying itself by ground collision)
		if (inGround)
        {
			Clone.transform.Translate (ThrowDirection * 0.70f);
		}
        else
        {
			Clone.transform.Translate (ThrowDirection * 0.20f);
		}

		// set arrow speed and velocity
		Clone.GetComponent<AcornScript> ().speed = throwSpeed;
		Clone.GetComponent<AcornScript> ().Velocity = ThrowDirection;
	}
}
