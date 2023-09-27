using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveTrap : MonoBehaviour
{
    [Header("위아래로움직이면 체크 : 아래에서 시작해야함 \n좌우로움직이면 체크해제 :왼쪽에서 시작해야함")]
    [SerializeField] private bool m_MoveType;
    [SerializeField] private float m_MoveMax;
    [SerializeField] private float m_MoveSpeed;

    private bool m_endVecCheck = false;

    private Vector3 m_startVec;
    private Vector3 m_endVec;

    
    private Player m_player;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        if (m_MoveType)
        {
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + m_MoveMax));
        }
        else
        {
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + m_MoveMax, transform.position.y));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            m_player.SetPlayerTrapHit(true);
            m_player.SetTrapHpRemove(true);

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            m_player.SetTrapHpRemove(false);

        }
    }

    void Start()
    {
        Transform playerTrs = GameManager.instance.GetPlayerTransform();
        m_player = playerTrs.GetComponent<Player>();

        m_startVec = transform.position;
        if (m_MoveType)
        {
            m_endVec = new Vector3(m_startVec.x, m_startVec.y + m_MoveMax, m_startVec.z);
        }
        else
        {
            m_endVec = new Vector3(m_startVec.x + m_MoveMax, m_startVec.y, m_startVec.z);
        }
    }


    void Update()
    {
        trapRightAndLeftMove();
        trapUpAndDownMove();
    }
    private void trapRightAndLeftMove()
    {
        if (m_MoveType)
        {
            return;
        }
        if (m_endVecCheck == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, m_endVec, m_MoveSpeed * Time.deltaTime);
            if (transform.position.x >= m_endVec.x)
            {
                m_endVecCheck = true;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, m_startVec, m_MoveSpeed * Time.deltaTime);
            if (transform.position.x <= m_startVec.x)
            {
                m_endVecCheck = false;
            }
        }
    }
    private void trapUpAndDownMove()
    {
        if (m_MoveType == false)
        {
            return;
        }

        if (m_endVecCheck == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, m_endVec, m_MoveSpeed * Time.deltaTime);
            if (transform.position.y >= m_endVec.y)
            {
                m_endVecCheck = true;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, m_startVec, m_MoveSpeed * Time.deltaTime);
            if (transform.position.y <= m_startVec.y)
            {
                m_endVecCheck = false;
            }
        }
    }

}
