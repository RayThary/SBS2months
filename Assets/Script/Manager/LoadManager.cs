using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class LoadManager : MonoBehaviour
{
    public static LoadManager instance;

    private Transform playerTrs;
    private Player player;

    [SerializeField] private Image imgFade;    

    private Color fadeColor;
    private float speed = 1f;

    private bool fadeOffCheck;
    private bool stageChange;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    void Start()
    {
        imgFade.gameObject.SetActive(false);
        fadeColor = imgFade.color;
        playerTrs = GameManager.instance.GetPlayerTransform();
        player = playerTrs.GetComponent<Player>();
        stageChange = true;
        
    }


    void Update()
    {
        
        isFade();
        fadeOff();
    }

    private void isFade()
    {
        if (stageChange)
        {
            GameManager.instance.SetFadeCheck(true);
            imgFade.gameObject.SetActive(true);
            fadeColor.a = 1;
            imgFade.color = fadeColor;
            player.SetPlayerStop(true);
            fadeOffCheck = true;
            stageChange = false;
        }
    }
    private void fadeOff()
    {
        if (!fadeOffCheck)
        {
            return;
        }
        int fps = (int)(1.0f / Time.deltaTime);
        if (fps >= 43)
        {
            
            StartCoroutine("fadeOffStart");
        }
    }
    IEnumerator fadeOffStart()
    {
        yield return new WaitForSeconds(3f);
        fadeColor.a -= speed * Time.deltaTime;
        imgFade.color = fadeColor;
        if (fadeColor.a <= 0)
        {
            fadeOffCheck = false;
            player.SetPlayerStop(false);
            imgFade.gameObject.SetActive(false);
            GameManager.instance.SetFadeCheck(false);
        }

    }
    public void SetStageChageCheck(bool _value)
    {
        stageChange = _value;
    }

    public bool GetStageChange()
    {
        return fadeOffCheck;
    }
}
