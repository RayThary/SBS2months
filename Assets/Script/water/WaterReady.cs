using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterReady : MonoBehaviour
{
    [SerializeField]private WaterMove watermove;
    [SerializeField] private Transform waterHight;
    void Start()
    {
    }

    void Update()
    {
        watermove = GetComponentInChildren<WaterMove>();
        
        waterHightCheck();

    }
    private void waterHightCheck()
    {
        if (watermove != null)
        {
            waterHight = watermove.transform;
           
        }
    }
}
