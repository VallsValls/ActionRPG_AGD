using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //Movement
    public float moveVel = 5.0f;
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
        movement.x = Input.GetAxisRaw("Hor");
        movement.y = Input.GetAxisRaw("Ver");

        //Send movement Info to Animator
        animator.SetFloat("Hor", movement.x);
        animator.SetFloat("Ver", movement.y);
        float Vel = movement.magnitude; //Slightly faster: movement.sqrMagnitude
        animator.SetFloat("Vel", Vel);


        //Face the Mouse
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 toMouse = mouseWorldPos - body.position;

        if (Vel < 0.01)
        {
            animator.SetFloat("Hor", toMouse.x);
            animator.SetFloat("Ver", toMouse.y);
        }

    } //end Update()

    private void FixedUpdate()
    {
        if (movement.x != 0 || movement.y != 0)
        {
            //Normalize the movement vector
            if (movement.magnitude > 1)
                movement.Normalize();

            Move(movement * moveVel * moveModifier);

        }
    } //end FixedUpdate()

    private void Move(Vector2 move)
    {
        body.velocity += new Vector2(move.x * 0.2f, move.y * 0.2f);
        if (body.velocity.magnitude > moveVel)
        {
            body.velocity = body.velocity.normalized * moveVel;
        }

        //Uses Drag
        //body.velocity = new Vector2(move.x, move.y);
        //Simplest movement
        //body.MovePosition(body.position + move * Time.deltaTime);
    }


} //end Class Player
