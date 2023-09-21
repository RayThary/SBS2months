using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBackGround : MonoBehaviour
{
    [SerializeField] private float speed = 20f;

    private BoxCollider2D m_box2d;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BackGround")
        {

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    void Start()
    {
        m_box2d= GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x * Time.deltaTime * speed, 0, 0);
    }
}
