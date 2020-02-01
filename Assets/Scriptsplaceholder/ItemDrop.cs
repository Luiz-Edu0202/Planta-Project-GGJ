using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public Item itemTaked;

    public SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = itemTaked.image;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print(other);
        PlayerMovBehavior player = other.GetComponent<PlayerMovBehavior>();
        if(player != null)
        {
            
            player.ItemFunction(itemTaked.thisItem);
            Destroy(this.gameObject);
        }
    }

   
}
