using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_listCloud;
    [SerializeField] private Transform m_trsSpawn;
    [SerializeField] private Transform m_trsRemove;
    [SerializeField]private float m_spawnRange;
    private float m_spawnTime = 8;
    private float m_Timer = 0;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(new Vector2(m_trsSpawn.position.x, m_trsSpawn.position.y - m_spawnRange),
            new Vector3(m_trsSpawn.position.x, m_trsSpawn.position.y + m_spawnRange));
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        m_Timer += Time.deltaTime;
        if (m_Timer >= m_spawnTime)
        {
            spawnCloud();
        }
    }

    private void spawnCloud()
    {
        int iRand = Random.Range(0, m_listCloud.Count);
        GameObject objCloud = m_listCloud[iRand];

        Vector2 downCloudPos = new Vector2(m_trsSpawn.position.x, m_trsSpawn.position.y - m_spawnRange);
        Vector2 upCloudPos = new Vector2(m_trsSpawn.position.x, m_trsSpawn.position.y + m_spawnRange);
        float fRand = Random.Range(downCloudPos.y, upCloudPos.y);
        Vector3 cloudPos = m_trsSpawn.position;
        cloudPos.y = fRand;

        GameObject obj = Instantiate(objCloud, cloudPos, Quaternion.identity, transform);
        m_Timer = 0;
    }

    public Transform GetEndTrs()
    {
        return m_trsRemove;
    }
}
