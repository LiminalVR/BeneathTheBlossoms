using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Agent))]
public class SteeringModule : MonoBehaviour
{
    List<SteeringBehavior> mBehaviors = new List<SteeringBehavior>();
    Agent mAgent;

    private void Start()
    {
        var behaviors = GetComponents<SteeringBehavior>();
        mBehaviors.AddRange(behaviors);
        mAgent = GetComponent<Agent>();
    }

    public Vector3 Calculate()
    {
        Vector3 total = Vector3.zero;
        foreach (var behavior in mBehaviors) 
        {
            total += behavior.Calculate(mAgent);
        }
        return total;
    }

}
