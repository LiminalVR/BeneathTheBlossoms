using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuitBehavior : SteeringBehavior
{
    [SerializeField] private string mPursuitFilter;
    [SerializeField] private float mPursuitDistance;

    public override Vector3 Calculate(Agent agent)
    {
        Vector3 force = Vector3.zero;

        var agents = AIWorld.Instance.GetAgents();
        foreach (var agt in agents)
        {
            if (agt != agent // not compare to itself
                && Vector3.Distance(agt.transform.position, agent.transform.position) < mPursuitDistance // agent in range
                && agt.CompareTag(mPursuitFilter)) // agent is the type   
            {
                force += Vector3.Normalize(agt.transform.position - agent.transform.position) * agent.maxSpeed;
            }
        }

        if (force == Vector3.zero)
        {
            return Vector3.zero;
        }
        return (force - agent.velocity) * mWeight;
    }

}
