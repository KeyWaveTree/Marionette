using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Trigger : MonoBehaviour
{
    public GameObject[] monster;
    public Transform spawn;

    private bool spawned;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!spawned)
            {
                for (int i = 0; i < 7; i++)
                {
                    int random = Random.Range(0, 2);
                    float speed = Random.Range(1f, 4f);
                    GameObject go = Instantiate(monster[random]);
                    go.GetComponent<Monster>().speed = speed;
                    if (random == 0)
                    {
                        go.GetComponent<Monster>().type = Monster.MonsterType.A;
                        go.transform.position =
                            spawn.position + new Vector3(Random.Range(-15f, 15f), Random.Range(5f, 12f), 0);
                    }
                    else if (random == 1)
                    {
                        go.GetComponent<Monster>().type = Monster.MonsterType.B;
                        go.transform.position = spawn.position + new Vector3(Random.Range(-13f, 20f), 0f, 0f);
                    }
                  
                }

                spawned = true;
            }
        }
    }
}
