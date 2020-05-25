using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Poison : MonoBehaviour
{
    private float rotationSpeed = 5;
    public float damage = 0.5f;
    public GameObject poisonEffect;
    private PlagueDoctor plagueDoctor;
    private Player player;
    public float speed;
    private bool ground;
    private Rigidbody2D rigid2D;
    private float forceDuration;
    
  
    private bool death;
    private float zRot;
    
    public enum PoisonType
    {
        Boss, Obstacle
    }

    public PoisonType type;
    private void Start()
    {
        plagueDoctor = FindObjectOfType<PlagueDoctor>();
        player = FindObjectOfType<Player>();
        rigid2D = GetComponent<Rigidbody2D>();
        zRot = Random.Range(-60f, 60f);
    }

    private void Update()
    {
        switch (type)
        {
            case PoisonType.Boss:
                death = plagueDoctor.death;
                if(death)
                    Death();
                if(forceDuration <=1f)
                    rigid2D.AddForce(new Vector2(-1.5f,3f) * speed);
                break;
            case PoisonType.Obstacle:
                rigid2D.AddForce(Vector2.down * speed);
                break;
        }
        if (!ground)
        {
            transform.Rotate(new Vector3(0f,0f,zRot) * rotationSpeed * Time.deltaTime); 
        }

        rotationSpeed++;
        if (rotationSpeed > 10f)
            rotationSpeed = 10f;
        
        forceDuration += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            GameObject go =Instantiate(poisonEffect);
            go.transform.position = transform.position;
            Destroy(go,1.5f);
            Destroy(gameObject,.5f);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            player.TakeDamage(damage);
            GameObject go =Instantiate(poisonEffect);
            go.transform.position = transform.position;
            Destroy(go,1.5f);
            Destroy(gameObject,0.5f);
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
