using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderBehavior : SteeringBehavior
{
    [SerializeField] private float mWanderDistance;
    [SerializeField] private float mWanderRadius;
    [SerializeField] private float mWanderJitter;
    private float mWanderAngle;

    private void Awake()
    {
        mWanderAngle = Random.Range(-Mathf.PI * 2, Mathf.PI * 2);
    }

    // Steering Behavior Reference: 
    // https://gamedevelopment.tutsplus.com/tutorials/understanding-steering-behaviors-wander--gamedev-1624
    public override Vector3 Calculate(Agent agent)
    {
        // Calculate the circle center
        Vector3 circleCenter;
        if (Vector3.SqrMagnitude(agent.velocity) == 0.0f)
        {
            return Vector3.zero;
        }
        circleCenter = Vector3.Normalize(agent.velocity);
        circleCenter *= mWanderDistance;

        // Calculate the displacement force
        Vector3 displacement = new Vector3(0.0f, -1.0f);
        displacement *= mWanderRadius;

        // Randomly change direction by making it change its current angle
        float len = Vector3.Magnitude(displacement);
        displacement.x = Mathf.Cos(mWanderAngle) * len;
        displacement.y = Mathf.Sin(mWanderAngle) * len;

        // Change wanderAngle a bit
        mWanderAngle += Random.Range(-mWanderJitter, mWanderJitter);

        // Calculate and return the wander force
        Vector3 wanderForce = circleCenter + displacement;
        return wanderForce;
    }
}
