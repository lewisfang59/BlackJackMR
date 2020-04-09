using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip : MonoBehaviour
{
    private GameObject chipPrefab, gameChips;
    private int chipValue;
    public int Point { get { return this.chipValue; } }
    public GameObject ChipPrefab { get { return this.chipPrefab; } set { this.chipPrefab = value; } }

    ////// <summary>
    ////// future implemented consturctor for creating chips
    ////// </summary>
    ////// <param name="chipPrefab"></param>
    //public Chip(GameObject chipPrefab)
    //{
    //    this.chipPrefab = chipPrefab;
    //    string name = chipPrefab.name;
    //    int point = 0;
    //    switch (name)
    //    {
    //        case "bluechip":
    //            point = 5;
    //            break;
    //        case "greenchip":
    //            point = 10;
    //            break;
    //    }
    //    this.chipValue = point;
    //}

    /// <summary>
    /// baisc method for creating chip
    /// </summary>
    public Chip()
    {
        this.chipPrefab = null;
    }
}