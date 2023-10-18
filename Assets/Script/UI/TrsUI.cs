using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrsUI : MonoBehaviour
{
    private Transform m_TrsUI;
    void Start()
    {
        m_TrsUI=GetComponent<Transform>();
    }

    public Transform GetTrsUI()
    {
        return m_TrsUI;
    }
}
    
   
