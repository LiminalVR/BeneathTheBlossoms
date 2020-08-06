using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : Agent
{
    Vector3 force;
    void Start()
    {
        force = new Vector3(0, 0, 1);
        AIWorld.Instance.RegisterAgent(this);
    }

    void Update()
    {
        var acceleration = force / mass;
        velocity += acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        transform.position += velocity * Time.deltaTime;
    }

    private void OnDestroy()
    {
        AIWorld.Instance.UnregisterAgent(this);
    }
}
