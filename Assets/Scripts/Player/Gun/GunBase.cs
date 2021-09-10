using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public Transform player;
    public Transform gunPoint;
    public SOInt BulletsAmount;
    public float shotCooldown;
    public GameObject bulletPrefab;
    [Space]
    public KeyCode shotKey = KeyCode.A;
    public Action ShotCallBack;
    [Space]
    public RandomSound sound;

    private List<GameObject> _shotPoolingList = new List<GameObject>();
    private Coroutine _currentCoroutine;
    private bool _buttonIsPressed = false;

    private int _side = 1;

    private void OnEnable()
    {
        BulletsAmount.value = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(shotKey))
        {
            _buttonIsPressed = true;
            _currentCoroutine = StartCoroutine(ShotController());
        }
        else if (Input.GetKeyUp(shotKey))
        {
            _buttonIsPressed = false;
            StopCoroutine(_currentCoroutine);
        }
    }
    IEnumerator ShotController()
    {
        while (_buttonIsPressed)
        {
            Shot();
            yield return new WaitForSeconds(shotCooldown);
        }
    }

    private void Shot()
    {
        if (BulletsAmount.value <= 0) return;
        BulletsAmount.value--;

        ShotCallBack?.Invoke();
        if(sound) sound.PlayRandom();

        _side = 1;
        if (player.transform.localScale.x < 0) 
            _side = -1;

        foreach(var i in _shotPoolingList)
        {
            if (!i.activeInHierarchy)
            {
                i.GetComponent<BulletBase>().Initialize(gunPoint.transform.position, _side);
                return;
            }
        }
        var aux = Instantiate(bulletPrefab);
        aux.GetComponent<BulletBase>().Initialize(gunPoint.transform.position, _side);
        _shotPoolingList.Add(aux);
    }
}
