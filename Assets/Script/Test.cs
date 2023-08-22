using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float speed;
    public bool check;
    private Rigidbody2D rb;
    private Vector2 ve;
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
    }

  
    void Update()
    {
        ve = new Vector2(rb.velocity.x, speed);
        rb.velocity = ve;
            //StartCoroutine("fallWater");
        
        
    }

    IEnumerator fallWater()
    {
        if (check)
        {
            yield break;
        }
            yield return new WaitForSeconds(speed);
            Debug.Log("1");
            yield break;
    }
}
