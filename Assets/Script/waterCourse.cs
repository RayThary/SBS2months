using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterCourse : MonoBehaviour
{
    private SpriteRenderer m_spr;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            m_spr.enabled = true;
        }
    }

    void Start()
    {
        m_spr = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        
    }
}
