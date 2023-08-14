using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHight : MonoBehaviour
{
    [SerializeField]private Transform m_parentWater;
    [Header("삭제필요 버그고치고나서 숫자없애주기")]
    [SerializeField] private int waterCount=5;
    void Start()
    {
        m_parentWater = transform.GetComponentInParent<Transform>();
        
    }

    void Update()
    {
        
    }
}
