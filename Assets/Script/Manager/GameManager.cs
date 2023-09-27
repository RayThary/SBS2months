using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public enum eStage
    {
        Tutorial,
        Stage1,
        Stage2,
        Stage3,
    }
    [SerializeField]private eStage stage;//현재스테이지를 표현해줌 이거에따라 나오는위치가달라짐 나중에 삭제해도될듯
    public static GameManager instance;

    [SerializeField] private GameObject GameMenu;

    private Player player;

    private void Awake()
    {

        GameMenu.SetActive(false);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        } 
        player = FindObjectOfType<Player>();
        
    }

    void Start()
    {
        
    }

    private void Update()
    {
        gameMenu();
    }

    private void gameMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameMenu.SetActive(true);
            Time.timeScale = 0;
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
    
}
