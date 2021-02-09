using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //Movement
    public float moveSpeed = 5.0f;
    private float moveModifier = 1.0f;

    private Vector2 movement = new Vector2();
    private Rigidbody2D body;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        //Attach components to Variables
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();


    } //end Start()

    // Update is called once per frame
    void Update()
    {
        //Get Inputs
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //Send movement Info to Animator
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        float speed = movement.magnitude; //Slightly faster: movement.sqrMagnitude
        animator.SetFloat("Speed", speed);


        //Face the Mouse
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 toMouse = mouseWorldPos - body.position;

        if (speed < 0.01)
        {
            animator.SetFloat("Horizontal", toMouse.x);
            animator.SetFloat("Vertical", toMouse.y);
        }

    } //end Update()

    private void FixedUpdate()
    {
        if (movement.x != 0 || movement.y != 0)
        {
            //Normalize the movement vector
            if (movement.magnitude > 1)
                movement.Normalize();

            Move(movement * moveSpeed * moveModifier);

        }
    } //end FixedUpdate()

    private void Move(Vector2 move)
    {
        body.velocity += new Vector2(move.x * 0.2f, move.y * 0.2f);
        if (body.velocity.magnitude > moveSpeed)
        {
            body.velocity = body.velocity.normalized * moveSpeed;
        }

        //Uses Drag
        //body.velocity = new Vector2(move.x, move.y);
        //Simplest movement
        //body.MovePosition(body.position + move * Time.deltaTime);
    }


} //end Class Player
