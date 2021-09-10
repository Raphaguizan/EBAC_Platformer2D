using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{
    public string playerTag = "Player";
    public float timeToDestroy = 3f;
    public GameObject Image;
    public Collider2D collisionBox;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(playerTag))
        {
            Collect();
        }
    }

    protected virtual void Collect()
    {
        OnCollet();
        Image.SetActive(false);
        collisionBox.enabled = false;
        Destroy(gameObject, timeToDestroy);
    }


    protected virtual void OnCollet(){}
}
