using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeBehavior : SteeringBehavior
{
    [SerializeField]private string mFleeFilter;
    [SerializeField]private float mPanicDistance;
    
    public override Vector3 Calculate(Agent agent)
    {
        // Flee behavior reference:
        // https://gamedevelopment.tutsplus.com/tutorials/understanding-steering-behaviors-flee-and-arrival--gamedev-1303#:~:text=The%20flee%20behavior%20makes%20the,create%20even%20more%20complex%20movements.

        Vector3 force = Vector3.zero;

        var agents = AIWorld.Instance.GetAgents();
        foreach (var agt in agents) 
        {
            if (agt != agent // not compare to itself
                && Vector3.Distance(agt.transform.position, agent.transform.position) < mPanicDistance // agent in range
                && agt.CompareTag(mFleeFilter)) // agent is the type   
            {
                force += Vector3.Normalize(agent.transform.position - agt.transform.position) * agent.maxSpeed;
            }
        }

        if (force == Vector3.zero) 
        {
            return Vector3.zero;
        }
        return (force - agent.velocity) * mWeight;
    }

}
