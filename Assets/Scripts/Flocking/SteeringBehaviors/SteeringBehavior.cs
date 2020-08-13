using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteeringBehavior : MonoBehaviour
{
    public abstract Vector3 Calculate(Agent agent);
    [SerializeField]
    protected float mWeight = 1.0f;
    [SerializeField]
    protected bool mActive = true;
}
