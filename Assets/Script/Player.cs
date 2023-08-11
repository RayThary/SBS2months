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

    [Header("체크용 나중에삭제필요")]
    [SerializeField]private eType PlayerType;

    [SerializeField]private float changeCoolTime=3.0f;
    private float changeTimer=0.0f;
    private bool m_playerChangeCoolTime;

    private bool playerRedCheck=false;
    private bool playerBlueCheck=false;
    private bool playerGreenCheck=true;

    [SerializeField]private bool playerWaterCheck=false;
    [SerializeField]private float floatingTime = 1f;

    [SerializeField] private float floatingMovingMax=0.2f;
    [SerializeField]private float floatingMoving = 1f;
    private bool floatingChange = false;
    

    [SerializeField] private float m_speed=2.0f;
    private float m_gravity = 9.81f;
    private float m_jumpGravity = 0f;
    [SerializeField]private float m_playerJump = 5f;
    private bool m_jumpCheck=false;
    private bool m_groundCheck;


    private Animator m_anim;
    private Rigidbody2D m_rig2d;
    private Vector3 moveDir;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Ground")
        {
            m_groundCheck = true;
        }
        else if (collision.gameObject.tag == "Water")
        {
            if(PlayerType == eType.Blue)
            {
                playerWaterCheck = true;
            }
            else
            {
                //나중에 히트기능만들어주기
            }
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
            playerWaterCheck = false;
        }
    }

    void Start()
    {
        m_rig2d = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
    }

  
    void Update()
    {
        playerMove();
        playerJump();
        playerGravity();
        playerfloating();
        floatingTimeChange();
        playerChange();
        playerChaneTimer();
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
        if (!m_groundCheck)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            m_jumpCheck = true;
        }
    }

    private void playerGravity()
    {
        

        if (playerWaterCheck)
        {
            return;
        }
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
                if(m_jumpGravity > 0)
                {
                    m_jumpGravity = 0;
                }

            }
        }
        m_rig2d.velocity = new Vector2(m_rig2d.velocity.x, m_jumpGravity);
    }

    private void playerfloating()
    { 
        if (playerWaterCheck)
        {  
            if (floatingTime >= 1)
            {
                floatingMoving = -floatingMovingMax;
                floatingChange = true;
            }
            if (floatingTime <= -1)
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
                floatingTime -= Time.deltaTime*4;
            }
            if (!floatingChange)
            {
                floatingTime += Time.deltaTime*4;
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

    
}
