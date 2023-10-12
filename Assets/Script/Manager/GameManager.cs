using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public enum eStage
    {
        Tutorial,
        Stage1,
        Stage2,
    }
    [SerializeField] private eStage stage;//현재스테이지를 표현해줌 이거에따라 나오는위치가달라짐 나중에 삭제해도될듯
    public static GameManager instance;
    private TrsUI trsUI;
    private Transform m_TrsUI;

    private GameObject GameMenu;
    private GameObject PlayerDeathMenu;
    private bool deathCheck = false;
    private Player _player;
    private bool playerReStartCheck = false;

  
    
    
    private Player player
    {
        get
        {
            if (_player == null)
            {

                _player = FindObjectOfType<Player>();
            }
            return _player;
        }
    }


    private void Awake()
    {

        DontDestroyOnLoad(this);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            trsUI = FindObjectOfType<TrsUI>();
            m_TrsUI = trsUI.GetTrsUI();
            GameMenu = m_TrsUI.GetComponentInChildren<Transform>().Find("GameMenuBackGround").gameObject;
            PlayerDeathMenu = m_TrsUI.GetComponentInChildren<Transform>().Find("PlayerDeathMenu").gameObject;

            reStart();
        }
        gameMenu();
        playerDeathCheck();


    }

   

    private void gameMenu()
    {
        if (SceneManager.GetActiveScene().name != "GameScene")
        {
            return;
        }

        if (deathCheck)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void playerDeathCheck()
    {
        if (SceneManager.GetActiveScene().name != "GameScene")
        {
            return;
        }

        if (player.GetDeathCheck() == true)
        {
            PlayerDeathMenu.SetActive(true);
            deathCheck = true;
            Time.timeScale = 0;
        }
    }

    private void reStart()
    {
        if (playerReStartCheck == true)
        {
            player.SetDeathCheck(false);
            player.SetPlayerReset();
            player.SetOneDeathReturn();

            PlayerDeathMenu.SetActive(false);
            playerReStartCheck = false;
        }
    }

    



    public Transform GetPlayerTransform()
    {
        return player.transform;
    }
    public eStage GetStage()
    {
        return stage;
    }

    public void SetStage(eStage _stage)
    {
        stage = _stage;
    }

    public void SetDeathMenu(bool _value)
    {
        PlayerDeathMenu.SetActive(_value);
    }

    public bool GetReStart()
    {
        return playerReStartCheck;
    }

    public void SetReStart(bool _value)
    {
        playerReStartCheck = _value;
    }
}
