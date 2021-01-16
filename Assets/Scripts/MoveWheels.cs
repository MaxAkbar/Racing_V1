using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWheels : MonoBehaviour
{
    public WheelCollider[] wheelColliders;
    public GameObject[] wheels;

    public float torque = 2000;

    private float acceleration;
    private float steering;
    private float maxSteeringAngle = 30;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        acceleration = Input.GetAxis("Vertical");
        steering = maxSteeringAngle * Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        Drive();
    }

    private void Drive()
    {
        for (int i = 0; i < 4; i++)
        {
            acceleration = Mathf.Clamp(acceleration, -1, 1);
            float steer = Mathf.Clamp(steering, -1, 1) * maxSteeringAngle;
            float thrustTorque = acceleration * torque;

            wheelColliders[i].motorTorque = thrustTorque;

            if (i < 2)
            {
                wheelColliders[i].steerAngle = steer;
            }

            // mesh 
            Quaternion quat;
            Vector3 pos;

            wheelColliders[i].GetWorldPose(out pos, out quat);

            wheels[i].transform.position = pos;
            wheels[i].transform.rotation = quat;
        }
    }
}
