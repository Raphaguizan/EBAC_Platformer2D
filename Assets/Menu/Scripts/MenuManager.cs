using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    public List<GameObject> buttons;
    public float delay = .1f, animationTime = .5f;
    public Ease ease = Ease.OutBack;
    private void OnEnable()
    {
        ShowButtons();
    }
    private void ShowButtons()
    {
        InitializeButtons();
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].transform.DOScale(1, animationTime).SetDelay(i*delay).SetEase(ease);
        }
    }
    private void InitializeButtons()
    {
        foreach (var item in buttons)
        {
            item.transform.localScale = Vector3.zero;
        }
    }

}
