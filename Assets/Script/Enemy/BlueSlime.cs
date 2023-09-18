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

    [SerializeField] private bool playerCheck = false;//플레이어 인식범위체크용
    private bool playerOutCheck = false;//플레이어 나갔는지체크용
    private bool checkPlayerInBox2d = true;
    private bool right;//슬라임 좌우 체크용도

    private bool attackAfterReturn = false;
    [SerializeField]private bool slimeAttackCheck;

    [SerializeField] private bool returnStart;//원래자리로돌아갈지체크하는부분
    private Vector3 m_vecStartPoint;

    [SerializeField] private float m_forgetPlayerTime = 1.5f;
    [SerializeField] private float m_forgetPlayerTimer = 0.0f;

    //본인
    private Animator m_anim;
    private BoxCollider2D m_box2d;
    private Rigidbody2D m_rig2d;
    private BlueSlime m_blueSlime;
    //자식
    private EnemyHitBox m_hitbox;
    private bool m_hiyboxCheck = true;
    private Transform bluePhy2d;
    [SerializeField]private PolygonCollider2D m_poly2d;

    //플레이어
    private Player player;
    private Transform m_playerTrs;

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
        m_hitbox = GetComponentInChildren<EnemyHitBox>();
        m_anim.enabled = false;
        m_blueSlime = GetComponent<BlueSlime>();

        bluePhy2d = GetComponentInChildren<Transform>().Find("BlueSlimePhy");
        m_poly2d = bluePhy2d.GetComponent<PolygonCollider2D>();

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
        if (m_anim.GetBool("BlueDeath") == true)
        {
            m_rig2d.velocity = new Vector2(0, 0);
            return;
        }
        if (m_hitbox.GetBlueAttackCheck() == true)
        {
            blueSlimeAttackCherck();
            return;
        }
        blueSlimeCheck();
        blueSlimePerceive();
        blueSlimeMove();
        blueSlimeSee();
        blueSlimeBox2dOutCheck();
        backStartPos();

    }
    private void blueSlimeCheck()
    {
        if (slimeCheck == 1)
        {
            m_box2d.enabled = false;
            m_poly2d.enabled = false;
            m_blueSlime.enabled = false;
        }
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
            m_poly2d.enabled = true;
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
        if (returnStart || slimeAttackCheck)
        {
            return;
        }
        if (playerCheck)
        {
            m_playerTrs = GameManager.instance.GetPlayerTransform();
            if (transform.position.x - m_playerTrs.position.x > 0)
            {
                m_rig2d.velocity = new Vector2(-1 * m_moveSpeed, m_rig2d.velocity.y);
                right = false;
            }
            else if (transform.position.x - m_playerTrs.position.x < 0)
            {
                m_rig2d.velocity = new Vector2(1 * m_moveSpeed, m_rig2d.velocity.y);
                right = true;
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
        if (playerCheck)
        {
            if(player.GetPlayerGroundType()!= Player.eGroundType.Water)
            {
                returnStart = true;
            }
        }
        

    }
    private void blueSlimeSee()
    {
        if (right)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
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
                checkPlayerInBox2d = true;
                returnStart = false;
                m_poly2d.enabled = false;
                m_anim.enabled = false;
                slimeCheck = Random.Range(0, 2);
                m_Spr.sprite = m_LWaterGrass[grassType];
            }
        }
    }

    private void blueSlimeAttackCherck()
    {

        if (m_hiyboxCheck)
        {
            m_anim.SetBool("BlueAttack", true);
        }
        m_hiyboxCheck = false;
        if (slimeAttackCheck)
        {
            m_playerTrs = GameManager.instance.GetPlayerTransform();
            attackAfterReturn = true;
            if (!right)
            {
                m_rig2d.velocity = new Vector2(-1 * m_moveSpeed * 3, m_rig2d.velocity.y);
            }
            else if (right)
            {
                m_rig2d.velocity = new Vector2(1 * m_moveSpeed * 3, m_rig2d.velocity.y);
            }
        }
        else
        {
            m_rig2d.velocity = new Vector2(0, m_rig2d.velocity.y);
            if (attackAfterReturn)
            {
                m_hitbox.SetBlueAttackCheck(false);
                playerOutCheck = true;
                m_hiyboxCheck = true;
                attackAfterReturn = false;
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
        m_anim.SetBool("BlueAttack", false);
    }

    private void blueAttackMoveTrue()
    {
        slimeAttackCheck = true;
    }
    private void blueAttackMoveFalse()
    {
        slimeAttackCheck = false;
    }

  

    private void slimeDestroy()
    {
        Destroy(gameObject);
    }

}
