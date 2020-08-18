using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : Agent
{
    void Start()
    {
        AIWorld.Instance.RegisterAgent(this);
    }

    void Update()
    {
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        //transform.position += velocity * Time.deltaTime;
    }

    private void OnDestroy()
    {
        AIWorld.Instance.UnregisterAgent(this);
    }

    public void SetInitialVelocity(Vector3 velocity) 
    {
        this.velocity = velocity;
    }
}
