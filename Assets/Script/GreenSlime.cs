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
    public SlimeType eSlimeType;
    private int checkSlime;
    private bool isSlime;

    [SerializeField] private Sprite m_SprHideing;
    [SerializeField] private Sprite m_SprSlime;
    private SpriteRenderer m_Spr;

    [SerializeField] private float m_moveSpeed = 4.0f;
    private bool m_checkPlayer = false;
    private Animator m_anim;

    private bool PlayerCheck = false;

    private bool m_timerStart = false;
    private float m_timer = 0.0f;
    [SerializeField] private float m_forgetPlayerTime = 3f;
    private bool returnStart;

    private Vector3 m_vecStartPoint;

    private Transform m_playerTrs;
    private BoxCollider2D m_box2d;
    private Rigidbody2D m_rig2d;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isSlime)
        {
            return;
        }
        if (collision.gameObject.tag == "Player")
        {
            returnStart = false;
            m_timerStart = false;
            m_timer = 0.0f;
            m_Spr.sprite = m_SprSlime;
            m_anim.enabled = true;
            PlayerCheck = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isSlime)
        {
            return;
        }
        if (collision.gameObject.tag == "Player")
        {
            m_timerStart = true;
            PlayerCheck = false;
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
        m_vecStartPoint = transform.position;
        m_anim = GetComponent<Animator>();
        m_anim.enabled = false;
        m_Spr = GetComponent<SpriteRenderer>();
        m_box2d = GetComponent<BoxCollider2D>();
        m_rig2d = GetComponent<Rigidbody2D>();
        m_Spr.sprite = m_SprHideing;

    }

    void Update()
    {
        slimeCheck();

        if (!isSlime)
        {
            return;
        }
        checkPlayer();
        checkTimer();
        backStartPos();
    }

    private void slimeCheck()
    {
        if (checkSlime == 0)
        {
            isSlime = true;
        }
        else if (checkSlime == 1)
        {
            isSlime = false;
        }
    }
    private void checkPlayer()
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
        if (!PlayerCheck)
        {
            m_rig2d.velocity = new Vector2(0, m_rig2d.velocity.y);
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

}
