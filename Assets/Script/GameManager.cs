using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Player player;
    private waterCourse water;

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
