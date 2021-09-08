using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableArrow : ItemCollectableBase
{
    [SerializeField] private int _arrowAmount;
    protected override void OnCollet()
    {
        UiItensManager.Instance.AddArrow(_arrowAmount);
    }
}
