using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Stats")]
    private float horizontalMove;
    public float speed;
    public float jumpForce;
    public float checkRadius;
    private int extraJumps = 1;
    public int extraJumpsValue;
    public bool facingRight = true;
    public float RateOfFire = 0.5f;
    private float rateOfFire;

    [Header("Components")]
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    public GameObject gameController;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public Text JumpCounter;
    public Transform firePointRight;
    public Transform firePointLeft;
    public GameObject bullet;
    private GameController gameControllerScript;
    private Text jumpCounterText;
    public bool isGrounded;
    private bool isDead;
    private bool isAutomatic = false;
    private GameObject gameControllerObject;
    private GameObject jumpCountText;
    void Awake()
    {
        //Checking if there's a Game Controller and a jump text in the level already.
        gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        jumpCountText = GameObject.FindGameObjectWithTag("JumpText");


        //If a Game Controller is not null (is found), then grab that component.
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject;
        }
        //If a jump text is not null (is found), then grab that component.
        if (jumpCountText != null)
        {
            JumpCounter = jumpCountText.GetComponent<Text>();
        }
        //Setting certain components and preparing things.
        extraJumps = extraJumpsValue;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        rateOfFire = RateOfFire;
    }

    void FixedUpdate()
    {
        rateOfFire -= Time.deltaTime;
        horizontalMove = Input.GetAxisRaw("Horizontal");
        if (anim.GetBool("Dead") == true)
        {
            rb.velocity = new Vector2(0, 0);
        }
        if (Input.GetAxisRaw("Horizontal") > 0.01f && isDead == false || Input.GetAxisRaw("Horizontal") < -0.01f && isDead == false)
        {
            transform.Translate(new Vector2(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, 0f));
            Flip();
        }
        isDead = anim.GetBool("Dead");
        anim.SetBool("Grounded", isGrounded);
        anim.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            isAutomatic = !isAutomatic;
        }
        //Used for the double/triple jump.
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        if (isGrounded)
        {
            extraJumps = extraJumpsValue;
            UpdateJumpCounter();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded && isDead == false || Input.GetKeyDown(KeyCode.W) && isGrounded && isDead == false)
        {
            anim.SetTrigger("TakeOff");
            isGrounded = false;
            rb.velocity = Vector2.up * jumpForce;
            UpdateJumpCounter();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps > 0 && isDead == false || Input.GetKeyDown(KeyCode.W) && extraJumps > 0 && isDead == false)
        {
            anim.SetTrigger("TakeOff");
            isGrounded = false;
            extraJumps--;
            rb.velocity = Vector2.up * jumpForce;
            UpdateJumpCounter();
        }
        if (Input.GetButtonDown("Fire1") && !isAutomatic || isAutomatic && rateOfFire < 0f)
        {
            rateOfFire = RateOfFire;
            GunShooting();
        }
        UpdateJumpCounter();

    }
    public void Flip()
    {
        if (Input.GetAxisRaw("Horizontal") < 0.01f && isDead == false)
        {
            sr.flipX = true;
            facingRight = false;
        }
        else
        {
            sr.flipX = false;
            facingRight = true;
        }
    }
    public void GunShooting()
    {
        //Shooting Logic
        if (facingRight)
        {
            GameObject projectile = Instantiate(bullet, firePointRight.position, firePointRight.rotation);
            Destroy(projectile, 1.0f);
        }
        if (!facingRight)
        {
            GameObject projectile = Instantiate(bullet, firePointLeft.position, new Quaternion(0f, 180f, 0f, 0f));
            Destroy(projectile, 1.0f);
        }
    }
    public void UpdateJumpCounter()
    {
        //Depending on how many extra jumps the player has, Update the UI.
        if (isGrounded && extraJumps == 0)
        {
            JumpCounter.text = ($"You can jump 1 more time");
        }
        if (extraJumps > 0)
        {
            JumpCounter.text = ($"You can jump {extraJumps} more times");
        }
        if (extraJumps == 0)
        {
            JumpCounter.text = ("You can jump no more times.");
        }
    }
}