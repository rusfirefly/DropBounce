using UnityEngine;
using DG.Tweening;
using System;

public class Enemy : MonoBehaviour
{
    public static event Action<GameObject> MoveComplete;
    [SerializeField] private Transform _endPosition;
    [SerializeField] private float _distance;
    private bool _isMove;
    private float _speed;
    
    public void Inizialize(Transform positionend)
    {
        _endPosition = positionend;
        _isMove = true;
        _speed = 0.1f;
    }

    private void Update()
    {
        if (_isMove == false) return;
        float distance = Vector3.Distance(transform.position, _endPosition.position);
        
        if (distance < _distance)
        {
            MoveComplete?.Invoke(gameObject);
            _isMove = false;
        }

        transform.Translate(Vector3.left * Time.deltaTime * (Mathf.Abs(_speed - distance)));
    }
}
