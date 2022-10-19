using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeJointMotorReverce : MonoBehaviour
{
    [SerializeField] private float motorSpeed = 100;
    private HingeJoint2D hinge;
    private JointMotor2D motor;
    private void Start()
    {
        hinge = GetComponent<HingeJoint2D>();
        motor = hinge.motor;
    }
    private void Update()
    {
        if (hinge.jointAngle >= 180)
        {
            motor.motorSpeed = -motorSpeed;
            hinge.motor = motor;
        }
        if (hinge.jointAngle <= 0)
        {
            motor.motorSpeed = motorSpeed;
            hinge.motor = motor;
        }
    }
}
