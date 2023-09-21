using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private Player player;
    private BoxCollider2D m_box2d;

   
    void Start()
    {
        Transform playerTrs = GameManager.instance.GetPlayerTransform();
        player = playerTrs.GetComponent<Player>();

        m_box2d = GetComponent<BoxCollider2D>();
    }

    
    void Update()
    {
        hitCheck();
    }

    private void hitCheck()
    {
        if (m_box2d.IsTouchingLayers(LayerMask.GetMask("Player")))
        {

        }
    }
}
