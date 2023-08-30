using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private Transform playerTrs;
    private Player player;
    public enum ItemName
    {
        Key,

    }
    
    //일단 지정해서써주는데 나중에 자기자신의 레이어or태그를 확인후 자기가어떤아이템인지확인으로 바꿔줄지고민필요 
    [SerializeField] private ItemName itemName;

    [SerializeField] private float moveMax;
    [SerializeField] private float ItemStandingSpeed;
    private Vector3 ItemPos;

    [SerializeField] private Vector3 followPos;
    [SerializeField] private int followDelay;
    [SerializeField] private Queue<Vector3> playerPos;

    private bool playerItemTouch;

    private bool playerKeyTouch = false;
    private bool doorCheck;

    private Collider2D collider2d;

    private void Awake()
    {
        playerPos = new Queue<Vector3>();
    }

    void Start()
    {
        ItemPos = transform.position;
        playerTrs = GameManager.instance.GetPlayerTransform();
        player = playerTrs.GetComponent<Player>();
        

        collider2d = GetComponent<Collider2D>();        
    }


    void Update()
    {
        ItemMove();
        
        if(itemName == ItemName.Key)
        {
            playerKeyTouch = player.PlayerKeyCheck();
            Key();
        }
    }

    private void ItemMove()
    {
        if (collider2d.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            playerItemTouch = true;
        }
        if (playerItemTouch)
        {
            watch();
            follow();
        }
        else
        {
            Vector3 dirPos = ItemPos;
            dirPos.y += moveMax * Mathf.Sin(Time.time * ItemStandingSpeed);
            transform.position = dirPos;
        }
    }
    private void watch()
    {
        if (!playerPos.Contains(playerTrs.position))
        {
            playerPos.Enqueue(playerTrs.position);
        }

        if (playerPos.Count > followDelay)
        {
            followPos = playerPos.Dequeue();
        }
        else if (playerPos.Count < followDelay)
        {
            followPos = playerTrs.position;
        }
    }

    private void follow()
    {
        transform.position = followPos;
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
