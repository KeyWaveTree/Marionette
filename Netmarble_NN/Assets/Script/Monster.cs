using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private Player player;
    [HideInInspector]
    public int hp = 10;
    private bool death;
    private float contactDamage = 0.5f;
    private Vector3 direction;
    private bool chase;
    private float time;
    public float speed;
    public GameObject deadEffect;
    
    public enum MonsterType
    {
        A,B
    }

    public MonsterType type;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        switch (type)
        {
            case MonsterType.A:
                transform.position += new Vector3(0,Mathf.Cos(time)/20,0);
                if (chase)
                {
                    direction = player.gameObject.transform.position - transform.position;
                    direction.Normalize();
                    transform.position += direction * speed * Time.deltaTime;
                }
                break;
            case MonsterType.B:
                if (chase)
                {
                    direction = player.gameObject.transform.position - transform.position;
                    direction.Normalize();
                    Vector3 xDir = new Vector3(direction.x, 0, 0);
                    transform.position += xDir * speed * Time.deltaTime;
                }
                break;
        }
        
        
        if (hp <= 0)
            Death();
    }

    private void Death()
    {
        GameObject go = Instantiate(deadEffect, transform.position,Quaternion.identity);
        Destroy(go,1f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.TakeDamage(contactDamage);
            Death();
        }

        if (other.gameObject.CompareTag("Boundary"))
        {
            chase = true;
        }

        if (other.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1);
        }
    }
    
    private void TakeDamage(int damage)
    {
        hp -= damage;
    }
}