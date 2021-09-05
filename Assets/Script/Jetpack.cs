using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jetpack : MonoBehaviour
{
    [Header("Jetpack Values")]
    [SerializeField]
    private float jetpackForce = 5f;
    [SerializeField]
    private float fuel;
    [SerializeField]
    private float currentFuel;
    [SerializeField]
    private float fuelBurn;
    [SerializeField]
    private float rate = 0.05f;
    private int direction;
    private bool isFlying = false;

    [Header("Components")]
    public Slider slider;
    private Rigidbody2D rb;
    private PlayerMovement pm;
    private Animator anim;

    private void Awake()
    {
        //Set components up
        rb = GetComponent<Rigidbody2D>();
        pm = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        slider.value = currentFuel / fuel;
        anim.SetBool("Flying", isFlying);
        if (pm.isGrounded)
        {
            isFlying = false;
            rb.velocity = new Vector2(0, rb.velocity.y);
            currentFuel = fuel;
        }
        if (Input.GetButton("Jump") && currentFuel > 0)
        {
            isFlying = true;
            Direction(direction = Fly());
            currentFuel -= fuelBurn * rate;
        }
    }
    private void Direction(int direction)
    {
        switch (direction)
        {
            case 1:
                rb.velocity = Vector2.left * jetpackForce;
                break;
            case 2:
                rb.velocity = Vector2.right * jetpackForce;
                break;
            case 3:
                rb.velocity = Vector2.up * jetpackForce * 2;
                break;
            case 4:
                rb.velocity = Vector2.down * jetpackForce * 2;
                break;
            case 5:
                rb.velocity = (Vector2.left * jetpackForce) + (Vector2.up * jetpackForce * 2);
                break;
            case 6:
                rb.velocity = (Vector2.right * jetpackForce) + (Vector2.down * jetpackForce * 2);
                break;
            case 7:
                rb.velocity = (Vector2.up * jetpackForce * 2) + (Vector2.right * jetpackForce);
                break;
            case 8:
                rb.velocity = (Vector2.down * jetpackForce * 2) + (Vector2.left * jetpackForce);
                break;
            default:
                break;
        }
    }
    private int Fly()
    {
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)))
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                return direction = 5;

            }
            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                return direction = 8;
            }
            else
            {
                return direction = 1;
            }
        }
        else if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && Input.GetKey(KeyCode.Space))
        {
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                return direction = 6;
            }
            else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                return direction = 7;
            }
            else
            {
                return direction = 2;
            }
        }
        else if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && Input.GetKey(KeyCode.Space))
        {
            return direction = 3;
        }
        else if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && Input.GetKey(KeyCode.Space))
        {
            return direction = 4;
        }
        else return direction = 0;
    }
}