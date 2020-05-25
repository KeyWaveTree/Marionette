using System.Collections;
using System.Collections.Generic;
using Anima2D;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] headSprites;
    public Sprite[] attackSprites;
    public Image headImg;
    public Image attackImg;
    public GameObject[] heads;

    private Player player;
    private int headNum;

    void Start()
    {
        headNum = 0;
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChangeHead()
    {
        headNum++;
        if (headNum >= headSprites.Length)
            headNum = 0;
        if (headNum == 0)
        {
            heads[0].SetActive(true);
            heads[1].SetActive(false);
            player.head = Player.Head.Bucket;
        }
        else if (headNum == 1)
        {
            heads[0].SetActive(false);
            heads[1].SetActive(true);
            player.head = Player.Head.PlagueDoctor;
        }
        headImg.sprite = headSprites[headNum];
        attackImg.sprite = attackSprites[headNum];
    }
}