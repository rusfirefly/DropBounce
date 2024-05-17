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
    private float _time;
    private bool _isCoin;

    private Random _randomCoin;

    private void Start()
    {
        _randomCoin = new Random();
        GetObject();
        GetCoin();
    }

    public void Update()
    {
        _currentTime += Time.deltaTime;

        if (_isCoin == false)
        {
            if (_currentTime >= _spawnTime / 2)
            {
                GetCoin();
                _isCoin = true;
            }
        }

        if (_currentTime>=_spawnTime)
        {
            GetObject();
            _isCoin = false;
            _currentTime -= _spawnTime;
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
        _coin = _coinPool.GetObjectFromPool(_positionStart.position, Quaternion.identity).GetComponent<Coin>();
        _coin.Inizialize(_positionEnd);
    }
}
