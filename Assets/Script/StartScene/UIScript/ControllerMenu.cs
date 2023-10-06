using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerMenu : MonoBehaviour
{
    [SerializeField] private Transform m_GameMenu;
    private Transform m_TrsControllerMenu;
    [SerializeField]private Button m_BtnControllerExit;

    void Start()
    {
        m_TrsControllerMenu = GetComponent<Transform>();

        Transform controllerExit = GetComponentInChildren<Transform>().Find("ControllerExit");
        m_BtnControllerExit = controllerExit.GetComponent<Button>();

        m_TrsControllerMenu.gameObject.SetActive(false);

        m_BtnControllerExit.onClick.AddListener(() => BtnControllerMenuExit());

    }

    private void BtnControllerMenuExit()
    {
        m_TrsControllerMenu.gameObject.SetActive(false); 
        m_GameMenu.gameObject.SetActive(true);
    }


    void Update()
    {
        
    }
}
