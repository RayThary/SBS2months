using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class TextManager : MonoBehaviour
{
    public static TextManager instance;

    [SerializeField] private TextMeshProUGUI m_talkText;
    [SerializeField] private GameObject textObj;
    private bool m_firstTalk = true;
    private bool m_talking = false;
    private string m_text;
    [SerializeField] private string[] m_talkData;
    [SerializeField] private float typingSpeed = 0.5f;
    [SerializeField] private int m_talkNumber = 0;
    private bool m_textObjCheck;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        m_textObjCheck = false;
        textObj.SetActive(false);
    }

    void Update()
    {
        talkCheck();
        firstTalking();
    }
    private void talkCheck()
    {
        if (m_firstTalk == false)
        {
            return;
        }
        if (GameManager.instance.GetTalkCheck() == true)
        {
            m_textObjCheck = true;
            textObj.SetActive(true);
            StartCoroutine(talk(m_talkData[m_talkNumber]));
            m_firstTalk = false;
        }
    }
    private void firstTalking()
    {
        if (textObj.activeSelf == false)
        {
            return;
        }
        if (m_talking == true)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_talkNumber >= m_talkData.Length)
            {
                m_textObjCheck = false;
                textObj.SetActive(false);
                return;
            }
            StartCoroutine(talk(m_talkData[m_talkNumber]));

        }


    }
    IEnumerator talk(string _Talk)
    {
        m_talking = true;
        int i = 0;
        m_text = "";
        for (i = 0; i < _Talk.Length; i++)
        {
            m_text += _Talk[i];
            m_talkText.text = m_text;
            if (i == _Talk.Length - 1)
            {
                m_talking = false;
                m_talkNumber++;
            }
            yield return new WaitForSeconds(typingSpeed);
        }

    }

    public bool GetTextObj()
    {
        return m_textObjCheck;
    }
}

