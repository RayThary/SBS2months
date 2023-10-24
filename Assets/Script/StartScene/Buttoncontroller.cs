using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonController : MonoBehaviour
{
    private Transform m_start;
    private Transform m_end;
    private Button m_btnStart;
    private Button m_btnEnd;
    [SerializeField] private GameObject m_StartText;
    [SerializeField] private GameObject m_EndText;
    [SerializeField] private AudioClip m_btnClip;

    private Transform m_trsParent;
    [SerializeField]private Image m_imgParent;


    void Start()
    {
        m_trsParent = transform.parent;
        m_imgParent = m_trsParent.GetComponent<Image>();
        m_start = transform.Find("Start");
        m_end = transform.Find("End");

        m_btnStart = m_start.gameObject.GetComponent<Button>();
        m_btnEnd = m_end.gameObject.GetComponent<Button>();

        m_StartText.SetActive(false);
        m_EndText.SetActive(false);

        m_btnStart.onClick.AddListener(() => startButton(1));
        m_btnEnd.onClick.AddListener(() => endGame());
    }
    private void startButton(int _value)
    {
        SceneManager.LoadSceneAsync((int)_value);
        SoundManager.instance.SFXPlay(m_btnClip);
    }

    private void endGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#endif
    }

    private void Update()
    {
        
        if (m_imgParent.color.a >= 1)
        {
            m_StartText.SetActive(true);
            m_EndText.SetActive(true);
        }
    }
}
