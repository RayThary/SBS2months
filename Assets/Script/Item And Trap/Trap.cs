using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private Player player;
    public enum eTrapType
    {
        RedTrap,
        BlueTrap,
        GreenTrap,
    }
    [SerializeField] private eTrapType trapType;

    private SpriteRenderer m_spr;
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
        if (player.GetPlayerTrapHit() == true)
        {
            return;
        }
        if (m_box2d.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            if (player.GetPlayerTrapHitCheck())
            {
                if (trapType == eTrapType.GreenTrap)
                {
                    if (player.GetPlayerType() == Player.eType.Green)
                    {
                        player.SetPlayerTrapHit(true);
                        player.SetTrapHpRemove(true);
                    }
                }

                if (trapType == eTrapType.BlueTrap)
                {
                    if (player.GetPlayerType() == Player.eType.Blue)
                    {
                        player.SetPlayerTrapHit(true);
                        player.SetTrapHpRemove(true);
                    }
                }

                if (trapType == eTrapType.RedTrap)
                {
                    if (player.GetPlayerType() == Player.eType.Red)
                    {
                        player.SetPlayerTrapHit(true);
                        player.SetTrapHpRemove(true);
                    }
                }
            }
        }
    }

}
