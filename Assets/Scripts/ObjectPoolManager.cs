using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [Serializable]
    public class PooledObject 
    {
        public string name;
        public GameObject prefab;
        public int pooledSize;
    }

    public List<PooledObject> objectsToPool = new List<PooledObject>();
    private readonly Dictionary<string, List<GameObject>> _objectPoolByName = new Dictionary<string, List<GameObject>>();

    public bool IsInitialized { get { return _isInitialized; } }
    private bool _isInitialized;

    public void Start()
    {
        InitializePool();
        ServiceLocator.Register<ObjectPoolManager>(this);
    }

    private void InitializePool() 
    {
        foreach (PooledObject poolObj in objectsToPool) 
        {
            if (!_objectPoolByName.ContainsKey(poolObj.name))
            {
                GameObject poolGo = new GameObject(poolObj.name);
                poolGo.transform.SetParent(transform);
                _objectPoolByName.Add(poolObj.name, new List<GameObject>());
                for (int i = 0; i < poolObj.pooledSize; ++i)
                {
                    GameObject go = Instantiate(poolObj.prefab);
                    go.name = string.Format("{0}_{1:000}", poolObj.name, i);
                    go.transform.SetParent(poolGo.transform);
                    go.SetActive(false);
                    _objectPoolByName[poolObj.name].Add(go);
                }
            }
            else 
            {
                Debug.Log("Warning: Attempting to create multiple pools with same name.");
            }
        }
        _isInitialized = true;
    }

    public GameObject GetObjectFromPool(string poolName) 
    {
        GameObject ret = null;
        if (_objectPoolByName.ContainsKey(poolName))
        {
            ret = GetNextObject(poolName);
        }
        else 
        {
            Debug.LogError($"No Pool Exists With Name: {poolName}");
        }
        return ret;
    }

    private GameObject GetNextObject(string poolName) 
    {
        List<GameObject> pooledObjects = _objectPoolByName[poolName];
        foreach (GameObject go in pooledObjects)
        {
            if (go == null) 
            {
                Debug.LogError("Pooled Object is NULL");
                continue;
            }
            if (go.activeInHierarchy) 
            {
                continue;
            }
            else 
            {
                return go;
            }
        }
        Debug.Log("Object Pool Depleted. No Unused Object to return.");
        return null;
    }

    public void RecycleObject(GameObject go) 
    {
        go.SetActive(false);
    }
}
