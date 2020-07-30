using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteeringModule))]
public class Petal : Agent
{
    private SteeringModule mSteeringModule;
    private void Start()
    {
        float min = -2.0f;
        float max = 2.0f;
        AIWorld.Instance.RegisterAgent(this);
        velocity = Random.insideUnitSphere * maxSpeed;
        transform.rotation = Random.rotation;
        transform.position = new Vector3(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));
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

    private void LateUpdate()
    {
        neighbourhood = AIWorld.Instance.GetNeighborhood(transform.position, neighbourhoodRadius);
    }

    private void OnDestroy()
    {
        AIWorld.Instance.UnregisterAgent(this);
    }
}
