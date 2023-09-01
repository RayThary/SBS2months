using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potal : MonoBehaviour
{
    private CapsuleCollider2D cap2d;
    void Start()
    {
        cap2d = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        if(cap2d.IsTouchingLayers(LayerMask.GetMask("Player"))) 
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {

            }
        }
    }
}
