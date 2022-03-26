using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]

public class Cat : MonoBehaviour
{
    [SerializeField] private float _forceMoveX = 15;
    [SerializeField] private float _forceJump = 5;

    private Rigidbody2D _cat = null;
    private Animator _catRunWalk = null;
    private SpriteRenderer _catRender = null;
    private AudioSource[] _sounds = null;
    private AudioSource _meowSound = null;
    private AudioSource _eatSound = null;
    private Vector2 _speed;
    private bool isJumpReady = true;

    private const string MoveAnimator = "speed";
    private const string JumpTrigger = "isJump";
    private const string JumpAnimator = "jump";
    private const string AxisX = "Horizontal";

    private void FixedUpdate()
    {
        if (Input.GetAxis(AxisX) != 0)
        {
            MoveX();
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            Jump();
        }

        Animate();
    }

    private void Animate()
    {
        _speed = _cat.velocity;
        _catRunWalk.SetFloat(JumpAnimator, _speed.y);
        _catRunWalk.SetFloat(MoveAnimator, Mathf.Abs(_speed.x));
    }

    private void Jump()
    {
        if (isJumpReady)
        {
            isJumpReady = false;
            _catRunWalk.SetTrigger(JumpTrigger);
            _cat.AddForce(Vector2.up * _forceJump);
        }
    }

    private void Jump(float x, float y)
    {
        isJumpReady = false;
        _catRunWalk.SetTrigger(JumpTrigger);
        _cat.AddForce(new Vector2(x, y) * _forceJump);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Platform>(out Platform platform) && Input.GetKey(KeyCode.UpArrow) == false)
        {
            isJumpReady = true;
        }

        if (collision.gameObject.TryGetComponent<Sausage>(out Sausage sausage))
        {
            _eatSound.Play();
            sausage.Remove();
        }

        if (collision.gameObject.TryGetComponent<Human>(out Human human))
        {
            float bounceX = 0.6f;
            float bounceUpY = 1.1f;
            float bounceY = 0.1f;
            Vector2 contactNormal = collision.contacts[0].normal;
            _meowSound.Play();

            if (contactNormal.x > 0)
            {
                Jump(bounceX, bounceY);
            }
            else if (contactNormal.x < 0)
            {
                Jump(-bounceX, bounceY);
            }
            else if (contactNormal.y > 0)
            {
                Jump(0, bounceUpY);
            }
            else
            {
                Jump(0, -bounceY);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Platform>(out Platform platform))
        {
            isJumpReady = false;
        }
    }

    private void MoveX()
    {
        float error = 0.1f;
        _cat.AddForce(Vector2.right * _forceMoveX * Input.GetAxis(AxisX));

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
        _sounds = GetComponents<AudioSource>();
        _catRunWalk = GetComponent<Animator>();
        _cat = GetComponent<Rigidbody2D>();
        _catRender = GetComponent<SpriteRenderer>();
        Vector2 _speed = _cat.velocity;
        _meowSound = _sounds[1];
        _eatSound = _sounds[0];
    }
}
