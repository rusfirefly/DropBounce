using UnityEngine;
using DG.Tweening;
using System;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public static event Action CollectedCoin;

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private PlayerMove _move;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private LayerMask _homeMask;
    [SerializeField] private LayerMask _enemyMask;
    [SerializeField] private LayerMask _coinMask;
    [SerializeField] private CoinPool _coinPool;
    [SerializeField] private Ease _effect;


    private float _yHomePosition;

    private void Start()
    {
        _yHomePosition = transform.position.y;
    }

    private void OnEnable()
    {
        _move.Drop += OnDrop;
    }
    private void OnDisable()
    {
        _move.Drop -= OnDrop;
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
            Invoke("Die",1.5f);
            //Effect
            Destroy(gameObject);
        }
    }

    private void Die()
    {
        Debug.Log("GAME OVER!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Coin")
        {
            _coinPool.ReturnObjecToPool(other.gameObject);
            CollectedCoin?.Invoke();
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
