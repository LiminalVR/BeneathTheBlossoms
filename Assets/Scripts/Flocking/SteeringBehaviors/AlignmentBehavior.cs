using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignmentBehavior : SteeringBehavior
{
    public override Vector3 Calculate(Agent agent)
    {
        // Group behavior reference
        // https://gamedevelopment.tutsplus.com/tutorials/3-simple-rules-of-flocking-behaviors-alignment-cohesion-and-separation--gamedev-3444

        Vector3 force = agent.transform.forward;
        foreach (var neighbor in agent.neighbourhood) 
        {
            force += neighbor.transform.forward;
        }
        force /= agent.neighbourhood.Count + 1;
        return ((Vector3.Normalize(force) * agent.maxSpeed) - agent.velocity) * mWeight;
    }
}
