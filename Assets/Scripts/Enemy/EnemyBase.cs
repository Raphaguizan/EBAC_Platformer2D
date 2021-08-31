using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBase : MonoBehaviour
{
    public Vector2 friction = new Vector2(.2f,0);

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
}
