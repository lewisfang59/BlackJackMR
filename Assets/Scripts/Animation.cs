using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    private Animator anim;
    private GameObject btnHit, btnPass;
    // Start is called before the first frame update

    /// <summary>
    /// method for initilizing all animations
    /// </summary>
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// method for updating animations for based on each action
    /// </summary>
    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// method for updating animations for each event based on button press
    /// </summary>
    public void onBtnHitPress()
    {
        Debug.Log("TEST");
        anim.SetTrigger("test");
        anim.SetTrigger("test2");

    }
}
