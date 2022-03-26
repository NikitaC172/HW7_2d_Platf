using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class CatAnimation : MonoBehaviour
{
    private Animator _catAnimator = null;
    private Rigidbody2D _catRigibody = null;
    private SpriteRenderer _catRender = null;
    private Vector2 _speed;

    private const string MoveAnimator = "speed";
    private const string JumpAnimator = "jump";

    private void Update()
    {
        float error = 0.1f;
        _speed = _catRigibody.velocity;
        _catAnimator.SetFloat(JumpAnimator, _speed.y);
        _catAnimator.SetFloat(MoveAnimator, Mathf.Abs(_speed.x));

        if (_speed.x > error && _catRender.flipX == true)
        {
            _catRender.flipX = false;
        }

        if (_speed.x < -error && _catRender.flipX == false)
        {
            _catRender.flipX = true;
        }
    }

    private void Awake()
    {
        _catRender = GetComponent<SpriteRenderer>();
        _catAnimator = GetComponent<Animator>();
        _catRigibody = GetComponent<Rigidbody2D>();
    }
}
