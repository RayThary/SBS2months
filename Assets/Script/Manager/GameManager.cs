using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using static Cinemachine.CinemachineCore;

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
    private bool fadCheck = false;
    private Player _player;
    private bool playerReStartCheck = false;
    [SerializeField] private AudioClip btnEscSound;

    private bool oneTalkCheck = false;
    private bool firstTalk = false;
    private string[] talkdata;
    


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
        stage = eStage.Tutorial; 
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
        OneTalk();

    }

   

    private void gameMenu()
    {
        if (SceneManager.GetActiveScene().name != "GameScene")
        {
            return;
        }

        if (deathCheck || fadCheck) 
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SoundManager.instance.SFXPlay(btnEscSound);
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

    private void OneTalk()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            if (LoadManager.instance.GetStageChange() == true)
            {
                return;
            }
            
            oneTalkCheck = true;
            if (TextManager.instance.GetFirstTalk() == false)
            {
                firstTalk = true;
            }
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
    public void SetFadeCheck(bool _value)
    {
        fadCheck = _value;
    }

    public bool GetTalkCheck()
    {
        return oneTalkCheck;
    }

    public bool GetFirstTalk()
    {
        return firstTalk;
    }
}
