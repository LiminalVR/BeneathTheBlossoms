using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PetalSpawner : MonoBehaviour
{
    public string poolPetal = "Petal";

    public float totalSpawnTime;
    public AnimationCurve coolDownChange;
    public float maxCooldown;
    private float currTime = 0.0f;
    private float currCoolDown = 0.0f;

    private void Update()
    {
        currTime += Time.deltaTime;
        currCoolDown += Time.deltaTime;
        if (currTime < totalSpawnTime)
        {
            float cd = coolDownChange.Evaluate(currCoolDown / totalSpawnTime) * maxCooldown;
            if (currCoolDown > cd)
            {
                SpawnPetal();
                currCoolDown = 0.0f;
            }
        }
    }

    private void SpawnPetal()
    {
        GameObject petalGo = ServiceLocator.Get<ObjectPoolManager>().GetObjectFromPool(poolPetal);
        if (petalGo != null) 
        {
            var petal = petalGo.GetComponent<Petal>();
            petalGo.transform.position = transform.position;
            petal.OnDeath = Recycle;
            AIWorld.Instance.RegisterAgent(petal);
            petalGo.SetActive(true);
        }
    }

    private void Recycle(GameObject obj) 
    {
        ServiceLocator.Get<ObjectPoolManager>().RecycleObject(obj); 
        AIWorld.Instance.UnregisterAgent(obj.GetComponent<Agent>());
    }
}
