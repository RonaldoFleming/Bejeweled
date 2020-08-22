using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField]
    private List<PoolObjectConfiguration> _poolObjectsConfigurationList = default;

    private List<GameObject> _objectPool;

    [Serializable]
    private class PoolObjectConfiguration
    {
        public GameObject _objectToPool = null;
        public int _amountToPool = 0;
        public bool _expansible = false;
    }

    private void Start()
    {
        _objectPool = new List<GameObject>();

        foreach(PoolObjectConfiguration poolObjectConfiguration in _poolObjectsConfigurationList)
        {
            for(int i = 0; i < poolObjectConfiguration._amountToPool; i++)
            {
                GameObject go = Instantiate(poolObjectConfiguration._objectToPool);
                go.SetActive(false);
                _objectPool.Add(go);
            }
        }
    }

    public GameObject GetPooledObject(string tag)
    {
        foreach(GameObject pooledObject in _objectPool)
        {
            if(!pooledObject.activeInHierarchy && pooledObject.CompareTag(tag))
            {
                pooledObject.SetActive(true);
                return pooledObject;
            }
        }

        foreach(PoolObjectConfiguration poolObjectConfiguration in _poolObjectsConfigurationList)
        {
            if(poolObjectConfiguration._objectToPool.CompareTag(tag) && poolObjectConfiguration._expansible)
            {
                GameObject go = Instantiate(poolObjectConfiguration._objectToPool);
                _objectPool.Add(go);
                return go;
            }
        }

        return null;
    }
}
