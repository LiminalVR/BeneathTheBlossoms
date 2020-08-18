using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

[RequireComponent(typeof(SteeringModule))]
public class Petal : Agent, IPointerEnterHandler, IPointerExitHandler
{
    private SteeringModule mSteeringModule;
    public Action<GameObject> OnDeath;
    [SerializeField] private float selfDestructionTime;
    private float currTimer;
    [SerializeField] private float gravityWeight;

    private void OnEnable()
    {
        velocity = Random.insideUnitSphere * maxSpeed;
        transform.rotation = Random.rotation;
        currTimer = selfDestructionTime;
    }

    private void Start()
    {
        mSteeringModule = GetComponent<SteeringModule>();
    }

    private void Update()
    {
        currTimer -= Time.deltaTime;
        if (currTimer < 0.0f) 
        {
            OnDeath.Invoke(gameObject);
            return;
        }
        var force = mSteeringModule.Calculate();
        force += Vector3.down * gravityWeight;
        var acceleration = force / mass;
        velocity += acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        transform.position += velocity * Time.deltaTime;
        if (Vector3.Magnitude(velocity) > 1.0f)
        {
            transform.forward = Vector3.Normalize(velocity);
        }
    }

    private void LateUpdate()
    {
        neighbourhood = AIWorld.Instance.GetNeighborhood(transform.position, neighbourhoodRadius);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ServiceLocator.Get<Game>().SetTarget(transform);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ServiceLocator.Get<Game>().UnsetTarget(transform);
    }
}
