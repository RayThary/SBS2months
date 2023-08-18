using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMove : MonoBehaviour
{
    public enum eWaterType
    {
        Hight,
        Mid,
        Low,
    }
    private eWaterType waterType;
    //물관련
    [SerializeField] private float m_waterSpeed = 2.0f;

    private waterCourse water;
    private Transform m_parentWater;
    private Animator m_anim;

    private int waterCount;
    private int ListWaterCount;
    private List<Transform> m_listWater;
    private bool midcheck=true;
    private bool m_stopWaterMove=false;
    private bool m_readyWater;
    //물떨어지는시간
    [SerializeField]private float m_waterFallSpeed = 1f;
    private int m_waterFallTimer;
    //여기까지

    private Animator anim;

    //플레이어 관련
    private Transform m_trsPlayer;
    private Rigidbody2D m_rig2dPlayer;
    //여기까지
    private Rigidbody2D m_rig2d;
    private BoxCollider2D m_box2d;

    Vector2 m_vecTarget;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "WaterEnd")
        {
            m_waterFallSpeed = 0;

            if (waterType == eWaterType.Hight)
            {
                m_readyWater = true;
                m_anim.SetBool("Lever", false);
            }
        }
    }
    void Start()
    {
        m_trsPlayer = GameManager.instance.GetPlayerTransform();
        m_rig2dPlayer = m_trsPlayer.GetComponent<Rigidbody2D>();

        water = GetComponentInParent<waterCourse>();
        m_parentWater = transform.root;
        waterCount = water.GetWaterCount();
        m_listWater = water.GetWaterMidList();
        m_waterFallTimer = water.GetWaterFallTimer();
        m_anim = water.GetComponent<Animator>();

        ListWaterCount = m_listWater.Count;
        m_vecTarget = new Vector2(transform.position.x, transform.position.y + waterCount-1);
        //m_vecTarget = new Vector2(transform.position.x, transform.position.y + m_listWater.IndexOf(transform));

        waterTypeCheck();


        m_box2d = GetComponent<BoxCollider2D>();
        m_rig2d = GetComponent<Rigidbody2D>();
    }

    private void waterTypeCheck()
    {
        if (transform == m_listWater[0])
        {
            waterType = eWaterType.Hight;
        }
        else if (transform == m_listWater[waterCount - 1])
        {
            waterType = eWaterType.Low;
        }
        else
        {
            waterType = eWaterType.Mid;
        }

    }

    void Update()
    {
        waterMove();
        playerWaterMove();
        
    }

    private void waterMove()
    {
        if (!m_stopWaterMove)
        {
            WaterMoving();
        }
        Invoke("fallWaterCheck", m_waterFallTimer);
        
    }

    

    private void WaterMoving()
    {
        if (waterType == eWaterType.Hight)
        {
            transform.position = Vector2.MoveTowards(transform.position, m_vecTarget, m_waterSpeed * Time.deltaTime);
            if(transform.position.y== m_vecTarget.y)
            {
                m_stopWaterMove = true;
            }
        }
        
        if (waterType == eWaterType.Mid)
        {
            Invoke("WaterTargetMove", 0.4f);
            //transform.position = Vector2.MoveTowards(transform.position, m_vecTarget, m_waterSpeed * Time.deltaTime);
        }

        
    }

    private void WaterTargetMove()
    {

        for (int i = 1; i < ListWaterCount; i++)
        {
            if (midcheck)
            {
                if (transform == m_listWater[i])
                {
                    m_vecTarget = new Vector2(m_vecTarget.x, m_vecTarget.y - (i));
                    midcheck = false;
                }
            }
        }
        
        transform.position = Vector2.MoveTowards(transform.position, m_vecTarget, m_waterSpeed * Time.deltaTime);
    }
    private void lateWaterMove()
    {
        m_rig2d.velocity = new Vector2(m_rig2d.velocity.x, -m_waterFallSpeed);

    }

    private void fallWaterCheck()
    {
        m_stopWaterMove = true;
        lateWaterMove();
    }
    private void playerWaterMove()
    {
        //선행조건 waterhight 일땐 다른조건주기
        if (m_box2d.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            m_rig2dPlayer.velocity = new Vector2(m_rig2dPlayer.velocity.x, 1.5f);
        }
    }
}
