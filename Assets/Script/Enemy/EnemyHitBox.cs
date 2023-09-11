using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyHitBox : MonoBehaviour
{
    private GreenSlime greenSlime;//�׸������� ��Ʈ�ڽ������� �����÷����ʿ����� ������Ÿ����3��


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
            //���彽���� ��ũ��Ʈ
        }
        else if (slimeType == SlimeType.BlueSlime)
        {
            //��罺���� ��ũ��Ʈ
        }

        m_anim2d = GetComponentInParent<Animator>();
        poly2D = GetComponent<PolygonCollider2D>();
        player = GameManager.instance.GetPlayerTransform().GetComponent<Player>();
    }

    private void Update()
    {
        playerHpReMove();//������Դºκ�

        isGreenSlime();
        isRedSlime();//���� �´���ƮŸ�����������Ŀ� �������ٰ�
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

