using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	// object related
	private Rigidbody2D rigidbody;
	private Animator animator;

	[SerializeField]
	private LayerMask whatIsGround;

	// movement related
    private float movementSpeed;
	private bool facingRight;

	// jumping related props
	[SerializeField]
	private Transform[] groundPoints;
    private bool isGrounded;
	private bool jumping;
	private bool jumped;
	private float groundRadius;
	private float currentJumpForce;
    private float initJumpForce;
	private float maxJumpForce;

	// level related
	private bool hasKey;
    private string level;
    private int levNum;

    // Use this for initialization
    void Start()
    {
		rigidbody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();

        movementSpeed = 3;
		facingRight = true;

		groundRadius = 0.2f;
		currentJumpForce = 0f;
		initJumpForce = 300f;
		maxJumpForce = 500f;

        hasKey = false;
        level = "Level1";
        levNum = 1;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
		isGrounded = checkGrounded();
        Move(horizontal);
        Flip(horizontal);

        HandleLayers();
    }

	// collision detection logic
	void OnCollisionEnter2D(Collision2D collision) {
		// if collision is arrow delete the arrow and respawn the player
		if (collision.gameObject.tag == "Projectile") {
			Destroy (collision.gameObject);
			RespawnPlayer ();
		}
        if (collision.gameObject.tag == "Enemy") {
            RespawnPlayer();
        }
        if (collision.gameObject.tag == "Key") {
            //collision.gameObject.SetActive(false);
            Destroy(collision.gameObject);
            hasKey = true;
        }
        if (collision.gameObject.tag == "Goal" && hasKey) {
            // Code to enter next level
            levNum++;
            level = "Level" + levNum;
            Debug.Log(level);
            // SceneManager.LoadScene([Level here]);
            RespawnPlayer();
        }
    }

	// checks if player is touching the ground
    private bool checkGrounded()
    {
        if (rigidbody.velocity.y <= 0)
		{
			Debug.Log (groundPoints.Length);
            foreach(Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for(int i = 0; i < colliders.Length; i++)
                {
					Debug.Log ("colliders");
                    if(colliders[i].gameObject != gameObject)
                    {
                        animator.ResetTrigger("jump");
						Debug.Log ("passed check");
                        return true;
                    }
                }
            }
        }
        return false;
    }

	// Movement logic function
    private void Move(float horizontal)
    {
        rigidbody.velocity = new Vector2(horizontal * movementSpeed, rigidbody.velocity.y);
        animator.SetFloat("speed", Mathf.Abs(horizontal));

		Jump ();
    }

	// handles jumping logic
	private void Jump () {
		if (isGrounded && jumping)
		{	
			// jump up with inital jump force
			jumped = true;
			isGrounded = false;
            animator.SetTrigger("jump");

			rigidbody.AddForce (Vector2.up * initJumpForce);
			currentJumpForce = initJumpForce;
		} else if (!isGrounded && jumping && jumped) {
			// if space is still down increase jump height
			if (currentJumpForce < maxJumpForce) {
				rigidbody.AddForce (Vector2.up * 10f);
				currentJumpForce += 10f;
			}
		}
	}

    private void HandleInput()
    {
		if (Input.GetKeyDown (KeyCode.Space)) {
			jumping = true;
		} else if (Input.GetKeyUp (KeyCode.Space)) {
			jumping = false;
			jumped = false;
		}
    }

    private void Flip(float horizontal)
    {
        if(horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

	// respawn the player at starting point
	private void RespawnPlayer()
    {
		// get the current scene and reload the scene
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name, LoadSceneMode.Single);
    }

	// if player goes out of bound respawn player
    private void Bounds()
    {
        if (this.gameObject.transform.position.y <= -12f)
        {
			RespawnPlayer ();
        }
    }

    private void HandleLayers()
    {
        if(!isGrounded)
        {
            animator.SetLayerWeight(1, 1);
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }
    }
}