using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHight : MonoBehaviour
{
    [SerializeField]private Transform m_parentWater;
    [Header("�����ʿ� ���װ�ġ���� ���ھ����ֱ�")]
    [SerializeField] private int waterCount=5;
    void Start()
    {
        m_parentWater = transform.GetComponentInParent<Transform>();
        
    }

    void Update()
    {
        
    }
}
