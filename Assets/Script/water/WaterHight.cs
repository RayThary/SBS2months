using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHight : MonoBehaviour
{
    public enum eWaterType
    {
        Hight,
        Mid,
        Low,
    }
    public eWaterType waterType;
    //물관련
    [SerializeField] private float m_waterSpeed = 2.0f;

    private waterCourse water;
    private Transform m_parentWater;
    private Animator m_anim;

    private int waterCount;
    private int midWaterCount;
    private List<Transform> m_listWaterMid;
    private bool midcheck=true;
    private bool m_stopWaterMove=false;
    private bool m_waterFall = false;
    //여기까지

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
            Destroy(gameObject);
        }
    }
    void Start()
    {
        m_trsPlayer = GameManager.instance.GetPlayerTransform();
        m_rig2dPlayer = m_trsPlayer.GetComponent<Rigidbody2D>();

        water = GetComponentInParent<waterCourse>();
        m_parentWater = transform.root;
        waterCount = water.GetWaterCount();
        m_listWaterMid = water.GetWaterMidList();
        m_anim = water.GetComponent<Animator>();

        midWaterCount = m_listWaterMid.Count;
        m_vecTarget = new Vector2(transform.position.x, transform.position.y + waterCount-1);

        m_box2d = GetComponent<BoxCollider2D>();
        m_rig2d = GetComponent<Rigidbody2D>();
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

        if (m_anim.GetBool("Lever") == false)
        {
            m_stopWaterMove = true;
            if (waterType != eWaterType.Low)
            {
                lateWaterMove();
            }
            else
            {
                Invoke("lateWaterMove", 1f);
            }
        }
    }
    private void lateWaterMove()
    {
        m_rig2d.velocity = new Vector2(m_rig2d.velocity.x, -1);
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
            Invoke("midWaterMove", 0.4f);
        }

        
    }

    private void midWaterMove()
    {
        if (midcheck)
        {
            for (int i = 0; i < midWaterCount; i++)
            {
                if (transform == m_listWaterMid[i])
                {
                    Vector2 targer = new Vector2(m_vecTarget.x, m_vecTarget.y - (i + 1));
                    m_vecTarget = targer;
                    midcheck = false;
                    break;
                }
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, m_vecTarget, m_waterSpeed * Time.deltaTime);
    }

    private void playerWaterMove()
    {
        if (m_box2d.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            //m_rig2dPlayer.velocity = new Vector2()
        }
    }
}
