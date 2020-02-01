using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeBehavior : MonoBehaviour
{
    [SerializeField] private int Life;
    [SerializeField] private int Attack;
    [SerializeField] private int recoverValue;

    public void TakeDamage (int damage)
    {
        Life -= damage;
    }

    public void Recovery()
    {
        Life += recoverValue;
    }
}
