using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSlime : MonoBehaviour
{
    public enum SlimeType
    {
        Random,
        Grass,
        Slime,
    }
    [SerializeField] private SlimeType eSlimeType;
    private int checkSlime;


    [SerializeField] private Sprite m_SprHideing;
    private SpriteRenderer m_Spr;

    [SerializeField] private float m_moveSpeed = 4.0f;
    private Animator m_anim;

    private bool PlayerCheck = false;
    private bool playerHitCheck;

    //플레이어가 인식범위로나갔을때 몇초뒤에 돌아갈지 체크해주는부분
    private bool m_timerStart = false;
    private float m_timer = 0.0f;
    [SerializeField] private float m_forgetPlayerTime = 3f;
    private bool returnStart;

    //플레이어를인식했을때의자리
    private Vector3 m_vecStartPoint;

    private bool enemyDeathCheck = false;

    private Transform m_playerTrs;
    private BoxCollider2D m_box2d;//플레이어의 인식범위 박스크기로 밖에서조절해주면됨
    private Rigidbody2D m_rig2d;

    private Player player;
    private EnemyHitBox hitBox;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            if (player.GetPlayerType() != Player.eType.Green)
            {
                return;
            }

            m_vecStartPoint = transform.position;
            returnStart = false;
            m_timerStart = false;
            PlayerCheck = true;
            m_timer = 0.0f;
            m_anim.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            if (PlayerCheck)
            {
                m_timerStart = true;
                PlayerCheck = false;
            }
        }
    }

    void Start()
    {
        if (eSlimeType == SlimeType.Random)
        {
            checkSlime = Random.Range(0, 2);
        }
        else if (eSlimeType == SlimeType.Slime)
        {
            checkSlime = 0;
        }
        else if (eSlimeType == SlimeType.Grass)
        {
            checkSlime = 1;
        }

        m_anim = GetComponent<Animator>();
        m_anim.enabled = false;
        m_Spr = GetComponent<SpriteRenderer>();
        m_box2d = GetComponent<BoxCollider2D>();
        m_rig2d = GetComponent<Rigidbody2D>();
        m_Spr.sprite = m_SprHideing;

        hitBox = GetComponentInChildren<EnemyHitBox>();
        player = GameManager.instance.GetPlayerTransform().GetComponent<Player>();

        if (checkSlime == 1)
        {
            m_box2d.enabled = false;
            GetComponentInChildren<EnemyHitBox>().gameObject.SetActive(false);
            GetComponent<GreenSlime>().enabled = false;
        }
    }

    void Update()
    {
        
        if (enemyDeathCheck)
        {
            return;
        }
        slimeCheck();
        checkPlayerAndSlimeMove();
        checkTimer();
        backStartPos();
    }

    private void slimeCheck()
    {
        if (checkSlime == 1)
        {
            m_box2d.enabled = false;
            m_anim.enabled = false;
        }
    }
    private void checkPlayerAndSlimeMove()
    {
        if (returnStart)
        {
            return;
        }
        playerHitCheck = hitBox.GetPlayerHitCeck();
        if (playerHitCheck)
        {
            m_rig2d.velocity = new Vector2(0, 0);
        }
        else
        {
            if (PlayerCheck)
            {
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
            else
            {
                m_rig2d.velocity = new Vector2(0, m_rig2d.velocity.y);
            }
        }
    }
    private void checkTimer()
    {
        if (!m_timerStart)
        {
            return;
        }
        m_timer += Time.deltaTime;
        if (m_timer >= m_forgetPlayerTime)
        {
            returnStart = true;
        }
    }

    private void backStartPos()
    {
        if (returnStart)
        {
            m_rig2d.velocity = new Vector2(0, m_rig2d.velocity.y);
            transform.position = Vector2.MoveTowards(transform.position, m_vecStartPoint, 2f * Time.deltaTime);
            Vector2 slimePos = transform.position;
            slimePos.y = 0;
            Vector2 movePos = m_vecStartPoint;
            movePos.y = 0;
            float distance = Vector2.Distance(slimePos, movePos);
            if (distance < 0.1f)
            {
                m_timerStart = false;
                returnStart = false;
                m_anim.enabled = false;
                m_Spr.sprite = m_SprHideing;
                checkSlime = Random.Range(0, 2);
            }
        }
    }
    public bool GetPlayerCheck()
    {
        return PlayerCheck;
    }

    public void SetReturnStart(bool _value)
    {
        returnStart = _value;
    }
    public int GetSlimeType()
    {
        return checkSlime;
    }

    //애니메이션용
    private void EnemyDeathCheck()
    {
        enemyDeathCheck = true;
    }
    private void EnemyGreenDeath()
    {
        Destroy(gameObject);
    }
}
