using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamChange : MonoBehaviour
{

    [SerializeField] private List<Transform> m_LStage = new List<Transform>();// �����������������ִ°�

    private GameManager.eStage m_stage; // ���ӸŴ������� �����ý����������˾ƺ��°�
    private CinemachineConfiner2D m_cineConfiner2d;//�ó׸ӽ�
    private PolygonCollider2D m_poly2d;// �ó׸ӽ����� ������ �����������ؾ��� �׷��� �ٲ�Եȴٸ� �������ݶ��̴��� �ٲ��ִ�����
     
    
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
