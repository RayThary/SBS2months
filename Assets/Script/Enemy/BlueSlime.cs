using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BlueSlime : MonoBehaviour
{
    public enum SlimeType
    {
        Random,
        Grass,
        Slime,
    }
    [SerializeField] private SlimeType eSlimeType;
    private int slimeCheck;
    [SerializeField] private float m_moveSpeed = 2.0f;

    [SerializeField] private List<Sprite> m_LWaterGrass = new List<Sprite>();
    private SpriteRenderer m_Spr;
    private int grassType;//무슨풀인지정할부분

    private bool playerCheck = false;//플레이어 인식범위체크용
    private bool playerOutCheck = false;//플레이어 나갔는지체크용
    private bool playerInOutCheck; //플레이어가 들어왔다가 나갔는지체크용

    private float m_forgetPlayerTimer = 1.5f;
    private float m_forgerPlayerTime = 0.0f;

    //본인
    private Animator m_anim;
    private BoxCollider2D m_box2d;
    private Rigidbody2D m_rig2d;

    //플레이어
    private Player player;
    private Transform m_playerTrs;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (player.GetPlayerType() != Player.eType.Blue)
            {
                return;
            }
            m_anim.enabled = true;
            playerCheck = true;
            playerInOutCheck = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerCheck = false;
            playerOutCheck = true;
        }
    }
    void Start()
    {
        if (eSlimeType == SlimeType.Random)
        {
            slimeCheck = Random.Range(0, 2);
            if (slimeCheck == 0)
            {
                eSlimeType = SlimeType.Slime;
            }
            else if (slimeCheck == 1)
            {
                eSlimeType = SlimeType.Grass;
            }
        }

        player = GameManager.instance.GetPlayerTransform().GetComponent<Player>();

        m_Spr = GetComponent<SpriteRenderer>();
        m_anim = GetComponent<Animator>();
        m_box2d = GetComponent<BoxCollider2D>();
        m_rig2d = GetComponent<Rigidbody2D>();

        m_anim.enabled = false;

        grassType = Random.Range(0, 3);
        if (grassType == 0)
        {
            m_Spr.sprite = m_LWaterGrass[0];
        }
        else if (grassType == 1)
        {
            m_Spr.sprite = m_LWaterGrass[1];
        }
        else if (grassType == 2)
        {
            m_Spr.sprite = m_LWaterGrass[2];
        }



        if (eSlimeType == SlimeType.Grass)
        {
            m_box2d.enabled = false;
        }
    }


    void Update()
    {
        blueSlimeMove();
    }

    private void blueSlimeMove()
    {
        if (!playerCheck)
        {
            return;
        }
        m_playerTrs = GameManager.instance.GetPlayerTransform();
        if (transform.position.x - m_playerTrs.position.x > 0)
        {
            m_rig2d.velocity = new Vector2(-1 * m_moveSpeed, m_rig2d.velocity.y);
        }
        else if (transform.position.x - m_playerTrs.position.x < 0)
        {
            m_rig2d.velocity = new Vector2(1 * m_moveSpeed, m_rig2d.velocity.y);
        }
    }

    private void blueSlimeBox2dOutCheck()
    {
        if (playerOutCheck && playerInOutCheck)
        {
            m_forgetPlayerTimer += Time.deltaTime;
            if (m_forgetPlayerTimer >= m_forgerPlayerTime)
            {
                //처음자리로돌아가는스크립트 and 풀인지 지정해주는코드 만들어줄것
            }
        }
    }
}
