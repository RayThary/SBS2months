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
    [SerializeField]private bool playerKeyTouch = false;
    private float speed = 2f;

    private Collider2D collider2d;
    


    void Start()
    {

        playerTrs = GameManager.instance.GetPlayerTransform();
        player = playerTrs.GetComponent<Player>();


        collider2d = GetComponent<Collider2D>();
        
        
    }


    void Update()
    {
        
        if(itemName == ItemName.Key)
        {
            playerKeyTouch = player.PlayerKeyCheck();
            Key();
        }
    }

    private void Key()
    {
        if (collider2d.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            if (playerKeyTouch)
            {
                return;
            }
            
        }
    }

}
