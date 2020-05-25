using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public GameObject bulletEffect;
    [HideInInspector] public int damage;
    private PlagueDoctor plagueDoctor;
    private TrashKing trashKing;
    [HideInInspector]public int direction;
    
    private enum BulletType
    {
        PlagueDoctor, Bucket
    }
    [SerializeField]
    private BulletType type;
    void Start()
    {
        switch (type)
        {
            case BulletType.PlagueDoctor:
                damage = 2;
                break;
            case BulletType.Bucket:
                damage = 1;
                break;
        }
        switch (Manager.instance.stage)
        {
            case Manager.SceneStage.TrashKing:
                trashKing = FindObjectOfType<TrashKing>();
                break;
            case Manager.SceneStage.PlagueDoctor:
                plagueDoctor = FindObjectOfType<PlagueDoctor>();
                break;
        }
        
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        if(direction == 1)
            transform.position += Vector3.right * speed * Time.deltaTime;
        else if (direction == 2)
            transform.position += Vector3.left * speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Boundary"))
        {
            GameObject go = Instantiate(bulletEffect);
            go.transform.position = transform.position + new Vector3(0.5f, 0f, 0f);
            Destroy(go,0.5f);
            Destroy(gameObject);
        
            if (other.gameObject.CompareTag("Boss"))
            {
                switch (Manager.instance.stage)
                {
                    case Manager.SceneStage.TrashKing:
                        trashKing.TakeDamage(damage);
                        break;
                    case Manager.SceneStage.PlagueDoctor:
                        plagueDoctor.TakeDamage(damage);
                        break;
                }
            }
        }
    }
}
