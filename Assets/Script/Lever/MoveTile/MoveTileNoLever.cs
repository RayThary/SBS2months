using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveTileNoLever : MonoBehaviour
{
    [Header("위아래 체크 / 좌우 체크해제")]
    [SerializeField] private bool up;
    [SerializeField] private float m_MoveMax;
    [SerializeField] private float Speed = 2;
    private Transform m_moveTileTrs;


    private Vector3 m_startVec;
    private Vector3 m_endVec;


    private bool moveStart = true;

    private bool oneCheck = false;

    private Animator m_anim;
    private BoxCollider2D m_box2d;
    private Transform playerTrs;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        if (up)
        {
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + m_MoveMax));
        }
        else
        {
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + m_MoveMax, transform.position.y));
        }

    }

    void Start()
    {

        m_anim = GetComponent<Animator>();
        m_moveTileTrs = GetComponent<Transform>();
        m_box2d = GetComponent<BoxCollider2D>();

        playerTrs = GameManager.instance.GetPlayerTransform();


        m_startVec = transform.position;
        if (up)
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
        playeSetParent();
        moveTile();
    }


    private void playeSetParent()
    {
        if (m_box2d.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            playerTrs.parent = m_moveTileTrs;
            oneCheck = true;
        }
        else
        {
            if (oneCheck)
            {
                playerTrs.parent = null;
                oneCheck = false;
            }
        }
    }



    private void moveTile()
    {
        if (up)
        {
            if (moveStart)
            {
                transform.position = Vector2.MoveTowards(transform.position, m_endVec, Speed * Time.deltaTime);
                if (transform.position.y >= m_endVec.y)
                {
                    moveStart = false;
                }
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, m_startVec, Speed * Time.deltaTime);
                if (transform.position.y <= m_startVec.y)
                {
                    moveStart = true;
                }
            }

        }
        else
        {
            if (moveStart)
            {
                transform.position = Vector2.MoveTowards(transform.position, m_endVec, Speed * Time.deltaTime);
                if (transform.position.x >= m_endVec.x)
                {
                    moveStart = false;
                }
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, m_startVec, Speed * Time.deltaTime);
                if (transform.position.x <= m_startVec.x)
                {
                    moveStart = true;
                }
            }
        }
    }
}
