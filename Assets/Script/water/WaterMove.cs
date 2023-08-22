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
    [SerializeField]private eWaterType waterType;
    //물관련
    [SerializeField] private float m_waterSpeed = 2.0f;

    private waterCourse water;
    private Transform m_parentWater;
    private Animator m_anim;

    private int waterCount;
    private int ListWaterCount;
    private List<Transform> m_listWater;
    private bool midcheck=true;
    [SerializeField]private bool m_stopWaterMove=false;
    //물떨어지는시간
    [SerializeField]private float m_waterFallSpeed = 1f;
    private int m_waterFallTimer;
    [SerializeField]private bool waterFallCheck;
    //여기까지

    private bool leverReadyCheck;
    private bool checkWaterEnd;

    //플레이어 관련
    private Transform m_trsPlayer;
    
    //여기까지
    private Rigidbody2D m_rig2d;

    Vector2 m_vecTarget;
    Vector2 m_vecLowTarger;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "WaterEnd")
        {
            m_rig2d.velocity = new Vector2(m_rig2d.velocity.x, 0);
            checkWaterEnd = true;
        }
    }

    void Start()
    {
        
            waterFallCheck = true;
        m_trsPlayer = GameManager.instance.GetPlayerTransform();
        

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

        if (waterType == eWaterType.Low)
        {
            m_vecLowTarger = new Vector2(transform.position.x, transform.position.y);
        }

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
        leverCheck();
    }

    private void waterMove()
    {
        if (m_stopWaterMove) 
        {
            return;
        }
            WaterMoving();
        StartCoroutine("fallWater");
    }

    IEnumerator fallWater()
    {
        if (waterFallCheck)
        {
            waterFallCheck = false;
            leverReadyCheck = true;
            yield return new WaitForSeconds(m_waterFallTimer);
            m_rig2d.velocity = new Vector2(m_rig2d.velocity.x, -m_waterFallSpeed);
        }
    }

    private void leverCheck()
    {

        if (m_anim.GetBool("Lever") == true && leverReadyCheck && checkWaterEnd)
        {
            waterFallCheck = true;
            checkWaterEnd = false;
            leverReadyCheck = false;
            m_stopWaterMove = false;
        }
        
        
    }
    private void WaterMoving()
    {
        if (waterType == eWaterType.Hight)
        {
            transform.position = Vector2.MoveTowards(transform.position, m_vecTarget, m_waterSpeed * Time.deltaTime);
            if (transform.position.y >= m_vecTarget.y) 
            {
                m_stopWaterMove = true;
            }
        }
        
        if (waterType == eWaterType.Mid)
        {
            Invoke("WaterTargetMove", 0.4f);
        }

        if(waterType == eWaterType.Low) 
        {
            transform.position = Vector2.MoveTowards(transform.position, m_vecLowTarger, m_waterSpeed * Time.deltaTime);
            if (transform.position.y >= m_vecLowTarger.y)
            {
                m_stopWaterMove = true;
            }
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
        if (transform.position.y >= m_vecTarget.y)
        {
            m_stopWaterMove = true;
        }
        transform.position = Vector2.MoveTowards(transform.position, m_vecTarget, m_waterSpeed * Time.deltaTime);
    }
   
 
    
  
    
}
