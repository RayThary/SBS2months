using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyHitBox : MonoBehaviour
{
    private GreenSlime greenSlime;//그린슬라임 히트박스용으로 점점늘려줄필요있음 슬라임타입은3개


    public enum SlimeType
    {
        GreenSlime,
        RedSlime,
        BlueSlime,
    }
    [SerializeField] private SlimeType slimeType;

    private Player player;


    private bool PlayerCheck;
    private bool playerHitCheck = false;

    private PolygonCollider2D poly2D;
    private Animator m_anim2d;

    private void Start()
    {
        if (slimeType == SlimeType.GreenSlime)
        {
            greenSlime = GetComponentInParent<GreenSlime>();
        }
        else if (slimeType == SlimeType.RedSlime)
        {
            //레드슬라임 스크립트
        }
        else if (slimeType == SlimeType.BlueSlime)
        {
            //블루스라임 스크립트
        }

        m_anim2d = GetComponentInParent<Animator>();
        poly2D = GetComponent<PolygonCollider2D>();
        player = GameManager.instance.GetPlayerTransform().GetComponent<Player>();
    }

    private void Update()
    {
        playerHpReMove();//대미지입는부분

        isGreenSlime();
        isRedSlime();//각각 맞는히트타입을가져온후에 적용해줄것
        isBlueSlime();


    }

    private void playerHpReMove()
    {
        if (playerHitCheck)
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
                    playerHitCheck = true;
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

    }

    public bool GetPlayerHitCeck()
    {
        return playerHitCheck;
    }
}

