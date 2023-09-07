using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotalMove : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float maxYMove = 0.1f;
    private Vector3 me;
    private void Start()
    {
        me = transform.position;
    }

    void Update()
    {
        Vector3 dirPos = me;
        dirPos.y = me.y + maxYMove * Mathf.Sin(Time.time * speed);
        transform.position = dirPos;

    }
}
