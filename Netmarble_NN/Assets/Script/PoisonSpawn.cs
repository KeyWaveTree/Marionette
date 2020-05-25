using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSpawn : MonoBehaviour
{
    private Transform tr;
    private float createDuration;

    public GameObject poison;

    private void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    private void Update()
    {
        CreatePoison();
    }

    private void CreatePoison()
    {
        createDuration += Time.deltaTime;

        if (createDuration >= 0.5f)
        {
            GameObject go = Instantiate(poison);
            go.transform.position = tr.position + new Vector3(Random.Range(-10f, 10f), 0f, 0f);
            go.GetComponent<Poison>().type = Poison.PoisonType.Obstacle;
            go.GetComponent<Poison>().speed = Random.Range(1f, 5f);

            go.transform.parent = transform;


            createDuration = 0f;
        }
    }
}