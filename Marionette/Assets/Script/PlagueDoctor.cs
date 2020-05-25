using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlagueDoctor : MonoBehaviour
{
   
    public float plagueDoctorHp = 5000;
    public Image hpBar;
    public Transform shootPoint;
    public GameObject scythePrefab;
    public GameObject poisonPrefab;
    public GameObject[] monsterPrefab;
    public GameObject head;
    public float contactDamage = 0.5f;
    
    public bool death;

    private Animator plagueAnimator;
    private float attackSpeed = 1f;
    private Player player;
    private float maxHp = 5000;
    private bool hit;
    private bool attackMotion;
    private float basicAttackDuration;
    private float poisonAttackDuration;
    private float summonDuration;
    private float contactDuration;
    private GameObject projectile;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        plagueAnimator = GetComponent<Animator>();
        projectile = GameObject.FindWithTag("Projectile");
    }

    void Start()
    {
        hit = true;
    }

    // Update is called once per frame
    void Update()
    {
        BasicAttack();
        PoisonAttack();
        SummonMonster();
        if (plagueDoctorHp <= 2500f)
            attackSpeed = 2;
        if(plagueDoctorHp <= 0f)
            Death();
    }

    public void TakeDamage(float damage)
    {
        plagueDoctorHp -= damage;
        hpBar.fillAmount -= damage/maxHp;
    }

    private void SummonMonster()
    {
        summonDuration += Time.deltaTime;
        Debug.Log("summon");
        if (summonDuration >= 14f / attackSpeed)
        {
            int random = Random.Range(0, 2);
            GameObject go = Instantiate(monsterPrefab[random]);
            
            if (random == 0)
                go.GetComponent<Monster>().type = Monster.MonsterType.A;
            else if (random == 1)
                go.GetComponent<Monster>().type = Monster.MonsterType.B;
            summonDuration = 0f;
        }
    }
    
    private void PoisonAttack()
    {
        poisonAttackDuration += Time.deltaTime;
        
        if (poisonAttackDuration >= 10f / attackSpeed)
        {
            CreatePoison();
            poisonAttackDuration = 0f;
           // plagueAnimator.SetBool("Attack",false);
        }
    }

    private void CreatePoison()
    {
        GameObject go = Instantiate(poisonPrefab);
        go.GetComponent<Poison>().type = Poison.PoisonType.Boss;
        go.GetComponent<Poison>().speed = Random.Range(3f, 6f);
        go.transform.parent = projectile.transform;
        go.transform.position = shootPoint.position;
    }

    private IEnumerator AttackMotion()
    {
        plagueAnimator.SetBool("Attack",true);
        
        yield return new WaitForSeconds(2f);
        basicAttackDuration = 0f;
        attackMotion = false;
    }
    private void BasicAttack()
    {
        if(!attackMotion)
            basicAttackDuration += Time.deltaTime;
        
        if (basicAttackDuration >= 2f / attackSpeed && !attackMotion)
        {
            attackMotion = true;
            StartCoroutine(AttackMotion());
        }
        
    }

    private void CreateScythe()
    {
        GameObject go = Instantiate(scythePrefab);
        go.transform.parent = projectile.transform;
        go.transform.position = shootPoint.position;
    }

    private void Death()
    {
        plagueAnimator.SetTrigger("Die");
        Instantiate(head, transform.position,Quaternion.identity);
        death = true;
        Destroy(gameObject);
    }
    

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            contactDuration += Time.deltaTime;
            if (contactDuration > 1f)
            {
                hit = true;
                contactDuration = 0f;
            }

            if (!hit)
                return;
            hit = false;
            player.TakeDamage(contactDamage);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            hit = true;
    }
}