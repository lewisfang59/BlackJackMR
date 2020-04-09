using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    private Animator anim;
    private GameObject btnHit, btnPass;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onBtnHitPress()
    {
        Debug.Log("TEST");
        anim.SetTrigger("test");
        anim.SetTrigger("test2");

    }
}
