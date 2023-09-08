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
    void Start()
    {
      box2d = GetComponent<BoxCollider2D>();
    }

  
    void Update()
    {
        if (box2d.IsTouchingLayers(LayerMask.GetMask("GreenSlime")))
        {
            Debug.Log("2");
        }
        
        
    }


    
}
