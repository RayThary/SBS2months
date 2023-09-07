using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private Transform playerTrs;
    private Player player;
    private Transform ItemParentTrs;
    private Rigidbody2D playerRig2d;

    public enum ItemName
    {
        Key,
    }

    //일단 지정해서써주는데 나중에 자기자신의 레이어or태그를 확인후 자기가어떤아이템인지확인으로 바꿔줄지고민필요 
    [SerializeField] private ItemName itemName;

    [SerializeField] private float moveMax;
    [SerializeField] private float ItemStandingSpeed;
    private Vector3 ItemPos;

    [SerializeField] private float followSpeed;
    [SerializeField] private bool right;

    private bool playerItemTouch;

    private bool playerKeyTouch = false;
    private bool doorCheck;
    private Collider2D collider2d;


    void Start()
    {
        ItemPos = transform.position;
        playerTrs = GameManager.instance.GetPlayerTransform();
        player = playerTrs.GetComponent<Player>();
        playerRig2d = playerTrs.GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();
        ItemParentTrs = playerTrs.GetComponentInChildren<Transform>().Find("ItemParent");
    }


    void Update()
    {
        ItemMove();

        if (itemName == ItemName.Key)
        {
            playerKeyTouch = player.PlayerKeyCheck();
            Key();
        }
    }

    private void ItemMove()
    {
        if (playerItemTouch)
        {
            follow();
        }
        else
        {
            Vector3 dirPos = ItemPos;
            dirPos.y += moveMax * Mathf.Sin(Time.time * ItemStandingSpeed);
            transform.position = dirPos;
        }
        if (playerKeyTouch)
        {
            return;
        }
        if (collider2d.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            playerItemTouch = true;
        }
    }


    private void follow()
    {
        Vector2 TargetPos = ItemParentTrs.position;
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            right = false;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            right = true;
        }

        if (right)
        {
            TargetPos.x += 1;
        }
        else
        {
            TargetPos.x -= 1;
        }

        transform.position = Vector2.MoveTowards(transform.position, TargetPos, followSpeed * Time.deltaTime);
    }

    private void Key()
    {

        if (playerItemTouch)
        {
            doorCheck = player.PlayerdoorKeyCheck();
            if (doorCheck)
            {
                Destroy(gameObject);
            }
        }
    }

}
