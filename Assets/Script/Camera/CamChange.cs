using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamChange : MonoBehaviour
{

    [SerializeField] private List<Transform> m_LStage = new List<Transform>();// 스테이지를저장해주는곳

    private GameManager.eStage m_stage; // 게임매니저에서 가져올스테이지를알아보는곳
    private CinemachineConfiner2D m_cineConfiner2d;//시네머신
    private PolygonCollider2D m_poly2d;// 시네머신으로 제한을 폴리곤으로해야함 그래서 바뀌게된다면 폴리곤콜라이더를 바꿔주는형태
     
    
    void Start()
    {
        GameManager.instance.SetStage(GameManager.eStage.Tutorial);

        m_cineConfiner2d = GetComponent<CinemachineConfiner2D>();
    }


    void Update()
    {
        stageChange();
    }

    private void stageChange()
    {
        m_stage = GameManager.instance.GetStage();
        if (m_stage == GameManager.eStage.Tutorial)
        {
            m_poly2d = m_LStage[0].GetComponent<PolygonCollider2D>();
        }
        else if (m_stage == GameManager.eStage.Stage1)
        {
            m_poly2d = m_LStage[1].GetComponent<PolygonCollider2D>();
        }
        else if (m_stage == GameManager.eStage.Stage2)
        {
            m_poly2d = m_LStage[2].GetComponent<PolygonCollider2D>();
        }


        m_cineConfiner2d.m_BoundingShape2D = m_poly2d;
    }
   
}
