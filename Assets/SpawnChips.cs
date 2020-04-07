using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChips : MonoBehaviour
{

    private GameObject chipsObject;
    private bool spawnChip = true;

    private void Awake()
    {
        var chip = Resources.Load("ChipsPrefab/blueChip");
        chipsObject = chip as GameObject;
    }

    private void Update()
    {
        while (spawnChip)
        {
            Spawn();
            spawnChip = false;
        }
        
    }

    private void Spawn()
    {
        Instantiate(chipsObject, transform.position, transform.rotation);
    }
}
