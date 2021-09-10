using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableArrow : ItemCollectableBase
{
    [SerializeField] private int _arrowAmount;
    [SerializeField] private RandomSound sound;
    protected override void OnCollet()
    {
        if(sound)sound.PlayRandom();
        UiItensManager.Instance.AddArrow(_arrowAmount);
    }
}
