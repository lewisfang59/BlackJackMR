using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chips : MonoBehaviour
{
    public enum ChipValue
    {
        one = 1,
        five = 5,
        twentyFive = 25,
        hundred = 100,
        fiveHundred = 500
    }

    //public enum ChipColor
    //{
    //    blue = 1,
    //    bluesky = 2,
    //    green = 3,
    //    orange = 4,
    //    pink = 5
    //}
   
    private ChipValue chipValue;
    //private ChipColor chipColor;
    private GameObject gameObject;

    public GameObject Object
    {
        get { return this.gameObject; }
        set { this.gameObject = value; }
    }

    public ChipValue Value
    {
        get { return this.chipValue; }
        set { this.chipValue = value; }
    }

    //public ChipColor Color
    //{
    //    get { return this.chipColor; }
    //    set { this.chipColor = value; }
    //}

    public Chips(int chipValue, GameObject obj)
    {
        //chipColor = 0;
        //obj.AddComponent<BoxCollider>();
        //obj.AddComponent<Rigidbody>();
        //obj.AddComponent<NearInteractionGrabbable>();
        //obj.AddComponent<ManipulationHandler>();
        this.chipValue = (ChipValue)chipValue;
        this.gameObject = obj;
    }
}
