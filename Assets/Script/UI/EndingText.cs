using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingText : MonoBehaviour
{
    private List<TextMeshProUGUI> m_text = new List<TextMeshProUGUI>();
    private float alphaValue = 0;
    private float alpha1Value = 0;

    private float timer = 0;
    void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            m_text.Add(transform.GetChild(i).GetComponent<TextMeshProUGUI>());
            m_text[i].alpha = 0;
            m_text[i].gameObject.SetActive(false);
        }

        m_text[0].gameObject.SetActive(true);
    }


    void Update()
    {
        if (m_text[0].gameObject.activeSelf == true)
        {
            timer += Time.deltaTime;
            if (timer >= 2.5f)
            {
                alphaValue -= Time.deltaTime * 0.5f;
            }
            else
            {
                alphaValue += Time.deltaTime * 0.5f;
            }
            m_text[0].alpha = alphaValue;

            if (timer >= 2.5f)
            {
                if (alphaValue <= 0)
                {
                    m_text[0].gameObject.SetActive(false);
                    m_text[1].gameObject.SetActive(true);
                    timer = 0;
                    alphaValue = 0;
                }
            }
        }

        if (m_text[1].gameObject.activeSelf == true)
        {
            timer += Time.deltaTime;
            if (timer >= 2.5f)
            {
                alphaValue -= Time.deltaTime * 0.5f;
            }
            else
            {
                alphaValue += Time.deltaTime * 0.5f;
            }
            m_text[1].alpha = alphaValue;

            if (timer >= 2.5f)
            {
                if (alphaValue <= 0)
                {
                    m_text[1].gameObject.SetActive(false);
                    timer = 0;
                    alphaValue = 0;
                }
            }
        }

        if (m_text[0].gameObject.activeSelf == false && m_text[1].gameObject.activeSelf == false)
        {

            Invoke("mainSceneLoad", 1);
        }

    }

    private void mainSceneLoad()
    {
        SceneManager.LoadSceneAsync(0);

    }

}
