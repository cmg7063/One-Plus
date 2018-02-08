using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour {

    private bool direction; // true = right, false = left
    private float yPosition;
    public float speed;

    // Use this for initialization
    void Start () {
        direction = true;
        yPosition = 0;
        speed = 2.0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (direction)
        {
            transform.Translate(0.04f, 0, 0);
            yPosition += 0.04f;
            if(yPosition >= 4f)
            {
                direction = false;
            }
        }
        else
        {
            transform.Translate(-0.04f, 0, 0);
            yPosition -= 0.04f;
            if (yPosition <= 0)
            {
                direction = true;
            }
        }
    }
}
