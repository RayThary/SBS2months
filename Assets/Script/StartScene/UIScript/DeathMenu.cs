using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;

public class DeathMenu : MonoBehaviour
{
    private Player player;
    [SerializeField] private Button m_BtnReStart;
    [SerializeField] private Button m_BtnExit;
    [SerializeField] private AudioClip m_btnClip;
    private Animator m_anim2d;
    



    
    void Start()
    {
        Transform playerTrs = GameManager.instance.GetPlayerTransform();
        player = playerTrs.GetComponent<Player>();
        m_anim2d = player.GetComponent<Animator>();

        m_BtnReStart.onClick.AddListener(() => reStartButton(2));
        m_BtnExit.onClick.AddListener(() => exitButton(0));
    }

  
    private void reStartButton(int _value)
    {
        SceneManager.LoadSceneAsync((int)_value);
        SoundManager.instance.SFXPlay(m_btnClip);
    }

    private void exitButton(int _value)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync((int)_value);
        SoundManager.instance.SFXPlay(m_btnClip);
    }

}
