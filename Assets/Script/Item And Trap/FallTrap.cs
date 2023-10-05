using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallTrap : MonoBehaviour
{
    private Player player;

    private bool fallCheck = false;
    [SerializeField] private bool outGround = false;

    [SerializeField] private float fallCheckDistance = 6;
    [SerializeField] private float fallSpeed = 3;
    private bool oneHit = false;

    private RaycastHit2D checkPlayer;
    private BoxCollider2D box2d;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, -fallCheckDistance, transform.position.z));
    }

    void Start()
    {
        Transform playerTrs = GameManager.instance.GetPlayerTransform();
        player = playerTrs.GetComponent<Player>();
        box2d = GetComponent<BoxCollider2D>();
        box2d.enabled = false;
    }


    void Update()
    {
        playerCheck();
        fallTrapMove();
        hitCheck();
    }

    private void playerCheck()
    {
        checkPlayer = Physics2D.Raycast(transform.position, Vector2.down, fallCheckDistance, LayerMask.GetMask("Player"));
        if (checkPlayer.collider != null)
        {
            fallCheck = true;
            box2d.enabled = true;
        }
    }

    private void fallTrapMove()
    {
        if (fallCheck == false)
        {
            return;
        }
        transform.position += Vector3.down * Time.deltaTime * fallSpeed;
    }

    private void hitCheck()
    {
        if (box2d.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            Destroy(gameObject);
            if (player.GetNoHitCheck() == false)
            {
                player.SetTrapHpRemove(true);
            }
        }
    }
}
