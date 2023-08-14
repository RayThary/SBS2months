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
    [SerializeField] private int waterCount=5;

    private BoxCollider2D m_box2d;
    private Animator m_anim;

    private void Start()
    {
        m_box2d = GetComponent<BoxCollider2D>();
        m_anim = GetComponent<Animator>();
    }

    void Update()
    {
        CheckLever();
    }
    private void CheckLever()
    {
        if (m_box2d.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                m_anim.SetBool("Lever", true);
                spawnWaterCourse();
                StartCoroutine(checkReturnLever());
            }
        }
    }
    
    IEnumerator checkReturnLever()
    {
        yield return new WaitForSeconds(2f);
        m_anim.SetBool("Lever", false);
    }
    
    private void spawnWaterCourse()
    {
        for(int water = 0; water < waterCount; water++)
        {
            if (water == 0)
            {
                Instantiate(m_waterHight, TrsSpawnWater.position, Quaternion.identity,transform);
            }
            else if(water != waterCount-1)
            {
                Instantiate(m_waterMid, TrsSpawnWater.position, Quaternion.identity, transform);
            }
            else if(water == waterCount-1)
            {
                Instantiate(m_waterLow, TrsSpawnWater.position, Quaternion.identity, TrsSpawnWater);
            }
        }
    }

    public int GetWaterCount() 
    {
        return waterCount;
    }
}
