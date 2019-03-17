using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public float maxSpeed = 6.0f;
    public bool facingRight = true;
    public float moveDirection;
    private Rigidbody rigidbody;
    private Animator anim;
    public float jumpSpeed = 600.0f;
    public bool grounded = false;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    void Awake()
    {
        groundCheck = GameObject.Find("GroundCheck").transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = Input.GetAxis("Horizontal");
        if (grounded && Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("IsJumping");
            rigidbody.AddForce(new Vector2(0, jumpSpeed));
        }
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        rigidbody.velocity = new Vector2 (moveDirection * maxSpeed,rigidbody.velocity.y);
        if (moveDirection > 0.0f && !facingRight)
        {
            flip();
        }
        else if (moveDirection < 0.0f && facingRight)
        {
            flip();
        }
        anim.SetFloat("Speed", Mathf.Abs(moveDirection));
    }
    void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up, 180.0f, Space.World);
    }
}
