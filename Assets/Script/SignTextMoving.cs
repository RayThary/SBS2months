using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignTextMoving : MonoBehaviour
{
    [SerializeField] private float moveMax;
    [SerializeField] private float speed;

    private Vector3 SignPos;

    void Start()
    {
        SignPos = transform.position;

    }

    void Update()
    {
        Vector3 dirPos = SignPos;
        dirPos.y = SignPos.y + moveMax * Mathf.Sin(Time.time * speed);
        transform.position = dirPos;
    }
}
