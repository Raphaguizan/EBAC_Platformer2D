using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    #region Params
    public SOPlayerMovementSetup soPlayerMovement;
    public ParticleSystem walkParticleSystem;

    [HideInInspector]
    public bool canMove = true;

    private Rigidbody2D _myRigidbody;
    private float _currentSpeed;
    private bool _isJumping = false;

    private Animator _currentAnimator;

    #endregion

    #region core

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        _myRigidbody = GetComponent<Rigidbody2D>();
        _currentAnimator = Instantiate(soPlayerMovement.myAnimator, transform);
        _currentSpeed = soPlayerMovement.speed;
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
            _currentSpeed = soPlayerMovement.runSpeed;
        }
        else
        {
            _currentSpeed = soPlayerMovement.speed;
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
            _currentAnimator.SetBool(soPlayerMovement.walkBool, false);
            _currentAnimator.SetBool(soPlayerMovement.runBool, false);
        }


        if (_myRigidbody.velocity.x > soPlayerMovement.frictionBounder)
        {
            _myRigidbody.velocity -= soPlayerMovement.friction;
        }
        else if (_myRigidbody.velocity.x < -soPlayerMovement.frictionBounder)
        {
            _myRigidbody.velocity += soPlayerMovement.friction;
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
            _myRigidbody.transform.DOScaleX(side, soPlayerMovement.flipTime);
        }
        if (_currentSpeed == soPlayerMovement.speed)
        {
            _currentAnimator.SetBool(soPlayerMovement.walkBool, true);
            _currentAnimator.SetBool(soPlayerMovement.runBool, false);
        }
        else if (_currentSpeed == soPlayerMovement.runSpeed)
        {
            _currentAnimator.SetBool(soPlayerMovement.runBool, true);
            _currentAnimator.SetBool(soPlayerMovement.walkBool, false);
        }
    }
    #endregion

    #region Jump

    private void HandleJump()
    {
        if (_isJumping) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _myRigidbody.AddForce(Vector2.up * soPlayerMovement.jumpForce * _myRigidbody.gravityScale);
            _currentAnimator.SetTrigger(soPlayerMovement.jumpTrigger);
            JumpAnimation();
        }
    }

    private void JumpAnimation()
    {
        _myRigidbody.transform.localScale = Vector2.one;
        DOTween.Kill(_myRigidbody.transform);

        _myRigidbody.transform.DOScaleY(soPlayerMovement.jumpScaleY, soPlayerMovement.jumpScaleDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerMovement.animationsEase);
        _myRigidbody.transform.DOScaleX(soPlayerMovement.jumpScaleX, soPlayerMovement.jumpScaleDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerMovement.animationsEase);
    }
    #endregion

    #region Landing Controller
    private void LandingAnimation()
    {
        _currentAnimator.SetTrigger(soPlayerMovement.landTrigger);
        if (DOTween.IsTweening(_myRigidbody.transform)) return;

        _myRigidbody.transform.DOPunchScale(new Vector3(soPlayerMovement.landingScaleX, soPlayerMovement.landingScaleY, 0), soPlayerMovement.landingScaleDuration, 5, .5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        walkParticleSystem.Play();
        LandingAnimation();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        _isJumping = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        walkParticleSystem.Stop();
        StartCoroutine(JumpCoyoteTime(soPlayerMovement.coyoteTime));
    }

    IEnumerator JumpCoyoteTime(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        _isJumping = true;
    }
    #endregion
}
