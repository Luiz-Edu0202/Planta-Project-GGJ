using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerAnimBehavior anim;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canDoubleJump = true;
    [SerializeField] private bool DoubleJumpAccess;
    [SerializeField] private bool isDead = false;
    private bool onGround;
    private float x;
    private float y;
    private PlayerLifeBehavior playerLife;
    [SerializeField] private float maxSpeed = 4;
    [SerializeField] float jumpForce = 300;
    [SerializeField] private float currentSpeed;
    void Start()
    {
        currentSpeed = maxSpeed;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<PlayerAnimBehavior>();
        playerLife = GetComponent<PlayerLifeBehavior>();
    }

    void FixedUpdate()
    {
        Controls();
        Movimentate();

    }
    void Controls()
    {

        if (Input.GetButtonDown("Jump") && onGround && canJump)
        {
            Jump();
        }
        else if (Input.GetButtonDown("Jump") && !canJump && DoubleJumpAccess && canDoubleJump)
        {
            DoubleJump();
        }
        
        x = Input.GetAxis("Horizontal") * currentSpeed;
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Attack();
        }
    }

    void Movimentate()
    {
       
        if (!isDead)
        {


            rb.velocity = new Vector2(x, rb.velocity.y);
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                anim.WalkingAnim();
            }
            else
            {
                anim.EndWalkingAnim();
            }
            Flip();
        }
    }
    void Jump()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * jumpForce, 0);
        canJump = false;

    }
    void DoubleJump()
    {
        rb.AddForce(Vector2.up * jumpForce, 0);
        canDoubleJump = false;

    }
    void Flip()
    {
        Vector3 scale = transform.localScale;
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            if (scale.x < 0)
            {
                scale.x *= -1;
            }
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            if (scale.x > 0)
            {
                scale.x *= -1;
            }
        }
        transform.localScale = scale;
    }

    void Attack()
    {
        anim.AttackAnim(); ;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Ground")
        {
            onGround = true;
            canJump = true;
            canDoubleJump = true;
        }

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = false;
            canJump = false;
        }
    }

    public void ItemFunction(ItemType thisItem)
    {
        if(thisItem == ItemType.Mana)
        {
            jumpForce += 300;
            StartCoroutine("EndBust");
        }
        else if(thisItem == ItemType.Helf)
        {
           playerLife.Recovery();

        }
    }

    IEnumerator EndBust()
    {
        yield return new WaitForSeconds(15f);
        jumpForce -= 300;
    }
}
