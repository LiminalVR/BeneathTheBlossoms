using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CohesionBehavior : SteeringBehavior
{
    public override Vector3 Calculate(Agent agent)
    {
        // Group behavior reference
        // https://gamedevelopment.tutsplus.com/tutorials/3-simple-rules-of-flocking-behaviors-alignment-cohesion-and-separation--gamedev-3444

        Vector3 position = agent.transform.position;
        foreach (var neighbor in agent.neighbourhood) 
        {
            position += neighbor.transform.position;
        }
        if (agent.neighbourhood.Count >= 1)
        {
            position /= agent.neighbourhood.Count + 1;
            Vector3 desiredVelocity = Vector3.Normalize(position - agent.transform.position) * agent.maxSpeed;
            return (desiredVelocity - agent.velocity) * mWeight;
        }
        else 
        {
            return Vector3.zero;
        }

        
    }
}
