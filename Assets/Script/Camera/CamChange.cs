using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamChange : MonoBehaviour
{
    public enum eStage
    {
        Tutorial,
        Stage1,
        Stage2,
        Stage3,
    }
    private eStage stage;
    [SerializeField]private List<Transform> m_LStage = new List<Transform> ();
  

    private CinemachineConfiner2D m_cineConfiner2d;
    private PolygonCollider2D m_poly2d;

    void Start()
    {
        stage = eStage.Tutorial;
        m_cineConfiner2d = GetComponent<CinemachineConfiner2D>();
    }

    // Update is called once per frame
    void Update()
    {
        stageChange();
    }

    private void stageChange()
    {
        if (stage == eStage.Tutorial)
        {
            m_poly2d = m_LStage[0].GetComponent<PolygonCollider2D>();

        }
        else if (stage == eStage.Stage1)
        {
            m_poly2d = m_LStage[1].GetComponent<PolygonCollider2D>();
        }
        else if (stage == eStage.Stage2)
        {
            m_poly2d = m_LStage[2].GetComponent<PolygonCollider2D>();
        }
        else if (stage == eStage.Stage3)
        {
            m_poly2d = m_LStage[3].GetComponent<PolygonCollider2D>();
        }

        m_cineConfiner2d.m_BoundingShape2D = m_poly2d;
       
    }
    
}
