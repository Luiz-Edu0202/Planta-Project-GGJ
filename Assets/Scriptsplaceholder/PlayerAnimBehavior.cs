using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimBehavior : MonoBehaviour
{
    public Animator animCharater;

    public void WalkingAnim()
    {
        animCharater.SetBool("Walking", true);
    }
    public void EndWalkingAnim()
    {
        animCharater.SetBool("Walking", false);
    }
    public void AttackAnim()
    {
        animCharater.SetTrigger("Attack");
    }
}
