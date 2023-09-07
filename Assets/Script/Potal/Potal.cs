using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Potal : MonoBehaviour
{
    private Transform m_TrsPotal1;// Æ÷Å»1ÀÇ À§Ä¡ 
    private Transform m_TrsPotal2;// Æ÷Å»2ÀÇ À§Ä¡

    private Transform m_TrsPoint;// Æ÷Å»ÀÌ µµÂøÇÒÀ§Ä¡

    [SerializeField] private GameManager.eStage Potal1Stage;// Æ÷Å»1ÀÌ °¥°÷
    [SerializeField] private GameManager.eStage Potal2Stage;// Æ÷Å»2°¡ °¥°÷

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
