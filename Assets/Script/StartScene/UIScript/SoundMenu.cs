using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundMenu : MonoBehaviour
{
    [SerializeField] private Transform m_GameMenu;
    private Transform m_TrsSoundMenu;
    private Button m_BtnControllerExit;

    void Start()
    {
        m_TrsSoundMenu = GetComponent<Transform>();

        Transform controllerExit = GetComponentInChildren<Transform>().Find("Exit");
        m_BtnControllerExit = controllerExit.GetComponent<Button>();

        m_TrsSoundMenu.gameObject.SetActive(false);

        m_BtnControllerExit.onClick.AddListener(() => BtnSoundMenuExit());


    }
    private void BtnSoundMenuExit()
    {
        m_TrsSoundMenu.gameObject.SetActive(false);
        m_GameMenu.gameObject.SetActive(true);
    }

}
