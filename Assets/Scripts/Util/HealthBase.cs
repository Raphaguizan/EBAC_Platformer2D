using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class HealthBase : MonoBehaviour
{
    public int lifePoints = 10;

    [Header("Death Animation")]
    public float animDuration = 1f;
    public float animY = .3f;
    public float animX = 1.3f;
    [Header("Sound")]
    public AudioSource sound;

    private int _curretnLife;
    private bool _isDead = false;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        _curretnLife = lifePoints;
        _isDead = false;
    }

    public void Damage(int damage)
    {
        _curretnLife -= damage;

        if (_curretnLife <= 0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        var sptRenderer = GetComponent<SpriteRenderer>();
        if (DOTween.IsTweening(sptRenderer)) DOTween.KillAll();

        _isDead = true;

        transform.DOScaleY(animY, animDuration);
        transform.DOScaleX(animX, animDuration);
        sptRenderer.DOBlendableColor(Color.black, animDuration);

        // poderia usar um script de movimentação base para generalizar essa parte para player e inimigos
        var player = GetComponent<PlayerMovement>();
        if(player != null)
        {
            player.canMove = false;
        }

        if (sound) sound.Play();

        Destroy(gameObject, animDuration);
    }
}
