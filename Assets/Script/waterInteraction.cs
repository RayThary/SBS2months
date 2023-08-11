using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterInteraction : MonoBehaviour
{
    private Transform playerTrs;
    private Rigidbody2D playerRig2d;
    private BoxCollider2D box2d;

    void Start()
    {
        box2d = GetComponent<BoxCollider2D>();
        playerTrs = GameManager.instance.GetPlayerTransform();
        playerRig2d = playerTrs.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (box2d.IsTouchingLayers(LayerMask.GetMask("Player")))
        {

            //소환부분작용하고완성
            playerRig2d.velocity = new Vector2(playerRig2d.velocity.x, 1);
        }
        
    }
}
