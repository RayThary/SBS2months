using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("테스트용 hit 로 체력감소만안달게하기")]
    public bool NoHit=false;
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
    }
    public enum eHitGroundType//추가바람
    {
        None,
        Ground,
        Lava,
        Water,
    }

    [SerializeField]private eGroundType GroundType;
    private eType PlayerType;
    private eHitGroundType HitType;
    
    [SerializeField,Range(0,3)]private int playerHp = 3;
    //플레이어 슬라임 변경딜레이시간
    [SerializeField]private float changeCoolTime=3.0f;
    private float changeTimer=0.0f;
    private bool m_playerChangeCoolTime;

    private bool playerRedCheck=false;
    private bool playerBlueCheck=false;
    private bool playerGreenCheck=true;
    //물에있는지 확인용
    private bool playerWaterCheck=false;
    //물위에서 위아래도 떠다니는시간 내부에서 세부조절할필요도있음 1을변경해서 조정도가능
    [Header("물위에서있는기능")]
    [SerializeField] private float floatingTimer = 1.0f;
    private float floatingTime = 0f;
    [SerializeField] private float floatingMovingMax=0.2f;//물떠다니느 최대최소값 
    private float floatingMoving = 1f;
    private bool floatingChange = false;
    private bool playerWaterJump;
    private bool m_groundWaterCheck=false;

    private bool m_isWaterCourse;
    


    //땅&기본기능
    [SerializeField] private float m_speed=2.0f;
    private float m_gravity = 9.81f;
    private float m_jumpGravity = 0f;
    [SerializeField]private float m_playerJump = 5f;//플레이어의 점프력
    [SerializeField] private float m_playerInLavaJump = 3f;//용암속에서의 점프력 아마 저항있는부분에서의 점프력일듯 다른곳에서 사용시 이름바꿀필요가있음
    private bool m_jumpCheck=false;
    private bool m_groundCheck;


    //용암
    private bool m_groundLavaCheck;//용암에서의 점프를담당할예정근데아마 용암밖에나갔을때 써줄예정
    [SerializeField]private bool m_inLavaCheck;//용암안에있는지체크용 용암안에있을때 중력값정해줄용도의불값
    private bool m_lavaJumpCheck;//용암안에서의 점프를할거같음 
    private bool m_lavaGravityCheck;


    //대미지입는용도 부분
    [SerializeField] private float m_HitTime = 3.0f;
    private float m_hitTimer = 0.0f;
    private bool hitCheck;

    //아이템부분 많이쓸거같으면 json으로 인벤토리구현을해줄필요있음 아니면 불값으로 on/off체크해주는방법이좋을듯함
    private bool PlayerIsKey = false;
    private bool doorLockisOpen = false;
    private bool doorKeyCheck = false;

    private BoxCollider2D m_box2d;
    private Animator m_anim;
    private Rigidbody2D m_rig2d;
    private Vector3 moveDir;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Ground")
        {
            GroundType = eGroundType.Ground;
            HitType = eHitGroundType.Ground;
            m_groundCheck = true;

        }
        else if (collision.gameObject.tag == "Water")
        {
            GroundType = eGroundType.Water;
            HitType = eHitGroundType.Water;
            m_groundWaterCheck = true; 
        }
        else if (collision.gameObject.tag == "Lava")
        {
            GroundType = eGroundType.Lava;
            HitType = eHitGroundType.Lava;
            //m_inLavaCheck= true;
        
        }

       
    }

  
   

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            HitType = eHitGroundType.None;
            m_groundCheck = false;
        }
        if(collision.gameObject.tag == "Water")
        {
            HitType = eHitGroundType.None;
            m_groundWaterCheck = false;
        }
        if (collision.gameObject.tag == "Lava")
        {
            HitType = eHitGroundType.None;
            //m_inLavaCheck = false;
        }
    }

    void Start()
    {
        floatingTime = floatingTimer;
        m_rig2d = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
        m_box2d = GetComponent<BoxCollider2D>();
    }

  
    void Update()
    {
        playerDeathMotion();
        if (playerHp == 0)//플레이어가죽으면 끝
        {
            return;
        }
        playerInWater();//물안에있는지?
        playerMove();//이동
        playerJump();//모든점프를체크하는곳
        playerGravity();//모든 점프를통한 중력값을 여기서정해준다 
        //playerWaterGravity();//없는듯한데 왜있지
        playerfloating();//물위에서 떠있을때 알고리즘을담았습니다
        floatingTimeChange();//물위에서떠다닐때 위아래움직임의 시간체크하는부분
        playerLavefloating();//용암에있을때 어떻게떠있을지 
        playerChange();//플레이어슬라임타입을 바꾸는곳
        playerChaneTimer();//플레이어 슬라임타입의 쿨타임을정해주는곳
        playerCheckWaterHight();//물줄기를 만나면 솓아오르는용도로만들어줌
        playerHitCheck();//피격판정
        playerKeyDel();
        playerHitTime();//맞은뒤 무적시간같은부분
    }


    private void playerMove()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDir.x = 1;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            moveDir.x = 0;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDir.x = -1;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            moveDir.x = 0;
        }
        if (m_inLavaCheck)
        {
            m_rig2d.velocity = moveDir *0.5f* m_speed;
        }
        else
        {
            m_rig2d.velocity = moveDir * m_speed;
        }
    }

    private void playerJump()
    {
        if (m_groundCheck)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                m_jumpCheck = true;
            }
        }

        if (playerWaterCheck)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                playerWaterJump = true;
            }
        }

        if (m_inLavaCheck)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                m_lavaJumpCheck = true;
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
        else if(GroundType == eGroundType.Water)
        {
          
            if (!m_groundWaterCheck)
            {
                m_jumpGravity -= m_gravity * Time.deltaTime;
                if (m_jumpGravity < -10f)
                {
                    m_jumpGravity = -10f;
                }
            }
            else
            {
                if (playerWaterJump)
                {
                    Invoke("delayWaterJump", 0.5f);
                    playerWaterCheck = false;
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
        else if(GroundType == eGroundType.Lava)
        {
            if (m_inLavaCheck)
            {
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
        if (m_isWaterCourse)
        {
            m_jumpCheck =false;
            playerWaterJump = false;
            m_jumpGravity = m_playerJump;
        }
        
        m_rig2d.velocity = new Vector2(m_rig2d.velocity.x, m_jumpGravity);
    }

    private void playerInWater()
    {
        if (m_box2d.IsTouchingLayers(LayerMask.GetMask("Water")))
        {
            playerWaterCheck = true;
        }
        else
        {
            playerWaterCheck = false;
        }
    }
    
    private void delayWaterJump()
    {
        playerWaterJump = false;
    }
    private void playerfloating()
    {
        if (playerWaterJump)
        {
            return;
        }
        if (GroundType == eGroundType.Water)
        {
            if (playerWaterCheck)
            {
                m_groundWaterCheck = true;

                if (floatingTime >= floatingTimer)
                {
                    floatingMoving = -floatingMovingMax;
                    floatingChange = true;
                }
                else if (floatingTime <= -floatingTimer)
                {
                    floatingMoving = floatingMovingMax;
                    floatingChange = false;
                }
                m_rig2d.velocity = new Vector2(m_rig2d.velocity.x, floatingMoving);
            }
        }
    }

    private void floatingTimeChange()
    {
        if (playerWaterCheck)
        {
            if (floatingChange)
            {
                floatingTime -= Time.deltaTime;
            }
            if (!floatingChange)
            {
                floatingTime += Time.deltaTime;
            }
        }
    }

    
    private void playerLavefloating()
    {
        RaycastHit2D lavaHit = Physics2D.Raycast(transform.position, Vector2.zero, 0, LayerMask.GetMask("Lava"));
        if (lavaHit.collider != null)
        {
            m_inLavaCheck = true;
        }
        else
        {
            m_inLavaCheck = false;
        }
        if (m_inLavaCheck) 
        {
            if (m_lavaGravityCheck)
            {
                m_rig2d.velocity = new Vector2(m_rig2d.velocity.x, -0.4f);
            }
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

    private void playerCheckWaterHight()
    {
        if (m_box2d.IsTouchingLayers(LayerMask.GetMask("WaterCourse")))
        {
            m_rig2d.velocity = new Vector2(m_rig2d.velocity.x, 1.5f);
            m_jumpCheck = false;
            playerWaterJump = false;
        }

        if (m_box2d.IsTouchingLayers(LayerMask.GetMask("WaterHight")))
        {
            m_jumpCheck = false;
            playerWaterJump = false;
            m_isWaterCourse = true;
        }
        else
        {
            m_isWaterCourse = false;
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
            if (HitType != eHitGroundType.None || HitType != eHitGroundType.Ground)//그린일때 대미지를안입을경우 여기에추가
            {
                hitCheck = true;
            }
        }

        if (PlayerType == eType.Blue)
        {
            if (HitType == eHitGroundType.Water)//닿았을떄 피가안다는 슬라임인지체크한다 만약 맞으면 히트시간을 초기화해주는부분각자 괞찮은부분을 추가해주길바람
            {
                hitCheck = false;
                m_hitTimer = 0;
            }
            if (HitType != eHitGroundType.None || HitType != eHitGroundType.Water)//블루일때 대미지를안입을경우 여기에추가
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
            if (HitType != eHitGroundType.None || HitType != eHitGroundType.Lava)//레드일때 대미지를안입을경우 여기에추가
            {
                hitCheck = true;
            }
        }
    }


    private void playerHitTime()
    {
        if (!hitCheck)
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

    private void playerDeathMotion()
    {
        if (playerHp == 0)
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
        }
    }

    //애니메이터용은 이아래로 애니메이터에서 체크해줄부분임 
    private void playerDeath()
    {
        Destroy(gameObject);// 플레이어 데스모션에서 죽음모션일때 마지막이벤트에넣어놈 
    }

    private void playerHit()
    {
        if (NoHit)
        {
            return;
        }
        playerHp -= 1;
    }
    //여기까지

    public void OnTriggerPlayer(PlayerHitBox.HitBoxType _state, PlayerHitBox.HitType _hitType, Collider2D _collision)
    {
        switch (_state)
        {
            case PlayerHitBox.HitBoxType.Enter:
                switch (_hitType)
                {
                    case PlayerHitBox.HitType.Item:
                        if (_collision.gameObject.layer == LayerMask.NameToLayer("Key"))
                        {
                            PlayerIsKey = true;
                        }
                        if (_collision.gameObject.layer == LayerMask.NameToLayer("DoorLock"))
                        {
                            doorLockisOpen = true;
                        }
                            break;
                }
                break;

            
            case PlayerHitBox.HitBoxType.Exit:
                switch (_hitType)
                {
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
    public int PlayerHp()
    {
        return playerHp;
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
}
