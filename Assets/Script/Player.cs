using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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

    private eGroundType GroundType;
    private eType PlayerType;

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
    private float m_waterHightJumping = 3.0f;

    [SerializeField] private float m_speed=2.0f;
    private float m_gravity = 9.81f;
    private float m_jumpGravity = 0f;
    [SerializeField]private float m_playerJump = 5f;//점프력
    private bool m_jumpCheck=false;
    private bool m_groundCheck;


    private BoxCollider2D m_box2d;
    private Animator m_anim;
    private Rigidbody2D m_rig2d;
    private Vector3 moveDir;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Ground")
        {
            GroundType = eGroundType.Ground;


            m_groundCheck = true;

        }
        else if (collision.gameObject.tag == "Water")
        {
            GroundType = eGroundType.Water;
   
            m_groundWaterCheck = true;

                   
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            m_groundCheck = false;
        }
        if(collision.gameObject.tag == "Water")
        {
            m_groundWaterCheck = false;
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
        playerInWater();
        playerMove();
        playerJump();
        playerGravity();
        //playerWaterGravity();
        playerfloating();
        floatingTimeChange();
        playerChange();
        playerChaneTimer();
        playerCheckWaterHight();
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

        m_rig2d.velocity = moveDir * m_speed;
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
        if (m_isWaterCourse)
        {
            m_jumpCheck = true;
            playerWaterJump = true;
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
        }

        if (m_box2d.IsTouchingLayers(LayerMask.GetMask("WaterHight")))
        {
            m_isWaterCourse = true;
        }
        else
        {
            m_isWaterCourse = false;
        }
    }

    private void playerDeath()
    {
        Destroy(gameObject);
    }
    
    private void playerHit()
    {
        //플레이어 hp 감소시켜줄곳 
        //아마 이미지로 -1되면 한개씩없어지게만들듯함 +1되면 하트가 다시생기는느낌? 
    }
}
