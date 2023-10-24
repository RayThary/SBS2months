using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartTextKey : MonoBehaviour
{
    private TextMeshPro m_textMesh;
    private float m_textFontSize = 7f;
    private bool m_fontChange = true;
    void Start()
    {
        m_textMesh = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        fontSizeChange();
    }
    private void fontSizeChange()
    {
        
        if (m_textFontSize <= 7)
        {
            m_fontChange = true;

        }
        else if (m_textFontSize >= 8)
        {
            m_fontChange = false;
        }

        if (m_fontChange)
        {
            m_textFontSize += Time.deltaTime;
        }
        else
        {
            m_textFontSize-= Time.deltaTime;
        }
        m_textMesh.fontSize = m_textFontSize;
    }
}
