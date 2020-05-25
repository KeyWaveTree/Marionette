using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashDummy : MonoBehaviour
{
    private float hp = 15;
    public GameObject bomber;
    
    // Update is called once per frame
    void Update()
    {
        if(hp <= 0f)
            Death();
    }

    private void TakeDamage(float damage)
    {
        hp -= damage;
    }

    private void Death()
    {
        for (int i = 0; i < 10; i++)
        {
            Instantiate(bomber);
        }
        
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1f);
        }
    }
}
