using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    private float movementSpeed;
    private bool hasKey;
    private bool facingRight;

    private Rigidbody2D rigidbody;
    private Animator animator;

    [SerializeField]
    private Transform[] groundPoints;

    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround;

    private bool isGrounded;

    private bool jump;

    private float jumpForce;

    private string level;
    private int levNum;

    // Use this for initialization
    void Start()
    {
        movementSpeed = 3;
        groundRadius = 0.2f;
        jumpForce = 400;
        hasKey = false;
        facingRight = true;
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        level = "level1";
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
        isGrounded = IsGrounded();
        Move(horizontal);
        Flip(horizontal);
        ResetValues();
    }

	// collision detection logic
	void OnCollisionEnter2D(Collision2D collision) {
		// if collision is arrow delete the arrow and respawn the player
		if (collision.gameObject.tag == "Arrow")
        {
			Destroy (collision.gameObject);
			RespawnPlayer ();
		}
        if (collision.gameObject.tag == "Enemy") // for now, specify enemy tag
        {
            RespawnPlayer();
        }
        if (collision.gameObject.tag == "Key")
        {
            //collision.gameObject.SetActive(false);
            Destroy(collision.gameObject);
            hasKey = true;
        }
        if (collision.gameObject.tag == "Goal" && hasKey)
        {
            // Code to enter next level
            levNum++;
            level = "level" + levNum;
            Debug.Log(level);
            // SceneManager.LoadScene([Level here]);
            RespawnPlayer();
        }
    }

    private bool IsGrounded()
    {
        if (rigidbody.velocity.y <= 0)
        {
            foreach(Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for(int i = 0; i < colliders.Length; i++)
                {
                    if(colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void Move(float horizontal)
    {
        rigidbody.velocity = new Vector2(horizontal * movementSpeed, rigidbody.velocity.y);
        animator.SetFloat("speed", Mathf.Abs(horizontal));

        if(isGrounded && jump)
        {
            isGrounded = false;
            rigidbody.AddForce(new Vector2(0, jumpForce));
        }
    }

    private void HandleInput()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
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

    private void ResetValues()
    {
        jump = false;
    }

	// respawn the player at starting point
	private void RespawnPlayer()
    {
		//transform.position =  new Vector3(-15f, -8.75f, 0);
        // replace "tileTest" with level eventually
        SceneManager.LoadScene("tileTest", LoadSceneMode.Single);
    }

	// if player goes out of bound respawn player
    private void Bounds()
    {
        if (this.gameObject.transform.position.y <= -12f)
        {
			RespawnPlayer ();
        }
    }
}