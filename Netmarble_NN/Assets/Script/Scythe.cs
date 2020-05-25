using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 10f;
    public float damage = 0.5f;

    private Player player;
    private PlagueDoctor plagueDoctor;
    private bool death;
    private Vector3 direction;
    void Start()
    {
        plagueDoctor = FindObjectOfType<PlagueDoctor>();
        player = FindObjectOfType<Player>();
        Destroy(gameObject, 3f);
        direction = player.gameObject.transform.position - transform.position;
        direction.Normalize();
        
    }

    void Update()
    {
        death = plagueDoctor.death;
        if(death)
            Death();
        transform.position += direction * speed * Time.deltaTime;
        transform.Rotate(new Vector3(0f,0f,45f) * rotationSpeed*Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
    private void Death()
    {
        Destroy(gameObject);
    }
}
