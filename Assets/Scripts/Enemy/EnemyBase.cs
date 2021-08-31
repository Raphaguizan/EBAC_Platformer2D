using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBase : MonoBehaviour
{
    [Header("Physics")]
    public Vector2 friction = new Vector2(.2f,0);

    [Header("Damage")]
    public int damagePower = 5;

    private Rigidbody2D _myRigidbody;

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
        if (_myRigidbody.velocity.x > 0)
        {
            _myRigidbody.velocity -= friction;
        }
        else if (_myRigidbody.velocity.x < 0)
        {
            _myRigidbody.velocity += friction;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var coll = collision.gameObject.GetComponent<HeathBase>();
        if (coll != null)
        {
            coll.Damage(damagePower);
        }
    }
}
