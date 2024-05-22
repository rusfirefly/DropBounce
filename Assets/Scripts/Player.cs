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
    [SerializeField] private Ease _effect;
    [SerializeField] private LayerMask _coinMask;
    [SerializeField] private CoinPool _coinPool;

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
        //if ((collision.gameObject.layer & (1<<_groundMask)) == 0)
        if (CheckMask(collision.gameObject.layer, _groundMask))
        {
            transform.DOMoveY(_yHomePosition, 0.2f).SetEase(_effect);
            _rigidbody.isKinematic = true;
        }

        //if ((collision.gameObject.layer & (1 << _homeMask)) == 0)
        if (CheckMask(collision.gameObject.layer, _homeMask))
        {
            _move.IsDroped = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if ((other.gameObject.layer & (1 << _coinMask)) == 0)
        if(CheckMask(other.gameObject.layer, _coinMask))
        {
            _coinPool.ReturnObjecToPool(other.gameObject);
            CollectedCoin?.Invoke();
        }
    }

    private bool CheckMask(int onLayer, LayerMask mask)
    {
        if ((onLayer & (1 << _coinMask)) == 0)
        {
            return true;
        }
        return false;
    }

    private void OnDrop()
    {
        if(_rigidbody)
        {
            _rigidbody.isKinematic = false;
        }
    }
}
