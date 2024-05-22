using System;
using UnityEngine;
using Random = System.Random;

public class Spawn : MonoBehaviour
{
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private CoinPool _coinPool;
    [SerializeField] private Transform _positionStart;
    [SerializeField] private Transform _positionEnd;
    [SerializeField] private float _spawnTime;

    private Enemy _enemy;
    private Coin _coin;

    private float _currentTime;
    private float _timeCoin;
    private float _currntTimeCoin;
    private bool _isCoin;
    private int _numberCoin;

    private Random _randomCoin;
    private int _nextCoin;
    private const float _yOffsetCoin = -1.067f;

    public void Initialize()
    {
        _randomCoin = new Random();
        GetObject();
        _nextCoin = _randomCoin.Next(1, 4);
    }

    public void Update()
    {
        _currentTime += Time.deltaTime;

        if (_isCoin == false)
        {
            if (_currentTime >= _spawnTime / 2)
            {
                if ((_numberCoin % _nextCoin) == 0)
                {
                    GetCoin();
                    _nextCoin = _randomCoin.Next(1, 4);
                    _numberCoin = 0;
                }

                _isCoin = true;
                _numberCoin++;
            }
        }

        if (_currentTime>=_spawnTime)
        {
            GetObject();
            _isCoin = false;
            _currentTime -= _spawnTime;
            _spawnTime = _randomCoin.Next(1, 3);
        }


    }

    private void OnValidate()
    {
        Enemy.MoveComplete += OnMoveComplete;
        Coin.MoveComplete += OnMoveCompleteCoin;
    }

    private void OnDisable()
    {
        Enemy.MoveComplete -= OnMoveComplete;
        Coin.MoveComplete -= OnMoveCompleteCoin;
    }

    private void OnMoveCompleteCoin(GameObject obj)
    {
        _coinPool.ReturnObjecToPool(obj);
    }

    private void OnMoveComplete(GameObject obj)
    {
        _enemyPool.ReturnObjecToPool(obj);
    }

    private void GetObject()
    {
        Quaternion quaternion = Quaternion.Euler(-90, 0, 0);
        _enemy = _enemyPool.GetObjectFromPool(_positionStart.position, quaternion).GetComponent<Enemy>();
        _enemy.Inizialize(_positionEnd);
    }

    private void GetCoin()
    {
        Vector3 position = _positionStart.position;
        position.y = _yOffsetCoin;
        _coin = _coinPool.GetObjectFromPool(position, Quaternion.identity).GetComponent<Coin>();
        _coin.Inizialize(_positionEnd);
    }
}
