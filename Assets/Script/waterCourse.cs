using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterCourse : MonoBehaviour
{
    private Transform playerTrs;
    private Rigidbody2D playerRig2d;

    private Transform TrsSpawnWater;

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
        m_box2d = GetComponent<BoxCollider2D>();
        m_anim = GetComponent<Animator>();
        TrsSpawnWater = GetComponentInChildren<Transform>().Find("Water");
    }

    void Update()
    {
        CheckLever();
        checkZ();
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
        else if(noSpawn)
        {
            if((m_anim.GetBool("Lever") == false) && leverReady)//&& bool값하나 추가해서 몇초뒤에작동하는코드에 그떄서야 불값on해주는걸로하면될듯
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
    //IEnumerator checkReturnLever()
    //{
        
    //    yield return new WaitForSeconds(8f);
    //    //yield return new WaitUntil(()=>불문 ==true)true가되면 통과
    //    //yield return new WaitWhile(()=> 불문==true)false 가되면 통과

    //    //yield return new WaitUntil(() =>
    //    //{
    //    //    return 불값 == false; 
    //    //})

    //    m_anim.SetBool("Lever", false);
        
    //    yield return null;
    //}
    
    private void spawnWaterCourse()
    {
        GameObject obj = null;
        for (int water = 0; water < waterCount; water++)
        {
            if (water == 0)
            {
                obj =  Instantiate(m_waterHight, TrsSpawnWater.position, Quaternion.Euler(new Vector3(0, 0, 180f)), TrsSpawnWater);
                waterList.Add(obj.transform);
                
            }
            else if(water != waterCount-1)
            {
                obj = Instantiate(m_waterMid, TrsSpawnWater.position, Quaternion.Euler(new Vector3(0,0,180f)), TrsSpawnWater);
                waterList.Add(obj.transform);
            }
            else if(water == waterCount-1)
            {
                 obj =Instantiate(m_waterLow, TrsSpawnWater.position, Quaternion.Euler(new Vector3(0, 0, 180f)), TrsSpawnWater);
                waterList.Add(obj.transform);
                noSpawn = true;
            }
            
        }
    }

  

    //private void checkRemoveWaterList()
    //{
    //    int count = waterMidList.Count;
        
    //    if (waterMidList!=null && waterMidList.Count > 0) //&&마지막이 트리거작동했을때 waterMidList[count-1] == null)
    //    {
    //        noLever = true;
    //    }
    //    else
    //    {
    //        noLever = false;
    //    }
    //}
    public int GetWaterCount() 
    {
        return waterCount;
    }
    
    public List<Transform> GetWaterMidList()
    {
        return waterList;
    }
    public int GetWaterFallTimer()
    {
        return m_waterFallTimer;
    }
}
