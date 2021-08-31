using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("movement params")]
    public float speed = 10f;
    public float runSpeed = 20f;

    public Vector2 friction = new Vector2(.1f, 0);

    [Header("Jump Params")]
    public float jumpForce = 200f;

    [Header("animations Params")]
    public Ease animationsEase = Ease.OutBack;
    [Space]
    public float JumpScaleY = 1.5f;
    public float JumpScaleX = .7f;
    public float JumoScaleDuration = .2f;
    [Space]
    public float LandingScaleY = .5f;
    public float LandingScaleX = .3f;
    public float LandingScaleDuration = .2f;

    private Rigidbody2D _myRigidbody;
    private float _currentSpeed;
    private bool _isJumping = false;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        _myRigidbody = GetComponent<Rigidbody2D>();
        _currentSpeed = speed;
        _isJumping = false;
    }

    private void Update()
    {
        HandleJump();
        HandleMovement();
        if (Input.GetKeyDown(KeyCode.A))
        {
            LandingAnimation();
        }
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _currentSpeed = runSpeed;
        }
        else
        {
            _currentSpeed = speed;
        }


        if (Input.GetKey(KeyCode.RightArrow))
        {
            _myRigidbody.velocity = new Vector2(_currentSpeed, _myRigidbody.velocity.y);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            _myRigidbody.velocity = new Vector2(-_currentSpeed, _myRigidbody.velocity.y);
        }


        if (_myRigidbody.velocity.x > 0)
        {
            _myRigidbody.velocity -= friction;
        }
        else if (_myRigidbody.velocity.x < 0)
        {
            _myRigidbody.velocity += friction;
        }
    }

    private void HandleJump()
    {
        if (_isJumping) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _myRigidbody.AddForce(Vector2.up * jumpForce * _myRigidbody.gravityScale);

            JumpAnimation();
        }
    }

    private void JumpAnimation()
    {
        _myRigidbody.transform.localScale = Vector2.one;
        DOTween.Kill(_myRigidbody.transform);

        _myRigidbody.transform.DOScaleY(JumpScaleY, JumoScaleDuration).SetLoops(2, LoopType.Yoyo).SetEase(animationsEase);
        _myRigidbody.transform.DOScaleX(JumpScaleX, JumoScaleDuration).SetLoops(2, LoopType.Yoyo).SetEase(animationsEase);
    }

    private void LandingAnimation()
    {
        if (DOTween.IsTweening(_myRigidbody.transform)) return;

        _myRigidbody.transform.DOPunchScale(new Vector3(LandingScaleX, LandingScaleY, 0), LandingScaleDuration, 5, .5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        LandingAnimation();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
            _isJumping = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
            _isJumping = true;
    }
}
