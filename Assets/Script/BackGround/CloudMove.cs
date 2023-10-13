using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_listCloud;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void spawnCloud()
    {
        int iRand = Random.Range(0, m_listCloud.Count);
        GameObject objCloud = m_listCloud[iRand];
        
    }
}
