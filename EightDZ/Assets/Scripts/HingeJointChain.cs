using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeJointChain : MonoBehaviour
{
    [SerializeField] private Rigidbody2D ball;
    [SerializeField] private  Collider2D triggerColl;
    private int count=0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (count >= 1)
        {
            //GetComponent<HingeJointChain>().enabled = false;
            //How to disable script itself???
            return;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            count++;
            ball.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            ball.velocity = Vector3.left;
        }
    }
}

/*private void OnCollisionEnter2D(Collision2D collision)
{
    count++;
    if (count > 1)
    {
        //GetComponent<HingeJointChain>().enabled = false;
        return;
    }
    ball.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
    ball.velocity = Vector3.left;
}*/