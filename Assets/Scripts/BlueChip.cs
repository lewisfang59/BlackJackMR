using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueChip : MonoBehaviour
{
    private Chips chip;
    [SerializeField]
    private GameObject blueChip;
    [SerializeField]
    private int chipValue = 1;

    public Chips chips
    {
        get { return this.chip; }
        set { this.chip = value; }  
    }

    Chips CreateChip()
    {
        chip = new Chips(1, blueChip);
        return chip;
    }

    // Start is called before the first frame update
    void Start()
    {
        chip = new Chips(1, blueChip);
        for (int i = 0; i < 6; i++)
        {
            GameObject gameChip = Instantiate(chip.Object, transform.position, transform.rotation);
            gameChip.AddComponent<BoxCollider>();
            gameChip.AddComponent<Rigidbody>();
            gameChip.AddComponent<NearInteractionGrabbable>();
            gameChip.AddComponent<ManipulationHandler>();
            gameChip.AddComponent<FingerCursor>();
        }
        //Instantiate(blueChip, transform.position, transform.rotation);
    }

    //Update is called once per frame
    //void Update()
    //{
        

    //}
}
