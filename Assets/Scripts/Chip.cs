using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip : MonoBehaviour
{
    private GameObject chipPrefab, gameChips;
    private int chipValue;
    public int Point { get { return this.chipValue; } }
    public GameObject ChipPrefab { get { return this.chipPrefab; } }

    public Chip(GameObject chipPrefab)
    {
        this.chipPrefab = chipPrefab;
        string name = chipPrefab.name;
        int point = 0;
        switch (name)
        {
            case "bluechip":
                point = 5;
                break;
            case "greenchip":
                point = 10;
                break;
        }
        this.chipValue = point;
    }
    public void BetChips(int betAmount, GameObject chipPrefab)
    {
        int numChips = betAmount / 5;
        for (int i = 0; i <= numChips; i++)
        {
            gameChips = Instantiate(chipPrefab, transform.position, transform.rotation);
            gameChips.AddComponent<BoxCollider>();
            gameChips.AddComponent<Rigidbody>();
        }
        
    }

    public void RoundResult(int betAmount, bool roundResult, GameObject chipPrefab)
    {
        int numChips = betAmount / 5;
        if (roundResult)
        {
            for (int i = 0; i <= numChips; i++)
            {
                gameChips = Instantiate(chipPrefab, transform.position, transform.rotation);
                gameChips.AddComponent<BoxCollider>();
                gameChips.AddComponent<Rigidbody>();
            }
        }
        else
        {
            Debug.Log("attempting to destroy");
            //Destroy(gameObject);
            Destroy(this);
        }
    }
    void Start()
    {
        BetChips(100, Resources.Load<GameObject>("ChipsPrefab/bluechip"));
    }
    void Update()
    {
        RoundResult(100, false, Resources.Load<GameObject>("ChipsPrefab/bluechip"));
    }
}
