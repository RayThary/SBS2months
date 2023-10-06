using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("�׽�Ʈ�� hit �� ü�°��Ҹ��ȴް��ϱ�")]
    [SerializeField] private bool NoHit = false;//���߿� �����ð��� Ʈ��γ־������ɵ� �ϴ��׽�Ʈ�뵵�����־��Ͽ���
    public enum eType
    {
        Red,
        Blue,
        Green,
    }
    public enum eGroundType
    {
        Ground,
        Lava,
        Water,
        WaterCourse,
        Trap,
    }
    public enum eHitGroundType//�߰��ٶ�
    {
        None,
        Ground,
        Lava,
        Water,
        WaterCourse,
        Trap,
    }
    //üũ�����θ��� ���߿� 3��serializeField�����ʿ�
    [SerializeField] private eType PlayerType;
    [SerializeField] private eGroundType GroundType;
    [SerializeField] private eHitGroundType HitType;

    private eGroundType beforGroundType;

    [SerializeField, Range(0, 3)] private int playerHp = 3;
    //�÷��̾� ������ ��������̽ð�
    [SerializeField] private float changeCoolTime = 3.0f;
    private float changeTimer = 0.0f;
    private bool m_playerChangeCoolTime;

    private bool playerRedCheck = false;
    private bool playerBlueCheck = false;
    private bool playerGreenCheck = true;

    //�������������ϱ����ѿ뵵
    [SerializeField] private bool playerWaterJumpCheck = false;
    private bool playerWaterCheck = false;
    private bool playerNoContinuityJunp;

    //�������� ���Ʒ��� ���ٴϴ½ð� ���ο��� �����������ʿ䵵���� 1�������ؼ� ����������
    [Header("���������ִ±��")]
    [SerializeField] private float floatingTimer = 1.0f;
    private float floatingTime = 0f;
    [SerializeField] private float floatingMovingMax = 0.2f;//�����ٴϴ� �ִ��ּҰ� 
    private float floatingMoving = 1f;
    private bool floatingChange = false;
    private bool playerWaterJump = false;
    private bool m_nofloating;
    private bool m_fristWaterTouch;

    private bool waterHightCheck;
    [Header("�⺻���")]
    //��&�⺻���
    [SerializeField] private float m_speed = 2.0f;
    private float m_gravity = 9.81f;
    private float m_jumpGravity = 0f;
    [SerializeField] private float m_playerJump = 5f;//�÷��̾��� ������
    [SerializeField] private float m_playerInLavaJump = 3f;//��ϼӿ����� ������ �Ƹ� �����ִºκп����� �������ϵ� �ٸ������� ���� �̸��ٲ��ʿ䰡����
    private bool m_jumpCheck = false;
    private bool m_groundCheck;
    private bool m_deathCheck = false;
    private bool m_oneDeathCheck = false;//����Ʈ�����ѹ�üũ��

    //���
    private bool m_groundLavaCheck = true;//��Ͽ����� ����������ҿ����ٵ��Ƹ� ��Ϲۿ��������� ���ٿ���
    private bool m_inLavaCheck;//��Ͼȿ��ִ���üũ�� ��Ͼȿ������� �߷°������ٿ뵵�ǺҰ�
    private bool m_lavaJumpCheck;//��Ͼȿ����� �������ҰŰ��� 
    private bool m_lavaGravityCheck = true;
    private bool m_lavaDownCheck;//��ϼӿ��� �Ʒ��ΰ��� ���������Գ������¿뵵
    private bool m_lavaJumpDelay = false;
    private float m_lavaJumpDelayTime = 0.0f;

    //������Դ¿뵵 �κ�
    [SerializeField] private float m_HitTime = 3.0f;
    private float m_hitTimer = 0.0f;
    private bool hitCheck;

    //������Դ¿뵵 ��ù����ð�
    [SerializeField] private float m_HitInvincibilityTime = 1.0f;
    private float m_hitInvincibilityTimer = 0.0f;
    private bool m_hitInvincibility;

    private BoxCollider2D m_groundCheckBox2d;
    private bool m_playerTrapHit;//Ʈ������ �¾Ҵ��� üũ�뵵
    private bool m_trapHitCheck;//�÷��̾�� �¾Ҵ��� üũ�뵵
    private bool m_fallTrapHitCheck;//�÷��̾�� �������°� �¾Ҵ���üũ���� �����뵵
    private bool m_trapHitInvincibility;
    private bool m_trapGroundHit;
    private bool m_trapHpRemove;

    [SerializeField] private float m_TrapHitInvincibilityTime = 2.0f;
    private float m_TrapHitInvincibilityTimer = 0.0f;

    private bool m_trapHitAlphaChange = true;
    [SerializeField] private float m_alphaChangeTime = 0.3f;
    private float m_alphaChangeTimer = 0.0f;
    private float m_alphaChangeTimeCheck;
    private Color m_sprColor;

    //�����ۺκ� ���̾��Ű����� json���� �κ��丮�����������ʿ����� �ƴϸ� �Ұ����� on/offüũ���ִ¹������������
    [SerializeField]private bool PlayerIsKey = false;//�׽�Ʈ�� �����ʿ�
    private bool doorLockisOpen = false;
    private bool doorKeyCheck = false;

    [SerializeField]private bool playerStop = false;//�÷��̾ ����������¿뵵 ������Ʈ�����������������̹Ƿ� �����̸�ȵɰ�쿡���ʿ�����

    //�ε忡�� ���̵��ξƿ��� �������������뵵
    private bool fadeCheck;
    private bool fadeInCheck;
    private bool noMoveJump = false;

    //�÷��̾��Ǻ��ο���ã�ƿð͵�
    private SpriteRenderer m_spr;
    private Transform m_Trs;
    private BoxCollider2D m_box2d;
    private Animator m_anim;
    private Rigidbody2D m_rig2d;
    private Vector3 moveDir;

    void Start()
    {
        floatingTime = floatingTimer;
        m_Trs = GetComponent<Transform>();
        m_groundCheckBox2d = m_Trs.Find("PlayerGroundCheck").GetComponent<BoxCollider2D>();
        m_rig2d = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
        m_box2d = GetComponent<BoxCollider2D>();
        m_spr = GetComponent<SpriteRenderer>();
        m_sprColor = m_spr.color;
        m_alphaChangeTimeCheck = m_alphaChangeTime;

    }


    void Update()
    {
        playerFadeCheck();//���̵��ξƿ��� �����������ϰ��ϴºκ�
        playerDeathMotion();
        playerInvincibilityTime();//�ǰݽ� �����ð�
        playerTrapHpRemove();//Ʈ��������Դºκ�
        if (playerStop)//�÷��̾������ ��
        {
            m_rig2d.velocity = new Vector2(0, m_rig2d.velocity.y);
            playerGravity();//��� ���������� �߷°��� ���⼭�����ش� 
            return;
        }
        playerMove();//�̵�
        playerJump();//���������üũ�ϴ°�
        lavaJumpDelay();//��ϼ������Ҷ� �������δ����� ������
        playerGravity();//��� ���������� �߷°��� ���⼭�����ش� 
        playerfloating();//�������� �������� �˰�������ҽ��ϴ�
        floatingTimeChange();//�����������ٴҶ� ���Ʒ��������� �ð�üũ�ϴºκ�
        playerChange();//�÷��̾����Ÿ���� �ٲٴ°�
        playerChaneTimer();//�÷��̾� ������Ÿ���� ��Ÿ���������ִ°�
        playerCheckWaterHight();//���ٱ⸦ ������ ���ƿ����¿뵵�θ������
        playerHitCheck();//�����������ǰ�����
        playerTrapHitCheck();//Ʈ����Ʈ�����ִºκ�
        playerTrapHitInvincibility();
        playerKeyDel();//����������Ȯ���� Ű�����Ұ������ִ¿뵵
        playerHitTime();//������ �����ð������κ�
        playerStopCheck();//�����̸�ȵǴºκ����������߰������ʿ�����
    }
    
    private void playerMove()
    {
        if (noMoveJump)
        {
            return;
        }
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    moveDir.x = 1;
        //    m_Trs.localScale = new Vector3(1, 1, 1);
        //}
        //else if (Input.GetKeyUp(KeyCode.RightArrow))
        //{
        //    moveDir.x = 0;
        //}

        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    moveDir.x = -1;
        //    m_Trs.localScale = new Vector3(-1, 1, 1);

        //}
        //else if (Input.GetKeyUp(KeyCode.LeftArrow))
        //{
        //    moveDir.x = 0;
        //}

        moveDir.x = Input.GetAxisRaw("Horizontal");
        if (moveDir.x != 0)
        { 
            m_Trs.localScale = new Vector3(moveDir.x == 1 ? 1 : -1 , 1, 1);
        }

        if (m_inLavaCheck)
        {
            m_rig2d.velocity = moveDir * 0.5f * m_speed;
        }
        else
        {
            m_rig2d.velocity = moveDir * m_speed;
        }
    }

    private void playerJump()
    {
        if (noMoveJump)
        {
            return;
        }
        if (m_groundCheck)//�����ִ��� üũ�ϱ����ѿ뵵
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                m_jumpCheck = true;
            }
        }

        //�������� ��ӹ��ȿ������ϱ� �̰ɷ�üũ�ϴ°Ը´µ���
        if (GroundType == eGroundType.Water)
        {
            if (playerWaterJump == false)
            {
                if (playerNoContinuityJunp)
                {
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        playerWaterJump = true;//������ ������������
                        playerWaterCheck = false;//�������ִ���
                        playerNoContinuityJunp = false;//�ι�����������
                        m_nofloating = true;//�������ִ��ڵ� �ȵ��԰���뵵
                    }

                }

            }
        }

        if (m_inLavaCheck)//��Ͼȼӿ��ִ���üũ��
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (m_lavaJumpDelay == false)
                {
                    m_lavaJumpCheck = true;
                    m_lavaJumpDelay = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                m_lavaDownCheck = true;
            }

            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                m_lavaDownCheck = false;
            }
        }

    }

    private void lavaJumpDelay()
    {
        if (m_lavaJumpDelay)
        {
            m_lavaJumpDelayTime += Time.deltaTime;
            if (m_lavaJumpDelayTime >= 0.3f)
            {
                m_lavaJumpDelay = false;
                m_lavaJumpDelayTime = 0.0f;
            }
        }
    }

    private void playerGravity()
    {

        if (GroundType == eGroundType.Ground)
        {
            if (!m_groundCheck)
            {
                m_jumpGravity -= m_gravity * Time.deltaTime;
                if (m_jumpGravity < -10f)
                {
                    m_jumpGravity = -10f;
                }
            }
            else
            {
                if (m_jumpCheck)
                {
                    m_jumpCheck = false;
                    m_jumpGravity = m_playerJump;
                }
                else if (m_jumpGravity < 0)
                {
                    m_jumpGravity += m_gravity * Time.deltaTime;
                    if (m_jumpGravity > 0)
                    {
                        m_jumpGravity = 0;
                    }

                }
            }
        }
        else if (GroundType == eGroundType.Water)
        {
            if (playerWaterJump)
            {
                m_jumpGravity = m_playerJump;
            }
            if (playerWaterJumpCheck)
            {
                m_jumpGravity -= m_gravity * Time.deltaTime;

                if (m_jumpGravity < 0)
                {
                    m_nofloating = false;
                }

                if (m_jumpGravity < -10f)
                {
                    m_jumpGravity = -10f;
                }
            }




        }
        else if (GroundType == eGroundType.Lava)
        {
            if (m_inLavaCheck)
            {
                if (m_lavaGravityCheck)//��Ͼȿ����� �⺻�߷�
                {
                    if (m_lavaDownCheck)
                    {
                        m_jumpGravity = -2f;
                    }
                    else
                    {
                        m_jumpGravity = -0.4f;
                    }
                }

                if (m_lavaJumpCheck)
                {
                    m_lavaGravityCheck = false;
                    m_groundLavaCheck = false;
                    m_lavaJumpCheck = false;
                    m_jumpGravity = m_playerInLavaJump;
                }
                else if (!m_groundLavaCheck)
                {
                    m_jumpGravity -= m_gravity * Time.deltaTime;
                    if (m_jumpGravity < -3f)
                    {
                        m_jumpGravity += m_gravity * 1.5f * Time.deltaTime;
                        if (m_jumpGravity < -0.4f)
                        {
                            m_jumpGravity = -0.4f;
                            m_groundLavaCheck = true;
                            m_lavaGravityCheck = true;
                        }
                    }
                }

            }
            else
            {
                m_jumpGravity -= m_gravity * Time.deltaTime;
                if (m_jumpGravity < -10f)
                {
                    m_jumpGravity = -10f;
                }
            }
        }
        else if (GroundType == eGroundType.WaterCourse)
        {
            m_jumpGravity = 1.5f;
        }

        if (waterHightCheck)//���ְ���̿������� �ٸ������� �������̳����ִ°��� ���δ�üũ�ؼ� �������ٰ�
        {
            m_groundCheck = false;
            m_jumpGravity = m_playerJump;
        }

        if (m_trapGroundHit)//���߿� Ÿ�Ժ��γ����ٻ��������� ������ �����´°��� �־ ���ٲ��ָ�ɵ�
        {
            m_jumpGravity = 2;
            m_trapGroundHit = false;
        }

        m_rig2d.velocity = new Vector2(m_rig2d.velocity.x, m_jumpGravity);
    }


    private void playerfloating()
    {
        if (m_nofloating)
        {
            return;
        }

        if (GroundType == eGroundType.Water)
        {
            if (!playerWaterCheck)
            {
                return;
            }
            if (floatingTime >= floatingTimer)
            {
                if (m_fristWaterTouch)
                {
                    floatingMoving = -floatingMovingMax - 0.1f;
                }
                else
                {
                    floatingMoving = -floatingMovingMax;
                }
                floatingChange = true;
            }
            else if (floatingTime < -floatingTimer)
            {
                floatingMoving = floatingMovingMax;
                floatingChange = false;
            }

            m_rig2d.velocity = new Vector2(m_rig2d.velocity.x, floatingMoving);
        }
    }


    private void floatingTimeChange()
    {
        if (GroundType == eGroundType.Water)
        {
            if (floatingChange)
            {
                if (m_fristWaterTouch)
                {
                    m_fristWaterTouch = false;
                }
                floatingTime -= Time.deltaTime;
            }
            if (!floatingChange)
            {
                floatingTime += Time.deltaTime;

            }
        }
    }




    private void playerCheckWaterHight()
    {
        if (m_box2d.IsTouchingLayers(LayerMask.GetMask("WaterCourse")))
        {
            BeforGroundTypeCheck();
            GroundType = eGroundType.WaterCourse;
            HitType = eHitGroundType.WaterCourse;
        }
        else
        {
            if (GroundType == eGroundType.WaterCourse)
            {
                GroundType = beforGroundType;
                HitType = eHitGroundType.None;
            }
        }
        if (m_box2d.IsTouchingLayers(LayerMask.GetMask("WaterHight")))
        {
            waterHightCheck = true;
        }
        else
        {
            waterHightCheck = false;
        }
    }

    private void BeforGroundTypeCheck()
    {
        if (GroundType != eGroundType.WaterCourse)
        {
            beforGroundType = GroundType;
        }

    }

    private void playerChange()
    {
        if (m_playerChangeCoolTime)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerType = eType.Red;
            playerSlimeCheck("Red");
            if (m_anim.GetBool("Red") == true)
            {
                return;
            }
            animSetBool("Red");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerType = eType.Blue;
            playerSlimeCheck("Blue");
            if (m_anim.GetBool("Blue") == true)
            {
                return;
            }
            animSetBool("Blue");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayerType = eType.Green;
            playerSlimeCheck("Green");
            if (m_anim.GetBool("Green") == true)
            {
                return;
            }
            animSetBool("Green");
        }
    }
    private void animSetBool(string _name)
    {

        m_anim.SetBool("Red", "Red" == _name);
        m_anim.SetBool("Blue", "Blue" == _name);
        m_anim.SetBool("Green", "Green" == _name);
        m_playerChangeCoolTime = true;

        #region
        /*
        if (_name == "Red")
        {
            m_anim.SetBool("Red", true);
            m_anim.SetBool("Blue", false);
            m_anim.SetBool("Green", false);
        }
        else if( _name == "Blue")
        {
            m_anim.SetBool("Red", false);
            m_anim.SetBool("Blue", true);
            m_anim.SetBool("Green", false);
        }
        else if (_name=="Green")
        {
            m_anim.SetBool("Red", false);
            m_anim.SetBool("Blue", false);
            m_anim.SetBool("Green", true);
        }
        m_playerChangeCoolTime = true;
        */
        #endregion
    }

    private void playerSlimeCheck(string _name)
    {
        playerRedCheck = _name == "Red";
        playerBlueCheck = _name == "Blue";
        playerGreenCheck = _name == "Green";

    }


    private void playerChaneTimer()
    {
        if (m_playerChangeCoolTime)
        {
            changeTimer += Time.deltaTime;
            if (changeTimer >= changeCoolTime)
            {
                changeTimer = 0.0f;
                m_playerChangeCoolTime = false;
            }
        }
    }
    private void playerTrapHitCheck()
    {
        if (m_trapHitInvincibility)
        {
            return;
        }
        if (m_groundCheckBox2d.IsTouchingLayers(LayerMask.GetMask("Trap")))
        {
            m_playerTrapHit = true;
            m_trapGroundHit = true;
        }




    }

    private void playerTrapHitInvincibility()
    {
        if (m_trapHitCheck)
        {
            NoHit = true;
            m_playerTrapHit = false;
            m_trapHitInvincibility = true;
            m_TrapHitInvincibilityTimer += Time.deltaTime;
            if (m_TrapHitInvincibilityTimer >= m_TrapHitInvincibilityTime)
            {
                NoHit = false;
                m_sprColor.a = 1f;
                m_spr.color = m_sprColor;
                m_trapHitInvincibility = false;
                m_trapHitCheck = false;
                m_TrapHitInvincibilityTimer = 0.0f;
            }
        }

        if (m_trapHitInvincibility)
        {
            if (m_trapHitAlphaChange)
            {
                m_sprColor.a = 0.5f;
                m_spr.color = m_sprColor;
                m_alphaChangeTimer += Time.deltaTime;
                if (m_alphaChangeTimer >= m_alphaChangeTime)
                {
                    m_trapHitAlphaChange = false;
                    m_alphaChangeTimer = m_alphaChangeTimeCheck;
                }
            }
            else
            {
                m_sprColor.a = 1f;
                m_spr.color = m_sprColor;
                m_alphaChangeTimer -= Time.deltaTime;
                if (m_alphaChangeTimer <= 0)
                {
                    m_trapHitAlphaChange = true;
                    m_alphaChangeTimer = 0.0f;
                }
            }
        }
    }

    private void playerTrapHpRemove()
    {
        if (NoHit)
        {
            return;
        }
        if (m_trapHpRemove)
        {
            playerHp -= 1;
            m_trapHpRemove = false;
            if (PlayerType == eType.Blue)
            {
                m_anim.SetTrigger("BlueHit");
            }
            else if (PlayerType == eType.Red)
            {
                m_anim.SetTrigger("RedHit");
            }
            else if (PlayerType == eType.Green)
            {
                m_anim.SetTrigger("GreenHit");
            }
        }
    }
    private void playerHitCheck()
    {
        if (PlayerType == eType.Green)
        {
            if (HitType == eHitGroundType.Ground)//������� �ǰ��ȴٴ� ����������üũ�Ѵ� ���� ������ ��Ʈ�ð��� �ʱ�ȭ���ִºκа��� �I�����κ��� �߰����ֱ�ٶ�
            {
                hitCheck = false;
                m_hitTimer = 0;
            }
            if (HitType != eHitGroundType.None && HitType != eHitGroundType.Ground)//�׸��϶� ���������������� ���⿡�߰�
            {
                hitCheck = true;
            }
            else if (GroundType == eGroundType.WaterCourse)
            {
                hitCheck = true;
            }
        }

        if (PlayerType == eType.Blue)
        {
            if (HitType == eHitGroundType.Water || HitType == eHitGroundType.WaterCourse)//������� �ǰ��ȴٴ� ����������üũ�Ѵ� ���� ������ ��Ʈ�ð��� �ʱ�ȭ���ִºκа��� �I�����κ��� �߰����ֱ�ٶ�
            {
                hitCheck = false;
                m_hitTimer = 0;
            }
            if (HitType != eHitGroundType.None && HitType != eHitGroundType.Water)//����϶� ���������������� ���⿡�߰�
            {
                hitCheck = true;
            }

        }

        if (PlayerType == eType.Red)
        {
            if (HitType == eHitGroundType.Lava)//������� �ǰ��ȴٴ� ����������üũ�Ѵ� ���� ������ ��Ʈ�ð��� �ʱ�ȭ���ִºκа��� �I�����κ��� �߰����ֱ�ٶ�
            {
                hitCheck = false;
                m_hitTimer = 0;
            }
            if (HitType != eHitGroundType.None && HitType != eHitGroundType.Lava)//�����϶� ���������������� ���⿡�߰�
            {
                hitCheck = true;
            }
            else if (GroundType == eGroundType.WaterCourse)
            {
                hitCheck = true;
            }
        }
    }


    private void playerHitTime()
    {
        if (!hitCheck || HitType == eHitGroundType.None)
        {
            return;
        }
        m_hitTimer += Time.deltaTime;
        if (m_hitTimer >= m_HitTime)
        {
            if (PlayerType == eType.Blue)
            {
                m_anim.SetTrigger("BlueHit");
            }
            else if (PlayerType == eType.Red)
            {
                m_anim.SetTrigger("RedHit");
            }
            else if (PlayerType == eType.Green)
            {
                m_anim.SetTrigger("GreenHit");
            }
            m_hitTimer = 0;
        }
    }

    private void playerInvincibilityTime()
    {
        if (m_hitInvincibility)
        {
            playerStop = true;

            m_hitInvincibilityTimer += Time.deltaTime;
            if (m_hitInvincibilityTimer >= m_HitInvincibilityTime)
            {
                playerStop = false;
                m_hitInvincibility = false;
                m_hitInvincibilityTimer = 0.0f;
            }
        }

    }

    private void playerKeyDel()
    {
        if (doorLockisOpen)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                PlayerIsKey = false;
                doorKeyCheck = true;
            }
            else
            {
                doorKeyCheck = false;
            }
        }
    }

    private void playerStopCheck()
    {
        if (playerHp <= 0)
        {
            playerStop = true;
        }
        else
        {
            playerStop = false;
        }
    }

    private void playerDeathMotion()
    {
        if (m_oneDeathCheck)
        {
            return;
        }
        if (playerHp <= 0)
        {
            m_rig2d.velocity = new Vector2(0, 0);
            if (PlayerType == eType.Blue)
            {
                m_anim.SetTrigger("BlueDeath");
            }
            else if (PlayerType == eType.Red)
            {
                m_anim.SetTrigger("RedDeath");
            }
            else if (PlayerType == eType.Green)
            {
                m_anim.SetTrigger("GreenDeath");
            }
            m_oneDeathCheck = true;
        }
    }

    private void playerFadeCheck()
    {
        fadeCheck = LoadManager.instance.GetStageChange();
        if (fadeCheck)
        {
            fadeInCheck = true;
        }
        if (fadeInCheck)
        {
            if (HitType == eHitGroundType.None)
            {
                noMoveJump = true;
            }
            else
            {
                noMoveJump = false;
                fadeInCheck = false;
            }
        }
    }

    //�ִϸ����Ϳ��� �̾Ʒ��� �ִϸ����Ϳ��� üũ���ٺκ��� 
    private void playerDeath()
    {
        m_deathCheck = true;
    }

    private void playerHit()
    {
        if (NoHit)
        {
            return;
        }
        playerHp -= 1;
        m_rig2d.velocity = new Vector2(0, 0);
        m_hitInvincibility = true;
    }
    //�������

    public void OnTriggerPlayer(PlayerHitBox.HitBoxType _state, PlayerHitBox.HitType _hitType, Collider2D _collision)
    {
        switch (_state)
        {
            case PlayerHitBox.HitBoxType.Enter:
                switch (_hitType)
                {
                    case PlayerHitBox.HitType.Ground:

                        if (_collision.gameObject.layer == LayerMask.NameToLayer("Ground") || _collision.gameObject.layer == LayerMask.NameToLayer("MoveGround"))
                        {
                            GroundType = eGroundType.Ground;
                            HitType = eHitGroundType.Ground;
                            playerWaterCheck = false;
                            m_groundCheck = true;
                        }
                        else if (_collision.gameObject.layer == LayerMask.NameToLayer("Water"))
                        {
                            GroundType = eGroundType.Water;
                            HitType = eHitGroundType.Water;
                            floatingTime = 0.5f;//�ܺο��������ؽð���θ����ٰ�
                            m_fristWaterTouch = true;
                            if (playerWaterJump == false)
                            {
                                playerNoContinuityJunp = true;
                                playerWaterJumpCheck = false;
                                playerWaterCheck = true;
                            }

                        }
                        else if (_collision.gameObject.layer == LayerMask.NameToLayer("Lava"))
                        {
                            GroundType = eGroundType.Lava;
                            HitType = eHitGroundType.Lava;
                            playerWaterCheck = false;
                            m_inLavaCheck = true;
                        }
                        else if (_collision.gameObject.layer == LayerMask.NameToLayer("WaterCourse"))
                        {
                            //playerWaterCheck = false;
                            //HitType = eHitGroundType.WaterCourse;
                        }

                        if (_collision.gameObject.layer == LayerMask.NameToLayer("Trap"))
                        {
                            m_groundCheck = true;
                        }

                        break;

                    case PlayerHitBox.HitType.Item:
                        if (_collision.gameObject.layer == LayerMask.NameToLayer("Key"))
                        {
                            PlayerIsKey = true;
                        }
                        if (_collision.gameObject.layer == LayerMask.NameToLayer("DoorLock"))
                        {
                            doorLockisOpen = true;
                        }

                        if (_collision.gameObject.layer == LayerMask.NameToLayer("FallTrap"))
                        {
                            m_trapGroundHit = true;
                            m_trapHitCheck = true;
                        }
                        break;
                }
                break;

            case PlayerHitBox.HitBoxType.Exit:
                switch (_hitType)
                {
                    case PlayerHitBox.HitType.Ground:
                        if (_collision.gameObject.layer == LayerMask.NameToLayer("Ground") || _collision.gameObject.layer == LayerMask.NameToLayer("MoveGround"))
                        {
                            HitType = eHitGroundType.None;
                            m_groundCheck = false;
                        }
                        else if (_collision.gameObject.layer == LayerMask.NameToLayer("Water"))
                        {
                            HitType = eHitGroundType.None;
                            playerWaterJump = false;
                            playerWaterJumpCheck = true;
                        }
                        else if (_collision.gameObject.layer == LayerMask.NameToLayer("Lava"))
                        {
                            HitType = eHitGroundType.None;
                            m_inLavaCheck = false;
                        }
                        else if (_collision.gameObject.layer == LayerMask.NameToLayer("WaterCourse"))
                        {
                            //HitType = eHitGroundType.None;
                        }

                        if (_collision.gameObject.layer == LayerMask.NameToLayer("Trap"))
                        {
                            m_groundCheck = false;
                            HitType = eHitGroundType.None;
                        }
                        break;

                    case PlayerHitBox.HitType.Item:
                        if (_collision.gameObject.layer == LayerMask.NameToLayer("DoorLock"))
                        {
                            doorLockisOpen = false;
                        }

                        break;
                }
                break;
        }
    }



    //�ܺο��� �ʿ��ѵ�����
    public int GetPlayerHp()
    {
        return playerHp;
    }

    //ü�±�¿뵵 ��������ŭ ��������Դ´� ��κ�1�ϵ���h
    public void EnmeyPlayerRemoveHp()
    {
        if (NoHit)
        {
            return;
        }
        m_rig2d.velocity = new Vector2(0, 0);
        m_hitInvincibility = true;
        if (PlayerType == eType.Blue)
        {
            m_anim.SetTrigger("BlueHit");
        }
        else if (PlayerType == eType.Red)
        {
            m_anim.SetTrigger("RedHit");
        }
        else if (PlayerType == eType.Green)
        {
            m_anim.SetTrigger("GreenHit");
        }
    }

    //�Ⱦ��� �Ⱦ���������ּ� Ű�κп��� ���̶� Ű������ɵ��� 
    public bool PlayerKeyCheck()
    {
        return PlayerIsKey;
    }

    public bool PlayerdoorKeyCheck()
    {
        return doorKeyCheck;
    }

    public void SetPlayerStop(bool _value)
    {
        playerStop = _value;
    }

    public bool GetPlayerTrapHitCheck()
    {
        return m_playerTrapHit;
    }

    public void SetPlayerTrapHit(bool _value)
    {
        m_trapHitCheck = _value;


    }
    public bool GetPlayerTrapHit()
    {
        return m_trapHitCheck;
    }

    public void SetTrapHpRemove(bool _value)
    {
        m_trapHpRemove = _value;
    }

    public eType GetPlayerType()
    {
        return PlayerType;
    }

    public eGroundType GetPlayerGroundType()
    {
        return GroundType;
    }

    public bool GetNoHitCheck()
    {
        return NoHit;
    }

    public bool GetDeathCheck()
    {
        return m_deathCheck;
    }
    public void SetDeathCheck(bool _value)
    {
        m_deathCheck = _value;
    }
    public void SetPlayerReset()
    {
        playerHp = 3;
        PlayerType = eType.Green;
        NoHit = false;
        playerStop = false;
        m_sprColor.a = 1f;
        m_spr.color = m_sprColor;
        m_trapHitInvincibility = false;
        m_trapHitCheck = false;
        m_TrapHitInvincibilityTimer = 0.0f;
    }
    public void SetOneDeathReturn()
    {
        m_oneDeathCheck = false;
    }
}
