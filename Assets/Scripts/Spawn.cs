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
    private int _difficulty = 3;
    private int _spawnCoinNumber = 0;
    private int _difficultyCoin = 3;
    private bool _isDifficul;
    private int _upDificul = 7;
    private float _speed;
 
    public void Initialize()
    {
        _randomCoin = new Random();
        GetObject();
        _nextCoin = _randomCoin.Next(1, _difficultyCoin);
        _speed = _enemy.GetComponent<ISpeeded>().GetSpeed(); 
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
                    _nextCoin = _randomCoin.Next(1, _difficultyCoin);
                    _spawnCoinNumber++;
                    if(_spawnCoinNumber != 0 && (_spawnCoinNumber % _upDificul == 0))
                    {
                        _isDifficul = true;
                    }
                    _numberCoin = 0;
                }

                _isCoin = true;
                _numberCoin++;
            }
        }

        DifficulUp();

        if (_currentTime >= _spawnTime)
        {
            GetObject();
            _isCoin = false;
            _currentTime -= _spawnTime;
            _spawnTime = _randomCoin.Next(_difficulty, 5);
        }
    }


    private void DifficulUp()
    {
        if (_isDifficul)
        {
            if (_difficulty > 1)
            {
                _difficulty--;
                _difficultyCoin++;
                _speed -= 0.2f;
                _coin.GetComponent<ISpeeded>().SetSpeed(_speed);
                _enemy.GetComponent<ISpeeded>().SetSpeed(_speed);
            }
            _isDifficul = false;
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

        //_speed = _coin.GetComponent<ISpeeded>().GetSpeed();
    }
}
