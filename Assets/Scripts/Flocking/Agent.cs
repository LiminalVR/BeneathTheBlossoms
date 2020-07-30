using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public Vector3 velocity;
    public float neighbourhoodRadius;
    public float maxSpeed = 1.0f;
    public float mass = 1.0f;
    public List<Agent> neighbourhood = new List<Agent>();
}