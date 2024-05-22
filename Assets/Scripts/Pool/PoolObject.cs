using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolObject : MonoBehaviour
{
    private protected GameObject _objectPrefab;

    private protected List<GameObject> _pool = new List<GameObject>();

    public void Initialize()
    {
        InitializePool();
    }

    protected virtual void InitializePool()
    {

    }

    public GameObject GetObjectFromPool(Vector3 position, Quaternion rotation)
    {
        foreach (GameObject gameObject in _pool)
        {
            if (!gameObject.activeInHierarchy)
            {
                gameObject.transform.position = position;
                gameObject.transform.rotation = rotation;
                gameObject.SetActive(true);

                return gameObject;
            }
        }

        GameObject newObject = Instantiate(_objectPrefab, position, rotation);
        _pool.Add(newObject);
        return newObject;
    }

    public void ReturnObjecToPool(GameObject gameObject)
    {
        GameObject obj = gameObject;
        obj.SetActive(false);
    }


}
