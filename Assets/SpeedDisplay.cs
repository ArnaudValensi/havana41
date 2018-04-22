using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedDisplay : MonoBehaviour {

    [SerializeField] TextMeshProUGUI _text;

    [SerializeField]  float Min = 0.8f;
    [SerializeField]  float Max = 0f;

    private void Update()
    {
        var t = (int)Mathf.Abs((((HavanaManager.Instance.GlobalTransitionInterval - Max) / (Min - Max))  *100) - 100)+1;
        _text.text = t.ToString();
    }
}
