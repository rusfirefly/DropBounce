using UnityEngine;
using DG.Tweening;
using System;

public class PlayerMove : MonoBehaviour
{
    public event Action Drop;

    private float _xPosition;
    public bool IsDroped;

    void Update()
    {
        TouchInput();
    }
    
    private void TouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            _xPosition = touchPosition.x;
            if (IsDroped == false)
            {
                DropCube(touch.phase);
            }
        }
    }

    private void Move()
    {
        _xPosition = Mathf.Clamp(_xPosition, -1.245f, 1.323f);
        Vector3 position = transform.position;
        position.x = _xPosition;
        transform.DOMoveX(_xPosition, 0.05f).SetEase(Ease.InCirc);
    }

    public void DropCube(TouchPhase phase)
    {
        Move();
        if (phase == TouchPhase.Ended)
        {
            IsDroped = true;
            Drop?.Invoke();
        }
    }
}
