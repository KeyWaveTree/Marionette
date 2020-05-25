using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour
{
    private Player player;
    private float hp = 5f;
    private float movingDuration;
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0f)
            Death();
        if (movingDuration > 1f)
            movingDuration = 0f;

        movingDuration += Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, player.gameObject.transform.position, movingDuration);
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    private void TakeDamage(float damage)
    {
        hp -= damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1f); 
        }

        if (other.gameObject.CompareTag("Player"))
        {
            player.TakeDamage(0.5f);
        }
    }
}
