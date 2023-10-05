using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSlime : MonoBehaviour
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

    [SerializeField] private float m_slimeSpeed = 2.0f;
    private bool PlayerCheck = false;
    private bool playerHitCheck;


    //플레이어가 인식범위로나갔을때 몇초뒤에 돌아갈지 체크해주는부분
    private bool m_timerStart = false;
    private float m_timer = 0.0f;
    [SerializeField] private float m_forgetPlayerTime = 3f;
    private bool returnStart;
    private bool redSlimeReturn;

    //플레이어를인식했을때의자리
    private Vector3 m_vecStartPoint;
    private bool m_oneCheckVector = false;

    private bool enemyDeathCheck = false;

    private BoxCollider2D m_box2d;//플레이어의 인식범위 박스크기로 밖에서조절해주면됨
    private PolygonCollider2D m_poly2d;
    private Animator m_anim;


    //오브젝트일때의움직임 
    [Header("좌우면 체크해제 올라가면체크")]
    [SerializeField] private bool up = false;
    [SerializeField] private float moveMax;
    [SerializeField]private float objSpeed = 2.0f;
    private Vector3 m_startPos;
    private Vector3 m_endPos;
    private bool m_endPosCheck = false;
    private bool m_objMoveCheck = true;

    private Rigidbody2D m_rig2d;
    private Player player;
    private Transform playerTrs;
    private EnemyHitBox hitBox;

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

        playerTrs = GameManager.instance.GetPlayerTransform();
        player = playerTrs.GetComponent<Player>();

        m_anim = GetComponent<Animator>();
        m_box2d = GetComponent<BoxCollider2D>();
        m_poly2d = GetComponent<PolygonCollider2D>();
        m_Spr = GetComponent<SpriteRenderer>();
        m_rig2d = GetComponent<Rigidbody2D>();

        m_anim.enabled = false;
        m_poly2d.enabled = false;

        //hitBox = GetComponentInChildren<EnemyHitBox>();


        m_startPos = transform.position;
        if (up)
        {
            m_endPos = new Vector3(m_startPos.x, m_startPos.y + moveMax, m_startPos.z);

        }
        else
        {
            m_endPos = new Vector3(m_startPos.x + moveMax, m_startPos.y, m_startPos.z);
        }

        if (checkSlime == 0)
        {
            //슬라임일땐 딱히건들게없음 나중에생각나면 적어줄필요있음
        }
        else if (checkSlime == 1)
        {
            m_box2d.enabled = false;
        }
    }


    void Update()
    {
        if (checkSlime == 1)
        {
            objMove();
            return;
        }
        else
        {
            if (enemyDeathCheck)
            {
                return;
            }
            playerCheck();
            objMove();
            slimeMove();
            slimeMoveStartPos();
        }
    }
    private void objMove()
    {
        if (m_objMoveCheck == false)
        {
            return;
        }

        if (up)
        {
            if (m_endPosCheck == false)
            {
                m_rig2d.velocity = new Vector2(m_rig2d.velocity.x, objSpeed);
                if (transform.position.y >= m_endPos.y)
                {
                    m_endPosCheck = true;
                }
            }
            else
            {
                m_rig2d.velocity = new Vector2(m_rig2d.velocity.x, -objSpeed);
                if (transform.position.y <= m_startPos.y)
                {
                    m_endPosCheck = false;
                }
            }
        }
        else
        {
            if (m_endPosCheck == false)
            {
                m_rig2d.velocity = new Vector2(objSpeed, m_rig2d.velocity.y);
                if (transform.position.x >= m_endPos.x)
                {
                    m_endPosCheck = true;
                }
            }
            else
            {
                m_rig2d.velocity = new Vector2(-objSpeed, m_rig2d.velocity.y);
                if (transform.position.x <= m_startPos.x)
                {
                    m_endPosCheck = false;
                }
            }
        }

    }
    private void playerCheck()
    {
        if (m_box2d.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            if (player.GetPlayerType() != Player.eType.Red)
            {
                return;
            }
            PlayerCheck = true;
            m_objMoveCheck = false;
            returnStart = false;
            m_poly2d.enabled = true;
            m_anim.enabled = true;
            //시간관련 잊어버리는부분 다시들어왔을땐 작동x 그리고 시간초기화부분
            m_timerStart = false;
            m_timer = 0.0f;
            if (m_oneCheckVector == false)
            {
                m_vecStartPoint = transform.position;
                m_oneCheckVector = true;
            }
        }
        else
        {
            if (PlayerCheck)
            {
                PlayerCheck = false;
                m_timerStart = true;
            }

        }
    }

    private void slimeMove()
    {
        if (redSlimeReturn)
        {
            returnStart = true;
            return;
        }
        if (PlayerCheck)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTrs.position, m_slimeSpeed * Time.deltaTime);
        }


        if (m_timerStart)
        {
            m_timer += Time.deltaTime;
            if (m_timer >= m_forgetPlayerTime)
            {
                returnStart = true;
                m_timer = 0.0f;

            }
        }
    }

    private void slimeMoveStartPos()
    {
        if (returnStart)
        {
            transform.position = Vector2.MoveTowards(transform.position, m_vecStartPoint, m_slimeSpeed * Time.deltaTime);
            Vector2 slimePos = transform.position;
            Vector2 startPos = m_vecStartPoint;

            float dis = Vector2.Distance(slimePos, startPos);
            if (dis < 0.1f)
            {
                PlayerCheck = false;
                returnStart = false;

                m_anim.enabled = false;
                m_poly2d.enabled = false;

                m_startPos = transform.position;
                if (up)
                {
                    m_endPos = new Vector2(startPos.x, startPos.y + moveMax);
                }
                else
                {
                    m_endPos = new Vector2(startPos.x + moveMax, startPos.y);
                }
                m_objMoveCheck = true;

                m_Spr.sprite = m_SprHideing;
                redSlimeReturn = false;
            }
        }
    }


    public bool GetPlayerCheck()
    {
        return PlayerCheck;
    }

    public void SetSlimeReturn(bool _value)
    {
        redSlimeReturn = _value;
    }

    public void SetEnemyDeathCheck(bool _value)
    {
        enemyDeathCheck = _value;
    }

    private void SetDestorySlime()
    {
        Destroy(gameObject);
    }
}
