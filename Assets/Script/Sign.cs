using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    private BoxCollider2D box2d;

    private TextMesh signText;

    void Start()
    {
        box2d = GetComponent<BoxCollider2D>();
        signText= GetComponentInChildren<TextMesh>();
    }

    
    void Update()
    {
        if (box2d.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            signText.text = "Press the \"z\" key";
        }
        else
        {
            signText.text = "";
        }
    }
}
