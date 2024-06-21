using UnityEngine;
using System;

public class Enemy : MonoBehaviour, ISpeeded
{
    public static event Action<GameObject> MoveComplete;
    [SerializeField] private Transform _endPosition;
    [SerializeField] private float _distance;
    private bool _isMove;
    [SerializeField] private float _speed;

    public float GetSpeed() => _speed;

    public void Inizialize(Transform positionend)
    {
        _endPosition = positionend;
        _isMove = true;
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

        transform.Translate(Vector3.left * Time.deltaTime * (Mathf.Abs(_speed - distance)) / 2);
    }
    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
}
