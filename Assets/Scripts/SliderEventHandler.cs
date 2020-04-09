using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;

public class SliderEventHandler : MonoBehaviour
{
    /// <summary>
    /// Method to update slider label as user scroll the slider.
    /// </summary>
    public void SliderLabelUpdater()
    {
        int multiplier = 100;

        TextMeshPro amountLabel = GetComponentInChildren<TextMeshPro>();
        PinchSlider slider = GetComponentInChildren<PinchSlider>();

        double betValue = System.Math.Floor(slider.SliderValue * multiplier);

        if(betValue % 5 == 0)
        {
            amountLabel.text = System.Math.Floor(slider.SliderValue * multiplier).ToString();
        }
    }
}
