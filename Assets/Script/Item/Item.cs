using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemName
    {
        Key,

    }
    //일단 지정해서써주는데 나중에 자기자신의 레이어or태그를 확인후 자기가어떤아이템인지확인으로 바꿔주기바람
    [SerializeField] private ItemName itemName;
    
    [SerializeField]private Collider2D collider2d;

    void Start()
    {
        collider2d = GetComponent<Collider2D>();       
    }

    // Update is called once per frame
    void Update()
    {
        
        if(itemName == ItemName.Key)
        {
            Key();
        }
    }

    private void Key()
    {
        if (collider2d.IsTouchingLayers(LayerMask.GetMask("Player"))) 
        {

        }
    }

}
