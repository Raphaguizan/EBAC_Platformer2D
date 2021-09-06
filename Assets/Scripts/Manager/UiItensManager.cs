using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Game.Util;

public class UiItensManager : Singleton<UiItensManager>
{
    public TextMeshProUGUI text;
    public GunBase gun;

    private void OnEnable()
    {
        gun.ShotCallBack += UpdateInterface;
    }

    private void OnDisable()
    {
        gun.ShotCallBack -= UpdateInterface;
    }

    public void AddArrow(int amount)
    {
        gun.AddBullets(amount);
        UpdateInterface();
    }

    private void UpdateInterface()
    {
        text.text = "x " + gun.BulletsAmount.ToString("00");
    }
}
