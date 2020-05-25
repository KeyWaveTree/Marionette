using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //이동
    private joysticktype _joystick2;
    private Animator playerAnimator;
    private int direction;
    public float speed = 10f;

    public bool stunned;

    //공격
    ButtonEvent button;
    bool bAttack = true;
    float fTime = 0.0f;
    private float attackDuration;
    public float AttSpeed = 0.2f;

    //점프
    public bool isGround; //바닥에 붙어있는지 판별 public으로 되어 있는 이유는 실핼중에 bool값을 체크하기 위해
    private Rigidbody2D rigid2D;
    private int heartCount;
    private float stunDuration;
    public GameObject gameOver;
    public Transform bulletStartPoint;
    public GameObject bucketBullet;
    public GameObject plagueDoctorBullet;
    public Image[] heart;

    //머리

    public enum Head
    {
        PlagueDoctor,
        Bucket
    }

    public Head head;

    private void Start()
    {
        _joystick2 = FindObjectOfType<joysticktype>();
        rigid2D = GetComponent<Rigidbody2D>();
        heartCount = 0;
        playerAnimator = transform.GetChild(0).GetComponent<Animator>();
        button = GameObject.Find("Attack").GetComponent<ButtonEvent>();
        direction = 1;
        head = Head.Bucket;
    }

    private void Update()
    {
        if (heart[3].fillAmount <= 0f)
        {
            Death();
        }
    }

    private void FixedUpdate()
    {
        //이동
        if (!stunned)
            Move();
        // 공격
        Attack();
        // 공격 딜레이
        delay();

        if (stunned)
        {
            Stun();
        }
    }

    private void Death()
    {
        gameOver.SetActive(true);
    }

    public void TakeDamage(float damage)
    {
        StartCoroutine(heartMotion(damage));
        playerAnimator.SetTrigger("Damage");
        Debug.Log(heartCount);
    }

    private void Stun()
    {
        if (stunDuration >= 0.5f)
        {
            stunDuration = 0f;
            stunned = false;
        }

        stunDuration += Time.deltaTime;
    }

    IEnumerator heartMotion(float damage)
    {
        var curHealth = heart[heartCount].fillAmount - damage;
        for (var i = 0f; i <= 1f; i += 0.5f)
        {
            heart[heartCount].fillAmount = Mathf.Lerp(heart[heartCount].fillAmount, curHealth, i);
            yield return null;
        }

        if (heart[heartCount].fillAmount <= 0f)
        {
            if (heartCount == 3)
                heartCount = 3;
            else
            {
                heartCount++;
            }
        }
    }

    //이동
    private void Move()
    {
        //이동
        transform.Translate(new Vector2(
            _joystick2.Horizontal * speed * Time.deltaTime + Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0));

        //애니메이션(추후 수정)
        if (_joystick2.Horizontal > 0 || Input.GetAxis("Horizontal") > 0) //Right
        {
            transform.localScale = new Vector2(1.5f, 1.5f);
            direction = 1;
            playerAnimator.SetBool("Run", true);
        }
        else if (_joystick2.Horizontal < 0 || Input.GetAxis("Horizontal") < 0) //Left
        {
            transform.localScale = new Vector2(-1.5f, 1.5f);
            direction = 2;
            playerAnimator.SetBool("Run", true);
        }
        else
        {
            playerAnimator.SetBool("Run", false);
        }
    }

    //공격 속도
    private void delay()
    {
        if (!bAttack)
        {
            fTime += Time.deltaTime;
            if (fTime > AttSpeed)
            {
                bAttack = true;
                fTime = 0;
            }
        }
    }

    //공격
    private void Attack()
    {
        if (button.bPressed || Input.GetKey(KeyCode.LeftControl))
        {
            attackDuration += Time.deltaTime;
            if (bAttack)
            {
                playerAnimator.SetBool("Attack", true);
                if (attackDuration >= 0.1f)
                {
                    switch (head)
                    {
                        case Head.PlagueDoctor:
                            GameObject go = Instantiate(plagueDoctorBullet, bulletStartPoint.position,
                                transform.rotation);
                                go.GetComponent<Bullet>().direction = direction;
    
                            break;
                        case Head.Bucket:
                            GameObject go2 = Instantiate(bucketBullet, bulletStartPoint.position, transform.rotation);
                            go2.GetComponent<Bullet>().direction = direction;

                            break;
                    }
                    attackDuration = 0f;
                }

                bAttack = false;
            }
        }
        else
        {
            playerAnimator.SetBool("Attack", false);
        }
    }

    public void Jump()
    {
        if (isGround || isGround && Input.GetKeyDown(KeyCode.Space))
        {
            rigid2D.velocity = Vector2.zero;
            rigid2D.velocity += new Vector2(0, 13.0f);
            playerAnimator.SetBool("Jump", true);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            this.isGround = true;
        }
    }

    //땅에서 탈출한 시점에 실행됨
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            this.isGround = false;
        }
    }
}