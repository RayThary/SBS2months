using Cinemachine;
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

    [SerializeField] private CinemachineImpulseSource _source;
    [SerializeField] private AudioClip m_hitSound;
    private Transform m_HP1;
    private Transform m_HP2;
    private Transform m_HP3;

    private Image m_imgHP1;
    private Image m_imgHP2;
    private Image m_imgHP3;

    private bool playerHitCheck = false;
    private bool oneImpulse = false;
    private bool twoImpulse = false;
    private bool threeImpulse = false;
    private bool hitSoundCheck = false;

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
            if (oneImpulse == false)
            {
                playerHitCheck = true;
            }
            m_imgHP1.sprite = m_HpOn;
            m_imgHP2.sprite = m_HpOn;
            m_imgHP3.sprite = m_HpOff;
        }
        else if (m_playerHp == 1)
        {
            if (twoImpulse == false)
            {
                playerHitCheck = true;
            }
            m_imgHP1.sprite = m_HpOn;
            m_imgHP2.sprite = m_HpOff;
            m_imgHP3.sprite = m_HpOff;
        }
        else if (m_playerHp == 0)
        {
            if (threeImpulse == false)
            {
                playerHitCheck = true;
            }
            m_imgHP1.sprite = m_HpOff;
            m_imgHP2.sprite = m_HpOff;
            m_imgHP3.sprite = m_HpOff;
        }
        playerImpulse();
        playerHitSound();
    }


    private void playerImpulse()
    {
        if (playerHitCheck && oneImpulse == false) 
        {
            _source.GenerateImpulse();
            hitSoundCheck = true;
            playerHitCheck = false;
            oneImpulse = true;
        }
        else if (playerHitCheck && twoImpulse == false)
        {
            _source.GenerateImpulse();
            hitSoundCheck = true;
            playerHitCheck = false;
            twoImpulse = true;
        }
        else if (playerHitCheck && threeImpulse == false)
        {
            _source.GenerateImpulse();
            hitSoundCheck = true;
            playerHitCheck = false;
            threeImpulse = true;
        }
    }
    private void playerHitSound()
    {
        if (hitSoundCheck)
        {
            SoundManager.instance.SFXPlay(m_hitSound);
            hitSoundCheck = false;
        }
    }
}
