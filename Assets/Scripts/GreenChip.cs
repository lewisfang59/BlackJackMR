using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenChip : MonoBehaviour
{
    [SerializeField]
    private GameObject greenChip;
    [SerializeField]
    private int chipValue = 5;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject gameChip = Instantiate(greenChip, transform.position, transform.rotation);
            gameChip.AddComponent<BoxCollider>();
            gameChip.AddComponent<Rigidbody>();
            gameChip.AddComponent<NearInteractionGrabbable>();
            gameChip.AddComponent<ManipulationHandler>();
            gameChip.AddComponent<TouchHandler>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
