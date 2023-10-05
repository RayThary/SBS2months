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
    [SerializeField] private List<Transform> m_startTrs = new List<Transform>();

    private Animator m_anim2d;
    



    
    void Start()
    {
        Transform playerTrs = GameManager.instance.GetPlayerTransform();
        player = playerTrs.GetComponent<Player>();
        m_anim2d = player.GetComponent<Animator>();

        m_BtnReStart.onClick.AddListener(() => reStartButton());
        m_BtnExit.onClick.AddListener(() => exitButton(0));
    }


    private void reStartButton()
    {

        if (GameManager.instance.GetStage() == GameManager.eStage.Tutorial)
        {
            player.transform.position = m_startTrs[0].transform.position;
        }
        else if (GameManager.instance.GetStage() == GameManager.eStage.Stage1)
        {
            player.transform.position = m_startTrs[1].transform.position;
        }
        else if (GameManager.instance.GetStage() == GameManager.eStage.Stage2)
        {
            player.transform.position = m_startTrs[2].transform.position;
        }
        Time.timeScale = 1.0f;
        player.SetDeathCheck(false);
        player.SetPlayerReset();
        player.SetOneDeathReturn();
        m_anim2d.SetTrigger("DeathReturn");

        GameManager.instance.SetDeathMenu(false);
    }

    private void exitButton(int _value)
    {
        SceneManager.LoadSceneAsync((int)_value);
    }

}
