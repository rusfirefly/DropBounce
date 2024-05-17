using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullPool : PoolObject
{
    [SerializeField] private GameObject _bullPrefab;
    [SerializeField] private int _poolSize;

    protected override void InitializePool()
    {
        _objectPrefab = _bullPrefab;

        for(int i = 0; i < _poolSize; i++)
        {
            GameObject gameObject = Instantiate(_bullPrefab, transform);
            gameObject.SetActive(false);
            _pool.Add(gameObject);
        }
    }

}
