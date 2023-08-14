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
    [SerializeField] private float m_waterSpeed = 2.0f;

    private waterCourse water;
    private Transform m_parentWater;
    private int waterCount;
    private int midWaterCount;
    private List<Transform> m_listWaterMid;
    [SerializeField]private bool midWaterCheck=false;
    private bool midcheck=true;
    private BoxCollider2D box2d;

    Vector2 m_vecTarget;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "WaterHight"&& waterType == eWaterType.Mid)
        {
            midWaterCheck = true;
        }
    }
    void Start()
    {
        water = GetComponentInParent<waterCourse>();
        m_parentWater = transform.root;
        waterCount = water.GetWaterCount();
        m_listWaterMid = water.GetWaterMidList();
        midWaterCount = m_listWaterMid.Count;
        m_vecTarget = new Vector2(transform.position.x, transform.position.y + waterCount-1);
        box2d = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if(waterType== eWaterType.Hight)
        {
            transform.position = Vector2.MoveTowards(transform.position,m_vecTarget, m_waterSpeed * Time.deltaTime);
        }    
        else if(midWaterCheck)
        {
            
            midWaterMove();
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

}
