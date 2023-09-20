using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class waterCourse : MonoBehaviour
{
    [SerializeField] private bool tutorialText;
    private TextMeshPro leverText;
    private Transform leverTextTrs;

    private Transform TrsSpawnWater;
    private Transform TrsEndWater;
    [SerializeField] private GameObject m_waterHight;
    [SerializeField] private GameObject m_waterMid;
    [SerializeField] private GameObject m_waterLow;

    [Header("물줄기 소환개수 위치따라 설정필요")]
    [SerializeField] private int waterCount;
    [SerializeField] private int m_waterFallTimer;
    private List<Transform> waterList = new List<Transform>();
    private bool noSpawn = false;
    private bool leverCheck;
    private bool leverReady;

    private float timer = 0.0f;
    [SerializeField] private float time = 10f;

    private BoxCollider2D m_box2d;
    private Animator m_anim;

    private void Start()
    {
        leverText = GetComponentInChildren<TextMeshPro>();
        leverTextTrs = GetComponentInChildren<Transform>().Find("LeverText");

        m_box2d = GetComponent<BoxCollider2D>();
        m_anim = GetComponent<Animator>();
        TrsSpawnWater = GetComponentInChildren<Transform>().Find("Water");
        TrsEndWater = GetComponentInChildren<Transform>().Find("WaterEnd");
    }

    void Update()
    {
        leverTutorialText();
        CheckLever();
        checkZ();
    }

    private void leverTutorialText()
    {
        if (tutorialText == false)
        {
            leverTextTrs.gameObject.SetActive(false);
            return;
        }

        if (m_box2d.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            leverText.text = "레버 작동 z키";
        }
        else
        {
            leverText.text = "";
        }

    }

    private void checkZ()
    {

        if (noSpawn)
        {
            if (!leverReady && !leverCheck)
            {
                timer += Time.deltaTime;
                if (timer >= time)
                {
                    leverReady = true;
                    timer = 0.0f;
                }
            }
        }
    }


    private void CheckLever()
    {
        if (m_anim.GetBool("Lever") == true)
        {
            return;
        }
        if (noSpawn == false)
        {
            if (m_box2d.IsTouchingLayers(LayerMask.GetMask("Player")))
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    leverCheck = true;
                    leverReady = false;
                    m_anim.SetBool("Lever", true);
                    Invoke("CheckWaterMove", m_waterFallTimer);
                    spawnWaterCourse();
                }
            }
        }
        else if (noSpawn)
        {
            if ((m_anim.GetBool("Lever") == false) && leverReady)//&& bool값하나 추가해서 몇초뒤에작동하는코드에 그떄서야 불값on해주는걸로하면될듯
            {
                if (m_box2d.IsTouchingLayers(LayerMask.GetMask("Player")) && Input.GetKeyDown(KeyCode.Z))
                {
                    leverCheck = true;
                    leverReady = false;
                    m_anim.SetBool("Lever", true);
                    Invoke("CheckWaterMove", m_waterFallTimer);
                }
            }
        }
    }

    private void CheckWaterMove()
    {
        if (leverCheck)
        {
            leverCheck = false;
            m_anim.SetBool("Lever", false);
        }

    }

    private void spawnWaterCourse()
    {
        GameObject obj = null;
        for (int water = 0; water < waterCount; water++)
        {
            if (water == 0)
            {
                obj = Instantiate(m_waterHight, TrsSpawnWater.position, Quaternion.Euler(new Vector3(0, 0, 180f)), TrsSpawnWater);
            }
            else if (water != waterCount - 1)
            {
                obj = Instantiate(m_waterMid, TrsSpawnWater.position, Quaternion.Euler(new Vector3(0, 0, 180f)), TrsSpawnWater);
            }
            else if (water == waterCount - 1)
            {
                obj = Instantiate(m_waterLow, TrsSpawnWater.position, Quaternion.Euler(new Vector3(0, 0, 180f)), TrsSpawnWater);
                noSpawn = true;
            }
            waterList.Add(obj.transform);
        }
    }




    public int GetWaterCount()
    {
        return waterCount;
    }

    public List<Transform> GetWaterList()
    {
        return waterList;
    }
    public int GetWaterFallTimer()
    {
        return m_waterFallTimer;
    }
    public Transform GetWaterEndTrs()
    {
        return TrsEndWater;
    }
}
