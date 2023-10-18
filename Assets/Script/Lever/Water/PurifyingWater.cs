using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PurifyingWater : MonoBehaviour
{
    private BoxCollider2D m_box2d;

    void Start()
    {
        m_box2d = GetComponentInChildren<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_box2d.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            
        }
    }
}
