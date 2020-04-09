using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;

public class SliderEventHandler : MonoBehaviour
{
    /// <summary>
    /// method for slider label updater
    /// </summary>
    public void SliderLabelUpdater()
    {
        TextMeshPro amountLabel = GetComponentInChildren<TextMeshPro>();
        PinchSlider slider = GetComponentInChildren<PinchSlider>();

        double betValue = System.Math.Floor(slider.SliderValue * 100);

        if(betValue % 5 == 0)
        {
            amountLabel.text = System.Math.Floor(slider.SliderValue * 100).ToString();
        }
    }
}
