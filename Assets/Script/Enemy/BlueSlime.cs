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

    [SerializeField] private List<Sprite> m_LWaterGrass=new List<Sprite>();
    private SpriteRenderer m_Spr;
    private int grassType;//무슨풀인지정할부분

    private Animator m_anim;
    private BoxCollider2D m_box2d;


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

        if(eSlimeType == SlimeType.Grass)
        {
            m_box2d.enabled = false;
            grassType = Random.Range(0, 3);
            if (grassType == 0)
            {
                m_Spr.sprite = m_LWaterGrass[0];
            }
            else if (grassType == 1)
            {
                m_Spr.sprite = m_LWaterGrass[1];
            }
            else if(grassType == 2)
            {
                m_Spr.sprite = m_LWaterGrass[2];
            }
        }
        else if (eSlimeType == SlimeType.Slime)
        {

        }

    }

    
    void Update()
    {
        
    }
}
