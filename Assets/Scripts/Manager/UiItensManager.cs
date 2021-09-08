using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Game.Util;

public class UiItensManager : Singleton<UiItensManager>
{
    [Header("arrows")]
    public TextMeshProUGUI textArrows;
    public GunBase gun;
    public SOInt gunBullets;
    [Header("coins")]
    public TextMeshProUGUI textCoins;
    public SOInt coinAmount;

    private void OnEnable()
    {
        gun.ShotCallBack += UpdateInterfaceArrows;
        coinAmount.value = 0;
    }

    private void OnDisable()
    {
        gun.ShotCallBack -= UpdateInterfaceArrows;
    }

    #region arrow interface
    public void AddArrow(int amount)
    {
        gunBullets.value += amount;
        UpdateInterfaceArrows();
    }

    private void UpdateInterfaceArrows()
    {
        textArrows.text = "x " + gunBullets.value.ToString("00");
    }

    #endregion

    #region Coin Interface
    public static void AddCoin(int amount = 1)
    {
        Instance.coinAmount.value += amount;
        Instance.UpdateInterfaceCoins();
    }


    private void UpdateInterfaceCoins()
    {
        textCoins.text = "= " + coinAmount.value.ToString("00");
    }
    #endregion
}
