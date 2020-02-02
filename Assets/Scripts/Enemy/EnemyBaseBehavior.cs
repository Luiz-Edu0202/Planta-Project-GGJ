using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyBaseBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 playerDistance;
    [SerializeField]private GameObject player;
    private SpriteRenderer sprite;
    private bool playerIsAlive;
    [SerializeField] private int life;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private ItemDrop itemInThisEnemy;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        playerDistance = player.GetComponent<Transform>().position - transform.position;
        if (Mathf.Abs(playerDistance.x) < 12 && Mathf.Abs(playerDistance.y) < 3)
        {
            rb.velocity = new Vector2(speed * (playerDistance.x / Mathf.Abs(playerDistance.x)), rb.velocity.y);
        }
        Flip();   
    }
    void Flip()
    {
        Vector3 scale = transform.localScale;
        if (playerDistance.x < 0)
        {
            if (scale.x < 0)
            {
                scale.x *= -1;
            }
        }
        else if (playerDistance.x > 0)
        {
            if (scale.x > 0)
            {
                scale.x *= -1;
            }
        }
        transform.localScale = scale;
    }

    public void TakeDamageEnemy(int damage)
    {
        life -= damage;
        if(life <= 0)
        {
            if(itemInThisEnemy != null)
            {
                Instantiate(itemInThisEnemy, transform.position, Quaternion.identity);
            }
            Destroy(this.gameObject);
        }
        else
        {
            StartCoroutine("DamageCoroutine");
        }
    }
    IEnumerator DamageCoroutine()
    {
        for (float i = 0; i < 0.2f; i += 1f)
        {
            sprite.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            sprite.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
       if(col.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerLifeBehavior>().TakeDamagePlayer(damage);
            StartCoroutine("CountDownOfDamage");
       } 
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StopCoroutine("CountDownOfDamage");
        }
    }
    IEnumerator CountDownOfDamage()
    {
        yield return new WaitForSeconds(1f);
        player.GetComponent<PlayerLifeBehavior>().TakeDamagePlayer(damage / damage);
        print("lala");
        if (life > 0)
        {
            StartCoroutine("CountDownOfDamage");
        }

    }


}
