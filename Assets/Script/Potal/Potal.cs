using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 이포탈은 스테이지 바꾸는용도라서 맵안에서의 텔레포트를만들고싶으면 쿨타임을주어서 바꾸는게나을듯함
/// </summary>
public class Potal : MonoBehaviour
{
    private Transform m_TrsPotal1;// 포탈1의 위치 
    private Transform m_TrsPotal2;// 포탈2의 위치

    private Transform m_TrsPoint;// 포탈이 도착할위치

    [SerializeField] private GameManager.eStage Potal1Stage;// 포탈1이 갈곳
    [SerializeField] private GameManager.eStage Potal2Stage;// 포탈2가 갈곳

    private CapsuleCollider2D potal1Cap2d;
    private CapsuleCollider2D potal2Cap2d;

    private Player player;
    private GameObject objPlayer;

    void Start()
    {
        m_TrsPotal1 = GetComponentInChildren<Transform>().Find("Potal1");
        m_TrsPotal2 = GetComponentInChildren<Transform>().Find("Potal2");


        potal1Cap2d = m_TrsPotal1.GetComponent<CapsuleCollider2D>();
        potal2Cap2d = m_TrsPotal2.GetComponent<CapsuleCollider2D>();
        player = FindObjectOfType<Player>();
        objPlayer = player.gameObject;
    }

    void Update()
    {
        isPotal();
    }

    private void isPotal()
    {
        if (potal1Cap2d.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                LoadManager.instance.SetStageChageCheck(true);
                GameManager.instance.SetStage(Potal1Stage);
                m_TrsPoint = m_TrsPotal2;
                usingPotal();
            }
        }
        if (potal2Cap2d.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                LoadManager.instance.SetStageChageCheck(true);
                GameManager.instance.SetStage(Potal2Stage);
                m_TrsPoint = m_TrsPotal1;
                usingPotal();
            }
        }
    }
    private void usingPotal()
    {
        objPlayer.transform.position = m_TrsPoint.position;
    }


}
