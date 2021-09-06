using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    #region Params
    [Header("movement params")]
    public float speed = 10f;
    public float runSpeed = 20f;

    public Vector2 friction = new Vector2(.1f, 0);
    public float frictionBounder = .2f;

    [Header("Jump Params")]
    public float jumpForce = 200f;
    public float coyoteTime = .1f;

    [Header("animations distortion Params")]
    public Ease animationsEase = Ease.OutBack;
    [Space]
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = .7f;
    public float jumpScaleDuration = .2f;
    [Space]
    public float landingScaleY = .5f;
    public float landingScaleX = .3f;
    public float landingScaleDuration = .2f;

    [Header("Animation Controller")]
    public Animator myAnimator;
    [SerializeField] private float flipTime = .1f;
    [SerializeField] private string _walkBool;
    [SerializeField] private string _runBool;
    [SerializeField] private string _jumpTrigger;
    [SerializeField] private string _landTrigger;


    [HideInInspector]
    public bool canMove = true;

    private Rigidbody2D _myRigidbody;
    private float _currentSpeed;
    private bool _isJumping = false;

    #endregion

    #region core

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        _myRigidbody = GetComponent<Rigidbody2D>();
        _currentSpeed = speed;
        _isJumping = false;
        canMove = true;
    }

    private void Update()
    {
        if (!canMove) return;
        HandleJump();
        HandleMovement();
    }
    #endregion

    #region Walk
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
            WalkAnimationController(1);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            _myRigidbody.velocity = new Vector2(-_currentSpeed, _myRigidbody.velocity.y);
            WalkAnimationController(-1);
        }
        else
        {
            myAnimator.SetBool(_walkBool, false);
            myAnimator.SetBool(_runBool, false);
        }


        if (_myRigidbody.velocity.x > frictionBounder)
        {
            _myRigidbody.velocity -= friction;
        }
        else if (_myRigidbody.velocity.x < -frictionBounder)
        {
            _myRigidbody.velocity += friction;
        }
        else
        {
            _myRigidbody.velocity = new Vector3(0, _myRigidbody.velocity.y);
        }
    }

    private void WalkAnimationController(int side)
    {
        if (_myRigidbody.transform.localScale.x != side)
        {
            _myRigidbody.transform.DOScaleX(side, flipTime);
        }
        if (_currentSpeed == speed)
        {
            myAnimator.SetBool(_walkBool, true);
            myAnimator.SetBool(_runBool, false);
        }
        else if (_currentSpeed == runSpeed)
        {
            myAnimator.SetBool(_runBool, true);
            myAnimator.SetBool(_walkBool, false);
        }
    }
    #endregion

    #region Jump

    private void HandleJump()
    {
        if (_isJumping) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _myRigidbody.AddForce(Vector2.up * jumpForce * _myRigidbody.gravityScale);
            myAnimator.SetTrigger(_jumpTrigger);
            JumpAnimation();
        }
    }

    private void JumpAnimation()
    {
        _myRigidbody.transform.localScale = Vector2.one;
        DOTween.Kill(_myRigidbody.transform);

        _myRigidbody.transform.DOScaleY(jumpScaleY, jumpScaleDuration).SetLoops(2, LoopType.Yoyo).SetEase(animationsEase);
        _myRigidbody.transform.DOScaleX(jumpScaleX, jumpScaleDuration).SetLoops(2, LoopType.Yoyo).SetEase(animationsEase);
    }
    #endregion

    #region Landing Controller
    private void LandingAnimation()
    {
        myAnimator.SetTrigger(_landTrigger);
        if (DOTween.IsTweening(_myRigidbody.transform)) return;

        _myRigidbody.transform.DOPunchScale(new Vector3(landingScaleX, landingScaleY, 0), landingScaleDuration, 5, .5f);
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
        StartCoroutine(JumpCoyoteTime(coyoteTime));
    }

    IEnumerator JumpCoyoteTime(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        _isJumping = true;
    }
    #endregion
}
