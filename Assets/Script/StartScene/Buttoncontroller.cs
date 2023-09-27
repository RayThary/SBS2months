using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    private Transform m_start;
    private Transform m_end;
    private Button m_btnStart;
    private Button m_btnEnd;

    void Start()
    {
        DontDestroyOnLoad(this);
        m_start = transform.Find("Start");
        m_end = transform.Find("End");

        m_btnStart = m_start.gameObject.GetComponent<Button>();
        m_btnEnd = m_end.gameObject.GetComponent<Button>();


        m_btnStart.onClick.AddListener(() => startButton(1));
        m_btnEnd.onClick.AddListener(() => endGame());
    }

    private void startButton(int _value)
    {
        SceneManager.LoadSceneAsync((int)_value);
    }

    private void endGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#endif
    }
}
