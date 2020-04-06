using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;

public class SliderEventHandler : MonoBehaviour
{
    

    public void test()
    {
        TextMeshPro amountLabel = GetComponentInChildren<TextMeshPro>();
        PinchSlider slider = GetComponentInChildren<PinchSlider>();

        amountLabel.text = System.Math.Floor(slider.SliderValue * 100).ToString();
    }
}
