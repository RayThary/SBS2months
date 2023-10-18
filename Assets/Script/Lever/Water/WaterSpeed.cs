using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpeed : MonoBehaviour
{
    [SerializeField] private BoxCollider2D m_water;
    private BoxCollider2D m_lever;
    private Animator m_waterAnim2d;
    private Animator m_anim2d;
    void Start()
    {
        m_waterAnim2d = m_water.GetComponent<Animator>();
        m_lever = GetComponent<BoxCollider2D>();
        m_anim2d = GetComponent<Animator>();
    }

    
    void Update()
    {
        if (m_lever.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                m_anim2d.SetTrigger("Lever");

            }
        }

    }
}
