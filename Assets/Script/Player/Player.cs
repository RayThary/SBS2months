using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("테스트용 hit 로 체력감소만안달게하기")]
    [SerializeField] private bool NoHit = false;//나중에 무적시간에 트루로넣어놓으면될듯 일단테스트용도로자주쓰일예정
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
    public enum eHitGroundType//추가바람
    {
        None,
        Ground,
        Lava,
        Water,
        WaterCourse,
        Trap,
    }
    //체크용으로만듬 나중에 3개serializeField삭제필요
    [SerializeField] private eType PlayerType;
    [SerializeField] private eGroundType GroundType;
    [SerializeField] private eHitGroundType HitType;

    private eGroundType beforGroundType;

    [SerializeField, Range(0, 3)] private int playerHp = 3;
    //플레이어 슬라임 변경딜레이시간
    [SerializeField] private float changeCoolTime = 3.0f;
    private float changeTimer = 0.0f;
    private bool m_playerChangeCoolTime;

    private bool playerRedCheck = false;
    private bool playerBlueCheck = false;
    private bool playerGreenCheck = true;

    //물에서점프를하기위한용도
    [SerializeField] private bool playerWaterJumpCheck = false;
    private bool playerWaterCheck = false;
    private bool playerNoContinuityJunp;

    //물위에서 위아래도 떠다니는시간 내부에서 세부조절할필요도있음 1을변경해서 조정도가능
    [Header("물위에서있는기능")]
    [SerializeField] private float floatingTimer = 1.0f;
    private float floatingTime = 0f;
    [SerializeField] private float floatingMovingMax = 0.2f;//물떠다니느 최대최소값 
    private float floatingMoving = 1f;
    private bool floatingChange = false;
    private bool playerWaterJump = false;
    private bool m_nofloating;
    private bool m_fristWaterTouch;

    private bool waterHightCheck;
    [Header("기본기능")]
    //땅&기본기능
    [SerializeField] private float m_speed = 2.0f;
    private float m_gravity = 9.81f;
    private float m_jumpGravity = 0f;
    [SerializeField] private float m_playerJump = 5f;//플레이어의 점프력
    [SerializeField] private float m_playerInLavaJump = 3f;//용암속에서의 점프력 아마 저항있는부분에서의 점프력일듯 다른곳에서 사용시 이름바꿀필요가있음
    private bool m_jumpCheck = false;
    private bool m_groundCheck;
    private bool m_deathCheck = false;
    private bool m_oneDeathCheck = false;//데스트리거한번체크용

    //용암
    private bool m_groundLavaCheck = true;//용암에서의 점프를담당할예정근데아마 용암밖에나갔을때 써줄예정
    private bool m_inLavaCheck;//용암안에있는지체크용 용암안에있을때 중력값정해줄용도의불값
    private bool m_lavaJumpCheck;//용암안에서의 점프를할거같음 
    private bool m_lavaGravityCheck = true;
    private bool m_lavaDownCheck;//용암속에서 아래로갈때 좀더빠르게내려가는용도
    private bool m_lavaJumpDelay = false;
    private float m_lavaJumpDelayTime = 0.0f;

    //대미지입는용도 부분
    [SerializeField] private float m_HitTime = 3.0f;
    private float m_hitTimer = 0.0f;
    private bool hitCheck;

    //대미지입는용도 잠시무적시간
    [SerializeField] private float m_HitInvincibilityTime = 1.0f;
    private float m_hitInvincibilityTimer = 0.0f;
    private bool m_hitInvincibility;

    private BoxCollider2D m_groundCheckBox2d;
    private bool m_playerTrapHit;//트랩에서 맞았는지 체크용도
    private bool m_trapHitCheck;//플레이어에서 맞았는지 체크용도
    private bool m_fallTrapHitCheck;//플레이어에서 떨어지는걸 맞았는지체크이후 삭제용도
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

    //아이템부분 많이쓸거같으면 json으로 인벤토리구현을해줄필요있음 아니면 불값으로 on/off체크해주는방법이좋을듯함
    [SerializeField]private bool PlayerIsKey = false;//테스트용 삭제필요
    private bool doorLockisOpen = false;
    private bool doorKeyCheck = false;

    [SerializeField]private bool playerStop = false;//플레이어가 멈춰있으라는용도 업데이트문맨위에있을예정이므로 움직이면안될경우에쓸필요있음

    //로드에서 페이드인아웃시 움직이지않을용도
    private bool fadeCheck;
    private bool fadeInCheck;
    private bool noMoveJump = false;

    //플레이어의본인에서찾아올것들
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
        playerFadeCheck();//페이드인아웃시 움직이지못하게하는부분
        playerDeathMotion();
        playerInvincibilityTime();//피격시 무적시간
        playerTrapHpRemove();//트랩대미지입는부분
        if (playerStop)//플레이어가죽으면 끝
        {
            m_rig2d.velocity = new Vector2(0, m_rig2d.velocity.y);
            playerGravity();//모든 점프를통한 중력값을 여기서정해준다 
            return;
        }
        playerMove();//이동
        playerJump();//모든점프를체크하는곳
        lavaJumpDelay();//용암속점프할때 연속으로눌를때 딜레이
        playerGravity();//모든 점프를통한 중력값을 여기서정해준다 
        playerfloating();//물위에서 떠있을때 알고리즘을담았습니다
        floatingTimeChange();//물위에서떠다닐때 위아래움직임의 시간체크하는부분
        playerChange();//플레이어슬라임타입을 바꾸는곳
        playerChaneTimer();//플레이어 슬라임타입의 쿨타임을정해주는곳
        playerCheckWaterHight();//물줄기를 만나면 솓아오르는용도로만들어줌
        playerHitCheck();//지형에따른피격판정
        playerTrapHitCheck();//트랩히트판정넣는부분
        playerTrapHitInvincibility();
        playerKeyDel();//문열었는지확인후 키삭제불값보내주는용도
        playerHitTime();//맞은뒤 무적시간같은부분
        playerStopCheck();//움직이면안되는부분이있으면추가해줄필요있음
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
        if (m_groundCheck)//땅에있는지 체크하기위한용도
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                m_jumpCheck = true;
            }
        }

        //물에서는 계속물안에있으니까 이걸로체크하는게맞는듯함
        if (GroundType == eGroundType.Water)
        {
            if (playerWaterJump == false)
            {
                if (playerNoContinuityJunp)
                {
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        playerWaterJump = true;//물에서 점프눌렀는지
                        playerWaterCheck = false;//물위에있는지
                        playerNoContinuityJunp = false;//두번점프금지용
                        m_nofloating = true;//물에떠있는코드 안들어게가기용도
                    }

                }

            }
        }

        if (m_inLavaCheck)//용암안속에있는지체크용
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
                if (m_lavaGravityCheck)//용암안에서의 기본중력
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

        if (waterHightCheck)//물최고높이에있을때 다른곳에서 점프력이낮춰주는곳을 전부다체크해서 들어가게해줄것
        {
            m_groundCheck = false;
            m_jumpGravity = m_playerJump;
        }

        if (m_trapGroundHit)//나중에 타입별로나눠줄생각있으면 위에서 각가맞는곳에 넣어서 값바꿔주면될듯
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
            if (HitType == eHitGroundType.Ground)//닿았을떄 피가안다는 슬라임인지체크한다 만약 맞으면 히트시간을 초기화해주는부분각자 괞찮은부분을 추가해주길바람
            {
                hitCheck = false;
                m_hitTimer = 0;
            }
            if (HitType != eHitGroundType.None && HitType != eHitGroundType.Ground)//그린일때 대미지를안입을경우 여기에추가
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
            if (HitType == eHitGroundType.Water || HitType == eHitGroundType.WaterCourse)//닿았을떄 피가안다는 슬라임인지체크한다 만약 맞으면 히트시간을 초기화해주는부분각자 괞찮은부분을 추가해주길바람
            {
                hitCheck = false;
                m_hitTimer = 0;
            }
            if (HitType != eHitGroundType.None && HitType != eHitGroundType.Water)//블루일때 대미지를안입을경우 여기에추가
            {
                hitCheck = true;
            }

        }

        if (PlayerType == eType.Red)
        {
            if (HitType == eHitGroundType.Lava)//닿았을떄 피가안다는 슬라임인지체크한다 만약 맞으면 히트시간을 초기화해주는부분각자 괞찮은부분을 추가해주길바람
            {
                hitCheck = false;
                m_hitTimer = 0;
            }
            if (HitType != eHitGroundType.None && HitType != eHitGroundType.Lava)//레드일때 대미지를안입을경우 여기에추가
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

    //애니메이터용은 이아래로 애니메이터에서 체크해줄부분임 
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
    //여기까지

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
                            floatingTime = 0.5f;//외부에서맞춰준시간대로맞춰줄것
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



    //외부에서 필요한데이터
    public int GetPlayerHp()
    {
        return playerHp;
    }

    //체력깍는용도 벨류값만큼 대미지를입는다 대부분1일듯함h
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

    //안쓸듯 안쓰면삭제해주셈 키부분에서 문이랑 키만쓰면될듯함 
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
