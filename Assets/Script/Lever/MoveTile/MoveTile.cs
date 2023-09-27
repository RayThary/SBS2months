using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//[ExecuteAlways]
public class MoveTile : MonoBehaviour
{
    [SerializeField] private bool tutorialText;
    private TextMeshPro leverText;
    private Transform leverTextTrs;

	[SerializeField] private float m_MoveMax;
    [SerializeField] private float Speed = 2;
	private Transform m_moveTileTrs;
	

	private Vector3 m_startVec;
	private Vector3 m_endVec;


	private bool moveStart = false;

	private BoxCollider2D m_timeMoveBox2d;
	private bool oneCheck = false;

	private Animator m_anim;
	private BoxCollider2D m_box2d;
	private Transform playerTrs;
    private void OnDrawGizmos()
    {
		Gizmos.color = Color.black;
		Gizmos.DrawLine(m_startVec, new Vector2(m_startVec .x+ m_MoveMax, m_startVec.y));

    }

    void Start()
	{
        leverText = GetComponentInChildren<TextMeshPro>();
        leverTextTrs = GetComponentInChildren<Transform>().Find("LeverText");

        m_anim = GetComponent<Animator>();
		m_moveTileTrs = GetComponent<Transform>().Find("MoveTile");
		m_box2d = GetComponent<BoxCollider2D>();
		m_timeMoveBox2d = m_moveTileTrs.GetComponent<BoxCollider2D>();

		playerTrs = GameManager.instance.GetPlayerTransform();


		m_startVec = m_moveTileTrs.position;
		m_endVec = new Vector3(m_startVec.x + m_MoveMax, m_startVec.y, m_startVec.z);
	}


	void Update()
	{
		playeSetParent();
		leverCheck();
		moveTile();
		leverTutorialText();

    }

    private void leverTutorialText()
    {
        if (tutorialText == false)
        {
            leverTextTrs.gameObject.SetActive(false);
            return;
        }

        if (m_box2d.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            leverText.text = "레버 작동 z키";
        }
        else
        {
            leverText.text = "";
        }

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
