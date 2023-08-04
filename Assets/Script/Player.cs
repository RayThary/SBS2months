using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float m_speed=2.0f;

    private float m_gravity = 9.81f;
    private float m_jumpGravity = 0f;
    [SerializeField]private float m_playerJump = 5f;
    private bool m_jumpCheck=false;
    private bool m_groundCheck;

    private Rigidbody2D m_rig2d;
    private Vector3 moveDir;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Ground")
        {
            m_groundCheck = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            m_groundCheck = false;
        }
    }

    void Start()
    {
        m_rig2d = GetComponent<Rigidbody2D>();
    }

  
    void Update()
    {
        playerMove();
        playerJump();
        playerGravity();
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
}
