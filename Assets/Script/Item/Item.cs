using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemName
    {
        Key,

    }
    //�ϴ� �����ؼ����ִµ� ���߿� �ڱ��ڽ��� ���̾�or�±׸� Ȯ���� �ڱⰡ�����������Ȯ������ �ٲ��ֱ�ٶ�
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
