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
    private bool playerHpReMoveCheck = false;
    private PolygonCollider2D poly2D;

    private void Start()
    {
        if(slimeType== SlimeType.GreenSlime)
        {
            greenSlime = GetComponentInParent<GreenSlime>();
        }
        else if(slimeType == SlimeType.RedSlime)
        {
            //���彽���� ��ũ��Ʈ
        }
        else if(slimeType == SlimeType.BlueSlime)
        {
            //��罺���� ��ũ��Ʈ
        }
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
        if (playerHitCheck&& playerHpReMoveCheck)
        {
            player.SetPlayerHp(1); 
            playerHpReMoveCheck = false;
        }
    }

    private void isGreenSlime()
    {
        if(slimeType != SlimeType.GreenSlime)
        {
            return;
        }
        PlayerCheck = greenSlime.GetPlayerCheck();
        if (PlayerCheck)
        {
            if (poly2D.IsTouchingLayers(LayerMask.GetMask("Player")))
            {
                playerHitCheck = true;
                playerHpReMoveCheck = true;
            }
        }
    }
    private void isRedSlime()
    {
        
    }

    private void isBlueSlime()
    {

    }
}

