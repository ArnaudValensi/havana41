using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LineCounter : MonoBehaviour {

    public static LineCounter Instance;

    [SerializeField] TextMeshProUGUI _text;

    int _internalScore = 0;

    private void Awake()
    {
        Instance = this;
        _internalScore = 0;
        UpdateUI();
    }

    void UpdateUI()
    {
        _text.text = _internalScore.ToString();
    }

    internal void AddLine(int v)
    {
        _internalScore += v;
        UpdateUI();
    }

}
