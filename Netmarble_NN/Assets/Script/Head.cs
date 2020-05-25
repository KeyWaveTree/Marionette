using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    private float time;
    private bool isGround;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(!isGround)
            transform.Rotate(new Vector3(0f,0f,45f),time);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }
}
