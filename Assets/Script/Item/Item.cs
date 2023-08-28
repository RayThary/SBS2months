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
    
    //�ϴ� �����ؼ����ִµ� ���߿� �ڱ��ڽ��� ���̾�or�±׸� Ȯ���� �ڱⰡ�����������Ȯ������ �ٲ���������ʿ� 
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
