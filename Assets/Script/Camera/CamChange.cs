using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamChange : MonoBehaviour
{
    private CinemachineConfiner2D m_cineConfiner2d;
    [SerializeField]private List<Transform>m_mapList = new List<Transform>();
    private PolygonCollider2D m_poly2d;

    void Start()
    {
        m_cineConfiner2d = GetComponent<CinemachineConfiner2D>();
        m_poly2d = m_mapList[1].GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        m_cineConfiner2d.m_BoundingShape2D = m_poly2d;
        //안됨 다른거찾아볼이유있음
    }
}
