using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Manager;

public class ItemCollectableCoin : ItemCollectableBase
{
    public RandomSound playAudio;
    protected override void OnCollet()
    {
        UiItensManager.AddCoin();
        playAudio.PlayRandom();
        VFXManager.PlayVFX(VFXType.COIN, transform.position);
    }
}
