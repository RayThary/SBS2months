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

    private RaycastHit2D checkPlayer;
    private BoxCollider2D box2d;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, -fallCheckDistance, transform.position.z));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            player.SetTrapHpRemove(true);
        }
        if (collision.gameObject.tag == "Ground" && outGround)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            outGround = true;
        }
    }
    void Start()
    {
        Transform playerTrs = GameManager.instance.GetPlayerTransform();
        player =  playerTrs.GetComponent<Player>(); 
        box2d = GetComponent<BoxCollider2D>();
    }


    void Update()
    {
        playerCheck();
        fallTrapMove();
    }

    private void playerCheck()
    {
        checkPlayer = Physics2D.Raycast(transform.position, Vector2.down, fallCheckDistance, LayerMask.GetMask("Player"));
        if (checkPlayer.collider != null)
        {
            fallCheck = true;
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
}
