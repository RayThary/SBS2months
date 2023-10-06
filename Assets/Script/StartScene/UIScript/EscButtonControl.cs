using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscButtonControl : MonoBehaviour
{
    private Transform m_parentTrs;

    private Transform m_TrsContinue;
    private Transform m_TrsReStart;
    private Transform m_TrsController;
    private Transform m_TrsExit;

    private Button m_BtnContinue;
    [SerializeField]private Button m_BtnSound;
    private Button m_BtnController;
    private Button m_BtnExit;

    [SerializeField]private Transform m_TrsControllerMenu;

    void Start()
    {
        m_parentTrs = transform.parent;

        m_TrsContinue = GetComponentInChildren<Transform>().Find("Continue");
        m_TrsReStart = GetComponentInChildren<Transform>().Find("Sound");
        m_TrsController = GetComponentInChildren<Transform>().Find("Controller");
        m_TrsExit = GetComponentInChildren<Transform>().Find("Exit");

        m_BtnContinue = m_TrsContinue.GetComponent<Button>();
        m_BtnSound = m_TrsReStart.GetComponent<Button>();
        m_BtnController = m_TrsController.GetComponent<Button>();
        m_BtnExit = m_TrsExit.GetComponent<Button>();



        m_BtnContinue.onClick.AddListener(() => btnCountinue());

        m_BtnController.onClick.AddListener(()=> btnController());
        m_BtnExit.onClick.AddListener(() => exitButton(0));
    }

    private void btnCountinue()
    {
        Time.timeScale = 1.0f;
        m_parentTrs.gameObject.SetActive(false);
    }

    private void btnController()
    {
        m_TrsControllerMenu.gameObject.SetActive(true);
        transform.gameObject.SetActive(false);
    }

    private void exitButton(int _value)
    {
        SceneManager.LoadSceneAsync((int)_value);
    }
    void Update()
    {

    }
}
