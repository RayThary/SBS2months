using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundMenu : MonoBehaviour
{
    [SerializeField] private Transform m_GameMenu;
    private Transform m_TrsSoundMenu;
    private Button m_BtnControllerExit;
    [SerializeField] protected AudioClip m_btnExitSound;
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
        SoundManager.instance.SFXPlay(m_btnExitSound);
        m_TrsSoundMenu.gameObject.SetActive(false);
        m_GameMenu.gameObject.SetActive(true);
    }

}
