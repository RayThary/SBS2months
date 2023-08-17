using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterCourse : MonoBehaviour
{
    private Transform playerTrs;
    private Rigidbody2D playerRig2d;
    [SerializeField] private Transform TrsSpawnWater;

    [SerializeField] private GameObject m_waterHight;
    [SerializeField] private GameObject m_waterMid;
    [SerializeField] private GameObject m_waterLow;

    [Header("물줄기 소환개수 위치따라 설정필요")]
    [SerializeField] private int waterCount;
    private List<Transform> waterMidList = new List<Transform>();
    private bool noLever = false;

    private Transform m_water;

    private BoxCollider2D m_box2d;
    private Animator m_anim;

    private void Start()
    {
        m_box2d = GetComponent<BoxCollider2D>();
        m_anim = GetComponent<Animator>();
        m_water = GetComponentInChildren<Transform>().Find("Water");
    }

    void Update()
    {
        CheckLever();
        Debug.Log(waterMidList.Count);
        //checkRemoveWaterList();
    }


    private void CheckLever()
    {
        if (noLever)
        {
            return;
        }
        if (m_box2d.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                m_anim.SetBool("Lever", true);
                if (waterMidList == null)
                {
                    spawnWaterCourse();
                }
                else if (waterMidList !=null)
                {
                    //다시위로올려줄것
                }
                StartCoroutine(checkReturnLever());
            }
        }
    }
    
    IEnumerator checkReturnLever()
    {
        yield return new WaitForSeconds(8f);
        m_anim.SetBool("Lever", false);
    }
    
    private void spawnWaterCourse()
    {
        for(int water = 0; water < waterCount; water++)
        {
            if (water == 0)
            {
                 Instantiate(m_waterHight, TrsSpawnWater.position, Quaternion.Euler(new Vector3(0, 0, 180f)), TrsSpawnWater);
            }
            else if(water != waterCount-1)
            {
                GameObject obj = Instantiate(m_waterMid, TrsSpawnWater.position, Quaternion.Euler(new Vector3(0,0,180f)), TrsSpawnWater);
                waterMidList.Add(obj.transform);
            }
            else if(water == waterCount-1)
            {
                 Instantiate(m_waterLow, TrsSpawnWater.position, Quaternion.Euler(new Vector3(0, 0, 180f)), TrsSpawnWater);
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
        return waterMidList;
    }
}
