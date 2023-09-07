using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum eStage
    {
        Tutorial,
        Stage1,
        Stage2,
        Stage3,
    }
    [SerializeField]private eStage stage;//���罺�������� ǥ������ �̰ſ����� ��������ġ���޶��� ���߿� �����ص��ɵ�
    public static GameManager instance;
    
    private Player player;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        player = FindObjectOfType<Player>();
        
    }

    void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public Transform GetPlayerTransform()
    {
        return player.transform;
    }
    public eStage GetStage()
    {
        return stage;
    }

    public void SetStage(eStage _stage)
    {
        stage = _stage;
    }
    
}
