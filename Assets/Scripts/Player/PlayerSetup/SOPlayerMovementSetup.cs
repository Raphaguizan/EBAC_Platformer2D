using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "Player_movement_Setup", menuName = "Game/Player/MovementSetup")]
public class SOPlayerMovementSetup : ScriptableObject
{
    public Animator myAnimator;

    [Header("movement params")]
    public float speed = 10f;
    public float runSpeed = 20f;

    public Vector2 friction = new Vector2(.8f, 0);
    public float frictionBounder = .4f;

    [Header("Jump Params")]
    public float jumpForce = 200f;
    public float coyoteTime = .06f;

    [Header("animations distortion Params")]
    public Ease animationsEase = Ease.OutBack;
    [Space]
    public float jumpScaleY = 1.1f;
    public float jumpScaleX = .8f;
    public float jumpScaleDuration = .2f;
    [Space]
    public float landingScaleY = -.3f;
    public float landingScaleX = .3f;
    public float landingScaleDuration = .2f;

    [Header("Animation Controller")]
    public float flipTime = .1f;
    public string walkBool = "walk";
    public string runBool = "run";
    public string jumpTrigger = "jump";
    public string landTrigger = "land";
}
