using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHp : MonoBehaviour
{
    private Player player;
    private Transform playerTrs;
    private int m_playerHp;

    [SerializeField] private Sprite m_HpOn;
    [SerializeField] private Sprite m_HpOff;


    private Transform m_HP1;
    private Transform m_HP2;
    private Transform m_HP3;

   private Image m_imgHP1;
   private Image m_imgHP2;
    private Image m_imgHP3;
    void Start()
    {

        playerTrs = GameManager.instance.GetPlayerTransform();
        player = playerTrs.GetComponent<Player>();
        
        m_HP1 = GetComponentInChildren<Transform>().Find("HP1");
        m_HP2 = GetComponentInChildren<Transform>().Find("HP2");
        m_HP3 = GetComponentInChildren<Transform>().Find("HP3");

        m_imgHP1 = m_HP1.GetComponent<Image>();
        m_imgHP2 = m_HP2.GetComponent<Image>();
        m_imgHP3 = m_HP3.GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        m_playerHp = player.GetPlayerHp();
        if (m_playerHp == 3)
        {
            m_imgHP1.sprite = m_HpOn;
            m_imgHP2.sprite = m_HpOn;
            m_imgHP3.sprite = m_HpOn;
        }   
        else if (m_playerHp == 2)
        {
            m_imgHP1.sprite = m_HpOn;
            m_imgHP2.sprite = m_HpOn;
            m_imgHP3.sprite = m_HpOff;
        }
        else if (m_playerHp == 1)
        {
            m_imgHP1.sprite = m_HpOn;
            m_imgHP2.sprite = m_HpOff;
            m_imgHP3.sprite = m_HpOff;
        }
        else if (m_playerHp == 0)
        {
            m_imgHP1.sprite = m_HpOff;
            m_imgHP2.sprite = m_HpOff;
            m_imgHP3.sprite = m_HpOff;
        }
    }
}
