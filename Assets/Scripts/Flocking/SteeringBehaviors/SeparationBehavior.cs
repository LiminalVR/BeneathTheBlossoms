using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeparationBehavior : SteeringBehavior
{
    public override Vector3 Calculate(Agent agent)
    {
        // Group behavior reference
        // https://gamedevelopment.tutsplus.com/tutorials/3-simple-rules-of-flocking-behaviors-alignment-cohesion-and-separation--gamedev-3444
       
        Vector3 force = Vector3.zero;
        foreach (var neighbor in agent.neighbourhood) 
        {
            if (neighbor == agent) { continue; }
            Vector3 delta = agent.transform.position - neighbor.transform.position;
            float dist = Vector3.Magnitude(delta);
            if (dist != 0.0f) 
            {
                force += Vector3.Normalize(delta) / dist;
            }
        }
        if (Vector3.Magnitude(force) == 0.0f) { return Vector3.zero; }
        return ((Vector3.Normalize(force) * agent.maxSpeed) - agent.velocity) * mWeight;
    }
}
