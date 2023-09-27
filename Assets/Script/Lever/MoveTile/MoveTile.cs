using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTile : MonoBehaviour
{
    [SerializeField] private float Speed = 2;
    private Transform m_moveTileTrs;
    private Transform m_startTrs;
    private Transform m_endTrs;

    private Vector3 m_startVec;
    private Vector3 m_endVec;

    private bool moveStart = false;

    private BoxCollider2D m_timeMoveBox2d;
    private bool oneCheck = false;

    private Animator m_anim;
    private BoxCollider2D m_box2d;
    private Transform playerTrs;
    void Start()
    {
        m_anim = GetComponent<Animator>();
        m_moveTileTrs = GetComponent<Transform>().Find("MoveTile");
        m_startTrs = GetComponent<Transform>().Find("StartPos");
        m_endTrs = GetComponent<Transform>().Find("EndPos");
        m_box2d = GetComponent<BoxCollider2D>();
        m_timeMoveBox2d = m_moveTileTrs.GetComponent<BoxCollider2D>();

        playerTrs = GameManager.instance.GetPlayerTransform();


        m_startVec = m_startTrs.position;
        m_endVec = m_endTrs.position;
    }


    void Update()
    {
        playeSetParent();
        leverCheck();
        moveTile();

    }

    private void playeSetParent()
    {
        if (m_timeMoveBox2d.IsTouchingLayers(LayerMask.GetMask("Player")))
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

    private void leverCheck()
    {
        if (m_anim.GetBool("TileLever") == true)
        {
            return;
        }

        if (m_box2d.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                m_anim.SetBool("TileLever", true);
                moveStart = true;
            }
        }
    }

    private void moveTile()
    {
        if (m_anim.GetBool("TileLever") == false)
        {
            return;
        }
        if (moveStart)
        {
            m_moveTileTrs.position = Vector3.MoveTowards(m_moveTileTrs.position, m_endVec, Speed * Time.deltaTime);
            if (m_moveTileTrs.position.x >= m_endVec.x)
            {
                moveStart = false;
            }
        }
        else
        {
            m_moveTileTrs.position = Vector3.MoveTowards(m_moveTileTrs.position, m_startVec, Speed * Time.deltaTime);
            if (m_moveTileTrs.position.x <= m_startVec.x)
            {
                m_anim.SetBool("TileLever", false);
            }
        }
    }

}
