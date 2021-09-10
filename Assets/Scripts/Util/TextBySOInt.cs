using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBySOInt : MonoBehaviour
{
    public SOInt number;
    public string beforeText;

    private TextMeshProUGUI _text;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        UpdateInterface();
    }

    public void UpdateInterface()
    {
        if (_text)
        {
            _text.text = beforeText + number.value;
        }
    }
}
