using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Manager;

public class ItemCollectableCoin : ItemCollectableBase
{
    protected override void OnCollet()
    {
        UiItensManager.AddCoin();
        VFXManager.PlayVFX(VFXType.COIN, transform.position);
    }
}
