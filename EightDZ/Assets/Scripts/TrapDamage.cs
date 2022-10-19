using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    [SerializeField] private int damageValue;
    private float timeColliding;
    private float timeThreshold = 2f;

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out CharacterController2D characterController2D)) //[SecuritySafeCritical] ???
        {
            if (characterController2D.isVulnerable == true)
            {
                characterController2D.HealthSystem.Damage(damageValue);
            }
            //Deal damage while jumping bug... How to Fix it?

        }
    }
    private void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out CharacterController2D characterController2D)) //[SecuritySafeCritical] ???
        {
            if (timeColliding < timeThreshold)
            {
                timeColliding += Time.deltaTime;
            }
            else
            {
                if (characterController2D.isVulnerable == true)
                {
                    characterController2D.HealthSystem.Damage(damageValue);
                }
                timeColliding = 0f;
            }
        }
    }
}
