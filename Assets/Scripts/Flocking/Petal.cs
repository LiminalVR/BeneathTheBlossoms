using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteeringModule))]
public class Petal : Agent
{
    private SteeringModule mSteeringModule;
    private void Start()
    {
        mSteeringModule = GetComponent<SteeringModule>();
    }

    private void Update()
    {
        var force = mSteeringModule.Calculate();
        var acceleration = force / mass;
        velocity += acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        transform.position += velocity * Time.deltaTime;
        if (Vector3.Magnitude(velocity) > 1.0f) 
        {
            transform.forward = Vector3.Normalize(velocity);
        }
    }
}
