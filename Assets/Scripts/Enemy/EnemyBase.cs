using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBase : MonoBehaviour
{
    [Header("Physics")]
    public Vector2 friction = new Vector2(.2f,0);
    public float fictionBounder = .2f;

    [Header("Damage")]
    public int damagePower = 5;

    private Rigidbody2D _myRigidbody;

    [Header("hit")]
    public string bulletTag = "Shot";

    public void Awake()
    {
        Init();
    }
    public void Init()
    {
        _myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (_myRigidbody.velocity.x > fictionBounder)
        {
            _myRigidbody.velocity -= friction;
        }
        else if (_myRigidbody.velocity.x < -fictionBounder)
        {
            _myRigidbody.velocity += friction;
        }
        else
        {
            _myRigidbody.velocity = new Vector3(0, _myRigidbody.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var coll = collision.gameObject.GetComponent<HealthBase>();
        if (coll != null)
        {
            coll.Damage(damagePower);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(bulletTag))
        {
            collision.gameObject.SetActive(false);

            var flash = GetComponent<Flash>();
            if (flash)
            {
                flash.StartFlash();
            }

            var health = GetComponent<HealthBase>();
            if (health)
            {
                health.Damage(collision.GetComponent<BulletBase>().Damage);
            }

            var hitSound = GetComponent<RandomSound>();
            if (hitSound)
            {
                hitSound.PlayRandom();
            }
        }
    }
}
