using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StartBackGroundMove : MonoBehaviour
{


    [SerializeField] private Image imgSign;
    [SerializeField] private TextMeshProUGUI textTitle;
    private TextMeshPro m_textMesh;
    private Transform m_backGroudTrs;
    private Vector3 m_targetVec;

    private bool m_firstCheck = false;
    private bool m_moveEnd = false;
    private bool m_signColorCheck = false;
    [SerializeField] private float moveSpeed = 1;
    void Start()
    {
        m_textMesh = GetComponentInChildren<TextMeshPro>();
        m_backGroudTrs = GetComponent<Transform>();
        m_targetVec = m_backGroudTrs.position;
        m_targetVec.y = m_targetVec.y - 5;
    }

    // Update is called once per frame
    void Update()
    {
        anyKeyCheck();
        backGroundMove();
        SeeSignAndTitle();
    }

    private void anyKeyCheck()
    {

        if (m_firstCheck == false && m_moveEnd == false)
        {
            if (Input.anyKeyDown)
            {
                m_firstCheck = true;
                m_textMesh.gameObject.SetActive(false);
            }
        }

    }
    private void backGroundMove()
    {
        if (m_firstCheck)
        {
            m_backGroudTrs.position = Vector2.MoveTowards(m_backGroudTrs.position, m_targetVec, moveSpeed * Time.deltaTime);
            if (m_backGroudTrs.position.y <= m_targetVec.y)
            {
                m_moveEnd = true;
                m_firstCheck = false;
            }
        }
    }

    private void SeeSignAndTitle()
    {
        if (m_moveEnd && m_signColorCheck == false)
        {

            Color signColor = imgSign.color;
            signColor.a += Time.deltaTime * 0.5f;
            Color titleColor = textTitle.color;
            titleColor.a += Time.deltaTime * 0.5f;

            textTitle.color = titleColor;
            imgSign.color = signColor;

            if (signColor.a >= 1 && titleColor.a >= 1)
            {
                m_signColorCheck = true;
            }
        }
    }

}
