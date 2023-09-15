using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float speed;
    public bool check;
    private Rigidbody2D rb;
    private Vector2 ve;
    private BoxCollider2D box2d;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("1");
    }
    void Start()
    {
      box2d = GetComponent<BoxCollider2D>();
    }

  
    void Update()
    {
        if (box2d.IsTouchingLayers(LayerMask.GetMask("test")))
        {
            Debug.Log("2");
        }
        
        
    }


    
}
