using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditorInternal;
using UnityEngine;


public class Test : MonoBehaviour
{
    public float speed;
    public bool check;
    private Rigidbody2D rb;
    private Vector2 ve;
    private BoxCollider2D box2d;
    private SpriteRenderer spr;
    private Color color;

    private bool x = true;
    private float timer=0.0f;
    public float time=0.5f;

    public float tx=0.5f;
    void Start()
    {
      box2d = GetComponent<BoxCollider2D>();
        spr = GetComponent<SpriteRenderer>();

        color=spr.color;
        
    }

  
    void Update()
    {

        spr.sortingOrder = 10;
    

    }


    
}
