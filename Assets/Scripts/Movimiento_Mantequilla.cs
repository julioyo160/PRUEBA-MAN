using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movimiento_Mantequilla : MonoBehaviour
{
    Animator animator;

    //Run
    public int runSpeed = 1;
    float horizontal;
    float vertical;
    bool facingRight;

    //Crounch

    //Slider

    //Jump
    public float jumpForce = 300;
    Rigidbody2D rigidbody1;
    float axisY;
    public bool isJumping;


    void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody1 = GetComponent<Rigidbody2D>();
        rigidbody1.Sleep();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        animator.SetFloat("Speed", Mathf.Abs(horizontal != 0 ? horizontal : vertical));

        if(transform.position.y < axisY)
            OnLanding();

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            axisY = transform.position.y;
            isJumping = true;
            rigidbody1.gravityScale = 1.5f;
            rigidbody1.WakeUp();
            rigidbody1.AddForce(new Vector2(transform.position.x + 7.5f, jumpForce));
            animator.SetBool("IsJumping", isJumping);
            Debug.Log("salto");
        }

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("isAtacking", true);
        }


    }

    public void endAttack()
    {
        animator.SetBool("isAtacking", false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "leche")
        {
            isJumping = false;
            rigidbody1.gravityScale = 0f;
            rigidbody1.Sleep();
            animator.SetBool("IsJumping", false);
            Debug.Log("aterrizo");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "leche")
        {
            rigidbody1.gravityScale = 1.5f;
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(horizontal * runSpeed, vertical * runSpeed, 0.0f);
        Vector3 newPosition = transform.position + movement * Time.deltaTime;
        newPosition.y = Mathf.Clamp(newPosition.y, -4.40f, 0f); // Establece los límites minY y maxY
        transform.position = newPosition;
        Flip(horizontal);
    }

    private void Flip(float horizontal)
    {
        if (horizontal < 0 && !facingRight || horizontal > 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;

        }
    }

    void OnLanding()
    {
        isJumping = false;
        rigidbody1.gravityScale = 0f;
        rigidbody1.Sleep();
        axisY = transform.position.y;
        animator.SetBool("IsJumping", false);
        Debug.Log("aterrizo");

    }

  

}
