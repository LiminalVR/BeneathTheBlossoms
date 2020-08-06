using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWorld : MonoBehaviour
{
    // This is a Singleton
    public static AIWorld Instance { get; private set; } = null;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    private List<Agent> mAgents = new List<Agent>();

    public void RegisterAgent(Agent agent)
    {
        mAgents.Add(agent);
    }

    public void UnregisterAgent(Agent agent)
    {
        mAgents.Remove(agent);
    }

    public List<Agent> GetNeighborhood(Vector3 center, float range)
    {
        // TODO: optimize this function using quad tree or other technique
        List<Agent> neighbors = new List<Agent>();
        float radiusSqr = range * range;
        foreach (var agent in mAgents)
        {
            var distSqr = Vector3.Distance(agent.transform.position, center);
            if (distSqr == 0.0f) { continue; }
            if (distSqr < radiusSqr)
            {
                neighbors.Add(agent);
            }
        }
        return neighbors;
    }

    public List<Agent> GetAgents() 
    {
        return mAgents;
    }
}
