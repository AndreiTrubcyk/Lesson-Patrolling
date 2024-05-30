using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PatrolBehaviour : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _speed;
    [SerializeField] private float _waitingTime;
    private Transform _nextPosition;
    private Transform _startPosition;
    private float _time;
    private int _counter;
    private float _temp;
    private bool _isAtPoint;

    private void Start()
    {
        _startPosition = _points[^1];
        _nextPosition = _points[_counter];
        _counter++;
    }

    private void Update()
    {
        if (!_isAtPoint)
        {
            Patrolling();
        }
        else
        {
            _temp += Time.deltaTime;
            if (_temp >= _waitingTime)
            {
                _isAtPoint = false;
                _temp = 0f;
            }
        }
    }

    private void Patrolling()
    {
        _time = Time.deltaTime * _speed;
        transform.LookAt(_nextPosition);
        var newTransform = Vector3.MoveTowards(transform.position, _nextPosition.position, _time);
        transform.localPosition = newTransform;
        if (transform.localPosition == _nextPosition.position)
        {
            _isAtPoint = true;
            _nextPosition = _points[_counter];
            if (_counter < _points.Length - 1)
            {
                _counter++;
            }
        }

        if (transform.localPosition == _startPosition.position)
        {
            _counter = 1;
            _nextPosition = _points[0];
        }
    }
}
