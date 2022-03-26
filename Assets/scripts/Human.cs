using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class Human : MonoBehaviour
{
    [SerializeField] private GameObject _route = null;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private int _delayPoint = 5;

    private Animator _humanWalk = null;
    private Transform[] _points = null;
    private Transform _target = null;
    private SpriteRenderer _humanRender = null;
    private int _currentPoint = FirstPoint;
    private bool _isMove = false;

    private const int FirstPoint = 1;
    private const string MoveAnimator = "MoveX";


    private void OnEnable()
    {
        _humanRender = GetComponent<SpriteRenderer>();
        _points = _route.GetComponentsInChildren<Transform>();
        _humanWalk = GetComponent<Animator>();
        StartCoroutine(Patrol(_delayPoint));
    }

    private void SetFlip()
    {
        if (_isMove == false)
        {
            _isMove = true;
            _humanWalk.SetFloat(MoveAnimator, (transform.position - _target.position).x);

            _humanRender.flipX = (transform.position.x - _target.position.x > 0);
        }
    }

    private void SetNextTargetPoint()
    {
        _humanWalk.SetFloat(MoveAnimator, 0);
        _currentPoint++;
        _isMove = false;

        if (_currentPoint >= _points.Length)
        {
            _currentPoint = FirstPoint;
        }
    }

    private void MoveToTargetPoint()
    {
        _target = _points[_currentPoint];
        transform.position = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
    }

    private IEnumerator Patrol(int delayPoint)
    {
        while (true)
        {
            var waitSeconds = new WaitForSeconds(delayPoint);
            MoveToTargetPoint();
            SetFlip();

            if (transform.position.x == _target.position.x)
            {
                SetNextTargetPoint();
                yield return waitSeconds;
            }

            yield return null;
        }
    }
}
