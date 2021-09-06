using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Flash : MonoBehaviour
{
    public Color flashColor, initialColor;
    public float flashTime = .2f;
    [SerializeField] private SpriteRenderer[] _sprites;
    private void OnValidate()
    {
        _sprites = transform.GetComponentsInChildren<SpriteRenderer>();
    }
    public void StartFlash()
    {
        if (DOTween.IsTweening(_sprites[0]))
        {
            DOTween.KillAll();
        }

        foreach (var s in _sprites)
        {
            s.color = initialColor;
            s.DOColor(flashColor, flashTime).SetLoops(2, LoopType.Yoyo);
        }
    }
}
