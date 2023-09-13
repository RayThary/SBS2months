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
    [SerializeField] private float m_moveSpeed=2.0f;

    [SerializeField] private List<Sprite> m_LWaterGrass=new List<Sprite>();
    private SpriteRenderer m_Spr;
    private int grassType;//무슨풀인지정할부분

    private bool playerCheck = false;


    private Animator m_anim;
    private BoxCollider2D m_box2d;
    private Rigidbody m_rig2d;

    private Transform m_playerTrs;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerCheck = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerCheck = false;
        }
    }
    void Start()
    {
        if(eSlimeType == SlimeType.Random)
        {
            slimeCheck = Random.Range(0, 2);
            if (slimeCheck == 0)
            {
                eSlimeType = SlimeType.Slime;
            }
            else if(slimeCheck == 1)
            {
                eSlimeType = SlimeType.Grass;
            }
        }

        m_Spr = GetComponent<SpriteRenderer>();
        m_anim = GetComponent<Animator>();
        m_box2d = GetComponent<BoxCollider2D>();


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
}
