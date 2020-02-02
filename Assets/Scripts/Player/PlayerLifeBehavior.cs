using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLifeBehavior : MonoBehaviour
{
    public int maxLife;
    public int Life;
    private SpriteRenderer spriteOfPlayer;
    public bool IsAlive = true;
    void Start()
    {
        spriteOfPlayer = GetComponent<SpriteRenderer>();
    }
    public void TakeDamagePlayer(int damage)
    {
        Life -= damage;
        StartCoroutine("DamageCoroutine");
    }

    IEnumerator DamageCoroutine()
    {
        for (float i = 0; i <= 0.2f; i += 0.2f)
        {
            spriteOfPlayer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteOfPlayer.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
        StopCoroutine(DamageCoroutine());
    }

    void Update()
    {
       if(Life <= 0)
        {
            IsAlive = false;
            SceneManager.LoadScene("Continua");
            Destroy(this.gameObject);
        }

    }
}
