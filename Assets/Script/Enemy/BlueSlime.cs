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
    private bool checkPlayerInBox2d = true;

    private bool slimeAttackCheck;

    private bool returnStart;//원래자리로돌아갈지체크하는부분
    private Vector3 m_vecStartPoint;

    [SerializeField] private float m_forgetPlayerTime = 1.5f;
    [SerializeField] private float m_forgetPlayerTimer = 0.0f;

    //본인
    private Animator m_anim;
    private BoxCollider2D m_box2d;
    private Rigidbody2D m_rig2d;

    //플레이어
    private Player player;
    private Transform m_playerTrs;

    #region
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        if (player.GetPlayerType() != Player.eType.Blue)
    //        {
    //            return;
    //        }
    //        m_vecStartPoint = transform.position;
    //        m_forgetPlayerTimer = 0.0f;
    //        m_anim.enabled = true;
    //        playerOutCheck = false;
    //        playerCheck = true;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        if (playerCheck)
    //        {
    //            playerCheck = false;
    //            playerOutCheck = true;
    //        }
    //    }
    //}
    #endregion
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
        blueSlimePerceive();
        blueSlimeMove();
        blueSlimeBox2dOutCheck();
        backStartPos();
    }

    private void blueSlimePerceive()
    {
        if (m_box2d.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            if (player.GetPlayerType() != Player.eType.Blue)
            {
                return;
            }
            m_forgetPlayerTimer = 0.0f;
            m_anim.enabled = true;
            playerOutCheck = false;
            playerCheck = true;
            if (checkPlayerInBox2d)
            {
                m_vecStartPoint = transform.position;
            }
            checkPlayerInBox2d = false;
        }
        else
        {
            if (playerCheck)
            {
                playerCheck = false;
                playerOutCheck = true;
            }
        }
    }
    private void blueSlimeMove()
    {
        if (returnStart)
        {
            return;
        }
        if (playerCheck)
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

    private void blueSlimeBox2dOutCheck()
    {
        if (playerOutCheck)
        {
            m_forgetPlayerTimer += Time.deltaTime;
            if (m_forgetPlayerTimer >= m_forgetPlayerTime)
            {
                returnStart = true;
            }
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
            if (distance <= 0.1f)
            {
                returnStart = false;
                checkPlayerInBox2d = true;
            }
        }
    }

    public void SetReturnStart(bool _value)
    {
        returnStart = _value;
    }

    public int GetSlimeType()
    {
        return slimeCheck;
    }

    //애니메이션용
    private void blueAttackOff()
    {
        m_anim.SetBool("blueAttack", false);
    }

    private void slimeDestroy()
    {
        Destroy(gameObject);
    }

}
