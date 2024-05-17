using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class TestPool : MonoBehaviour
{
    [SerializeField] private PoolObject _poolBall;

    private GameObject _ball;
    private Random _random;

    private float _currentTime;
    private float _firTime=0.4f;

    private void Start()
    {
        _random = new Random();
    }

    void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime > _firTime)
        {
            _ball = _poolBall.GetObjectFromPool(transform.position, Quaternion.identity);
            _currentTime -= _firTime;
        }
    }
}
