using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyHitBox : MonoBehaviour
{
    private GreenSlime greenSlime;//�׸������� ��Ʈ�ڽ������� �����÷����ʿ����� ������Ÿ����3��
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

    private bool oneHitCheck=false;

    [SerializeField]private bool blueSlimeAttackCheck;

    private PolygonCollider2D poly2D;
    private BoxCollider2D box2d;//���ݹ����ȿ��ִ���üũ�� ��罽���ӿ뵵
    private Animator m_anim2d;

    private void Start()
    {
        if (slimeType == SlimeType.GreenSlime)
        {
            greenSlime = GetComponentInParent<GreenSlime>();
            poly2D = GetComponent<PolygonCollider2D>();//�׸��������� ��Ҵ���üũ�뵵
        }
        else if (slimeType == SlimeType.BlueSlime)
        {
            blueSlime = GetComponentInParent<BlueSlime>();
            box2d = GetComponent<BoxCollider2D>();
            poly2D = GetComponent<PolygonCollider2D>();//��罽������ ���ݹ���
            poly2D.enabled = false;
        }
        else if (slimeType == SlimeType.RedSlime)
        {
            //���彽���� ��ũ��Ʈ
        }
        
        m_anim2d = GetComponentInParent<Animator>();
        player = GameManager.instance.GetPlayerTransform().GetComponent<Player>();
    }

    private void Update()
    {
        playerHpReMove();//������Դºκ�

        isGreenSlime();
        isRedSlime();//���� �´���ƮŸ�����������Ŀ� �������ٰ�
        isBlueSlime();
        blueSlimeAttack();//��罽������ ����������ºκ�

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

        if (blueSlimeAttackCheck)
        {
            m_anim2d.SetBool("BlueAttack", true);
            if (poly2D.IsTouchingLayers(LayerMask.GetMask("Player")))
            {
                if (!oneHitCheck)
                {
                    player.SetPlayerHp(1);
                    oneHitCheck = true;
                    m_anim2d.SetBool("BlueDeath", true);
                }
                //����� �÷��̾ ��������Դºκ� ���൵�߿���´ٸ� bluedeath�־��ٰ�
            }
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
                poly2D.enabled = true;
            }
            else
            {
                blueSlimeAttackCheck = false;
                blueSlime.SetReturnStart(true);
            }
        }
    }

    private void blueSlimeAttack()
    {
        if (blueSlimeAttackCheck)
        {
            m_anim2d.SetBool("BlueAttack", true);
            if (poly2D.IsTouchingLayers(LayerMask.GetMask("Player")))
            {
                //����� �÷��̾ ��������Դºκ� ���൵�߿���´ٸ� bluedeath�־��ٰ�
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
            //���彽������ ��������üũ��
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

