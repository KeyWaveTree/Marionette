﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonScript : MonoBehaviour
{
    Player PJ;
    // Start is called before the first frame update
    void Start()
    {
        PJ = GameObject.Find("Player").GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

//    public void Jump()
//    {
//        if(PJ.isGround)
//          PJ.bJump = true;
//    }
}
