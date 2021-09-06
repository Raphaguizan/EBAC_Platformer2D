using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public Vector2 direction;
    public float lifeTime;

    IEnumerator TimeToDestroy()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }

    void Update()
    {
        if(gameObject.activeInHierarchy)
            transform.Translate(direction * Time.deltaTime);
    }

    public void Initialize(Vector3 position, int side = 1)
    {
        gameObject.SetActive(true);

        direction.x = Mathf.Abs(direction.x) * side;
        if (side > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        transform.position = position;
        StartCoroutine(TimeToDestroy());
    }
}
