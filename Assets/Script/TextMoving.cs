using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMoving : MonoBehaviour
{
    [SerializeField] private float moveMax;
    [SerializeField] private float speed;

    private Vector3 TextPos;

    void Start()
    {
        TextPos = transform.position;

    }

    void Update()
    {
        Vector3 dirPos = TextPos;
        dirPos.y = TextPos.y + moveMax * Mathf.Sin(Time.time * speed);
        transform.position = dirPos;
    }
}
