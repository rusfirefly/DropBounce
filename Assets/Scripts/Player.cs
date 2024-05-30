using UnityEngine;
using DG.Tweening;
using System;
using YG;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public static event Action<int> CollectedCoin;
    public static event Action Die;

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private PlayerMove _move;
    [SerializeField] private CoinPool _coinPool;
    [SerializeField] private Ease _effect;

    private int _score;
    private float _yHomePosition;

    private void Start()
    {
        _yHomePosition = transform.position.y;
        _score = 0;
    }

    private void OnEnable()
    {
        _move.Drop += OnDrop;
        LevelHandler.NewGame += OnNewGame;
    }
    private void OnDisable()
    {
        _move.Drop -= OnDrop;
        LevelHandler.NewGame -= OnNewGame;
    }

    private void OnNewGame()
    {
        _score = 0;
        
    }

    private void OnValidate()
    {
        _rigidbody ??= GetComponent<Rigidbody>();    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            transform.DOMoveY(_yHomePosition, 0.2f).SetEase(_effect);
            _rigidbody.isKinematic = true;
        }

        if (collision.gameObject.tag == "Home")
        {
            _move.IsDroped = false;
        }

        if (collision.gameObject.tag == "Enemy")
        {
            //Invoke("Die",1.5f);
            OnDie();
            //Destroy(gameObject);
        }
    }

    private void OnDie()
    {
        Debug.Log("GAME OVER!");
        int bestScore = YandexGame.savesData.Score;

        if (_score > bestScore)
        {
            YandexGame.savesData.Score = _score;
            YandexGame.SaveProgress();
        }

        Die?.Invoke();
        Time.timeScale = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Coin")
        {
            _score++;
            _coinPool.ReturnObjecToPool(other.gameObject);
            CollectedCoin?.Invoke(_score);
        }
    }

    private void OnDrop()
    {
        if(_rigidbody)
        {
            _rigidbody.isKinematic = false;
        }
    }
}
