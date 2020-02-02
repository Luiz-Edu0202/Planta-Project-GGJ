using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerLifeBehavior playerLife;
    private bool onGround;
    private float x;
    private float y;
    private ItemType typeOfItem;
    [SerializeField] private int bustStregth;
    [SerializeField] private int bustSpeed;
    [SerializeField] private int bustHelth;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canDoubleJump = true;
    [SerializeField] private bool DoubleJumpAccess;
    [SerializeField] private GameObject attack;
    [SerializeField] private float maxSpeed = 4;
    [SerializeField] float jumpForce = 300;
    [SerializeField] private float currentSpeed;
    void Start()
    {
        currentSpeed = maxSpeed;
        rb = GetComponent<Rigidbody2D>();
        playerLife = GetComponent<PlayerLifeBehavior>();
    }

    void FixedUpdate()
    {
        Controls();
        Movimentate();

    }
    void Controls()
    {

        if (Input.GetKeyDown(KeyCode.X) && onGround && canJump)
        {
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.X) && !canJump && DoubleJumpAccess && canDoubleJump)
        {
            DoubleJump();
        }
        
        x = Input.GetAxis("Horizontal") * currentSpeed;
        if(Input.GetKeyDown(KeyCode.Z))
        {
            attack.SetActive(true);
            attack.GetComponent<PlayerAttackBehavior>().Attack();

        }
    }

    void Movimentate()
    {
        rb.velocity = new Vector2(x, rb.velocity.y);
        Flip();
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
        if(thisItem == ItemType.Cal)
        {
            currentSpeed += bustSpeed;
            typeOfItem = thisItem;
            StartCoroutine("EndBust");
        }
        else if(thisItem == ItemType.Agua)
        {
            GetComponent<PlayerLifeBehavior>().Life += bustHelth;
            typeOfItem = thisItem;
            StartCoroutine("EndBust");
        }
        else if (thisItem == ItemType.Adubo)
        {
            GetComponent<PlayerAttackBehavior>().attackValue += bustStregth ;
            typeOfItem = thisItem;
            StartCoroutine("EndBust");
        }
    }

    IEnumerator EndBust()
    {
        yield return new WaitForSeconds(15f);

        if (typeOfItem == ItemType.Cal)
        {
            currentSpeed = maxSpeed;
        }
        else if (typeOfItem == ItemType.Agua)
        {
            GetComponent<PlayerLifeBehavior>().Life = GetComponent<PlayerLifeBehavior>().maxLife;
        }
        else if (typeOfItem == ItemType.Adubo)
        {
            GetComponent<PlayerAttackBehavior>().attackValue = GetComponent<PlayerAttackBehavior>().attackValue - bustStregth;
        }
    }
}
