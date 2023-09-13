using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyHitBox : MonoBehaviour
{
    private GreenSlime greenSlime;//그린슬라임 히트박스용으로 점점늘려줄필요있음 슬라임타입은3개
    private BlueSlime blueSlime;

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

    private PolygonCollider2D poly2D;//그린슬라임용도 레드슬라임도 쓸듯?
    private BoxCollider2D box2d;//공격범위안에있는지체크용 블루슬라임용도
    private Animator m_anim2d;

    private void Start()
    {
        if (slimeType == SlimeType.GreenSlime)
        {
            greenSlime = GetComponentInParent<GreenSlime>();
            poly2D = GetComponent<PolygonCollider2D>();
        }
        else if (slimeType == SlimeType.BlueSlime)
        {
            blueSlime = GetComponentInParent<BlueSlime>();
            box2d = GetComponent<BoxCollider2D>();
        }
        else if (slimeType == SlimeType.RedSlime)
        {
            //레드슬라임 스크립트
        }

        m_anim2d = GetComponentInParent<Animator>();
        player = GameManager.instance.GetPlayerTransform().GetComponent<Player>();
    }

    private void Update()
    {
        playerHpReMove();//대미지입는부분

        isGreenSlime();
        isRedSlime();//각각 맞는히트타입을가져온후에 적용해줄것
        isBlueSlime();

        isSlimeCheck();
    }

    private void playerHpReMove()
    {
        if (playerGreenSlimeHitCheck)
        {
            player.SetPlayerHp(1);
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
    private void isRedSlime()
    {

    }

    private void isBlueSlime()
    {
        if (slimeType != SlimeType.BlueSlime)
        {
            return;
        }
        if (box2d.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            if(player.GetPlayerType() == Player.eType.Blue)
            {
                blueSlimeAttackCheck = true;
            }
            else
            {
                blueSlimeAttackCheck = false;
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
            if(blueSlime.GetSlimeType() != 0)
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

    // 블루 애니메이션용


   

  

}

