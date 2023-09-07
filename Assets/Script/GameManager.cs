using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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

    public Transform GetPlayerTransform()
    {
        return player.transform;
    }
   
    
}
