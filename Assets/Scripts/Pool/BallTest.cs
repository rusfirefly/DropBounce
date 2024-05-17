using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTest : MonoBehaviour
{
    [SerializeField] private PoolObject _pool;

    public void Start()
    {
        GetPool();
    }

    public void GetPool()
    {
        _pool = FindObjectOfType<PoolObject>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_pool)
        {

            _pool.ReturnObjecToPool(gameObject);
        }
    }
}
