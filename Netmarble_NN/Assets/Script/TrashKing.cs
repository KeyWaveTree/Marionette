using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashKing : MonoBehaviour
{
    public float trashKingHp = 700;
    public float rushDistance;
    public bool death;
    public float contactDamage = 0.5f;
    public GameObject head;
    public Image hpBar;
    public GameObject trash;
    public bool goTrash;
    private Player player;
    private bool hit;
    private bool rushHit;
    private bool stunning;
    private float basicAttackDuration;
    private float tripleAttackDuration;
    private float chargeDuration;
    private float basicAttackMotionTime;
    private float tripleAttackMotionTime;
    private bool isPlayTripleMotion;
    private bool isPlayBasicMotion;
   
    private float rushDuration;
    private bool invokeSpecialSkill;
    private float contactDuration;
    private float maxHp = 700;
    private Animator kingAnimator;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        kingAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        hit = true;
    }

    // Update is called once per frame
    void Update()
    {
        BasicAttack();
        RushAttack();
        TripleAttack();
        
        if (trashKingHp <= 300f && !invokeSpecialSkill)
            StartCoroutine(SpecialSkill());

        if (trashKingHp <= 0f)
            Death();
    }

    public void TakeDamage(float damage)
    {
        trashKingHp -= damage;
        hpBar.fillAmount -= damage / maxHp;
    }

    private IEnumerator SpecialSkill()
    {
        for (float i = 0; i <= 1f; i += 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, trash.transform.position, i);
            yield return null;
        }
        trashKingHp += 50f;
        invokeSpecialSkill = true;
        goTrash = true;
    }

    private IEnumerator RushToPlayer()
    {
        for (float i = 0; i <= 1f; i+= 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position, i);
            yield return null;
        }
    }
    

    private void BasicAttack()
    {
        if (!isPlayBasicMotion)
            basicAttackDuration += Time.deltaTime;

        if (basicAttackDuration >= 2f && !isPlayBasicMotion)
        {
            kingAnimator.SetBool("basicAttack", true);
            isPlayBasicMotion = true;
        }

        if (isPlayBasicMotion)
        {
            basicAttackMotionTime += Time.deltaTime;
            if (basicAttackMotionTime > 3f)
            {
                kingAnimator.SetBool("basicAttack", false);
                isPlayBasicMotion = false;
                basicAttackDuration = 0f;
            }
        }
    }

    private void RushAttack()
    {
        Vector2 playerToKing = transform.position - player.transform.position;
        float distance = playerToKing.magnitude;
        rushDuration += Time.deltaTime;
        if (distance <= rushDistance && rushDuration >= 5f)
        {
            rushHit = true;
            StartCoroutine(RushToPlayer());
            rushDuration = 0f;
        }
    }

    private void TripleAttack()
    {
        if(!isPlayTripleMotion)
            tripleAttackDuration += Time.deltaTime;
        
        if (tripleAttackDuration >= 7f)
        {
            chargeDuration += Time.deltaTime;
            if (chargeDuration >= 1f && !isPlayTripleMotion)
            {
                kingAnimator.SetBool("tripleAttack",true);
                isPlayTripleMotion = true;
            }
        }

        if (isPlayTripleMotion)
        {
            tripleAttackMotionTime += Time.deltaTime;
            if (tripleAttackMotionTime > 2f)
            {
                kingAnimator.SetBool("tripleAttack",false);
                isPlayTripleMotion = false;
                tripleAttackMotionTime = 0f;
            }
        }
    }

    private void Death()
    {
        Instantiate(head, transform.position, Quaternion.identity);
        death = true;
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // 그냥 닿았을 때
            if (!rushHit)
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (rushHit)
            {
                rushHit = false;
                player.stunned = true;
                player.TakeDamage(contactDamage *2);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            hit = true;
    }
}