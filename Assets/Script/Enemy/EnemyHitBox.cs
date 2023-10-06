using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyHitBox : MonoBehaviour
{
    private GreenSlime greenSlime;//그린슬라임 히트박스용으로 점점늘려줄필요있음 슬라임타입은3개
    private BlueSlime blueSlime;
    private RedSlime redSlime;

    public enum SlimeType
    {
        GreenSlime,
        RedSlime,
        BlueSlime,
    }
    [SerializeField] private SlimeType slimeType;

    private Player player;


    private bool PlayerCheck;
    private bool playerGreenSlimeHitCheck = false;
    private bool blueSlimeAttackCheck;
    private bool playerRedSlimeHitCheck = false;


    private bool oneHitCheck = false;


    private PolygonCollider2D poly2D;
    private BoxCollider2D box2d;//공격범위안에있는지체크용 블루슬라임용도
    private Animator m_anim2d;

    private void Start()
    {
        if (slimeType == SlimeType.GreenSlime)
        {
            greenSlime = GetComponentInParent<GreenSlime>();
            poly2D = GetComponent<PolygonCollider2D>();//그린슬라임은 닿았는지체크용도
            if (greenSlime.GetSlimeType() == 1)
            {
                poly2D.enabled = false;
            }
        }
        else if (slimeType == SlimeType.BlueSlime)
        {
            blueSlime = GetComponentInParent<BlueSlime>();
            box2d = GetComponent<BoxCollider2D>();
            poly2D = GetComponent<PolygonCollider2D>();//블루슬라임의 공격범위
            poly2D.enabled = false;
            if (blueSlime.GetBlueSlimeType() == 1)
            {
                box2d.enabled = false;
            }

        }
        else if (slimeType == SlimeType.RedSlime)
        {
            redSlime = GetComponentInParent<RedSlime>();
            poly2D = GetComponent<PolygonCollider2D>();
        }

        m_anim2d = GetComponentInParent<Animator>();
        player = GameManager.instance.GetPlayerTransform().GetComponent<Player>();
    }

    private void Update()
    {
        playerHpReMove();//대미지입는부분

        isGreenSlime();
        isBlueSlime();
        isRedSlime();//각각 맞는히트타입을가져온후에 적용해줄것


        isSlimeCheck();
    }

    private void playerHpReMove()
    {
        if (playerGreenSlimeHitCheck)
        {
            if (oneHitCheck)
            {
                return;
            }
            player.EnmeyPlayerRemoveHp();
            m_anim2d.SetTrigger("Death");
            Destroy(gameObject);
            oneHitCheck = true;
        }

        if (blueSlimeAttackCheck)
        {

            if (poly2D.IsTouchingLayers(LayerMask.GetMask("Player")))
            {
                if (!oneHitCheck)
                {
                    player.EnmeyPlayerRemoveHp();
                    oneHitCheck = true;
                    m_anim2d.SetBool("BlueDeath", true);
                }
                //여기는 플레이어가 대미지를입는부분 만약도중에닿는다면 bluedeath넣어줄것
            }
        }

        if (playerRedSlimeHitCheck)
        {
            player.EnmeyPlayerRemoveHp();
            m_anim2d.SetTrigger("Death");
            Destroy(gameObject);
        }
    }

    private void isGreenSlime()
    {
        if (slimeType != SlimeType.GreenSlime)
        {
            return;
        }
        PlayerCheck = greenSlime.GetPlayerCheck();
        if (PlayerCheck)
        {
            if (poly2D.IsTouchingLayers(LayerMask.GetMask("Player")))
            {
                if (player.GetPlayerType() == Player.eType.Green)
                {
                    playerGreenSlimeHitCheck = true;
                }
                else
                {
                    greenSlime.SetReturnStart(true);
                }
            }
        }
    }

    private void isBlueSlime()
    {
        if (slimeType != SlimeType.BlueSlime)
        {
            return;
        }
        if (box2d.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            if (player.GetPlayerType() == Player.eType.Blue)
            {
                blueSlimeAttackCheck = true;

                poly2D.enabled = true;
            }
            else
            {
                blueSlimeAttackCheck = false;
                blueSlime.SetReturnStart(true);
            }
        }
    }

    private void isRedSlime()
    {
        if (slimeType != SlimeType.RedSlime)
        {
            return;
        }
        PlayerCheck = redSlime.GetPlayerCheck();
        if (PlayerCheck)
        {
            if (poly2D.IsTouchingLayers(LayerMask.GetMask("Player")))
            {
                if (player.GetPlayerType() == Player.eType.Red)
                {
                    playerRedSlimeHitCheck = true;
                    redSlime.SetEnemyDeathCheck(true);
                }
                else
                {
                    redSlime.SetSlimeReturn(true);
                }
            }
        }
    }



    private void isSlimeCheck()
    {
        if (slimeType == SlimeType.GreenSlime)
        {
            if (greenSlime.GetSlimeType() != 0)
            {
                Destroy(gameObject);
            }
        }
        else if (slimeType == SlimeType.BlueSlime)
        {
            if (blueSlime.GetSlimeType() != 0)
            {
                Destroy(gameObject);
            }
        }
        else if (slimeType == SlimeType.RedSlime)
        {
            //레드슬람임이 무엇인지체크용
        }
    }

    public bool GetPlayerHitCeck()
    {
        return playerGreenSlimeHitCheck;
    }

    public bool GetBlueAttackCheck()
    {
        return blueSlimeAttackCheck;
    }

    public void SetBlueAttackCheck(bool _value)
    {
        blueSlimeAttackCheck = _value;
    }




}

