using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Player player;
    private Transform playerTrs;

    private bool playerKeyCheck;
    private bool OpenDoor = false;
    private BoxCollider2D box2d;
    [SerializeField]private BoxCollider2D box2dchile;

    [SerializeField] private float speed = 4;

    void Start()
    {
        box2d = GetComponent<BoxCollider2D>();
        Transform playerTrs = GameManager.instance.GetPlayerTransform();
        player = playerTrs.GetComponent<Player>();
        box2dchile = GetComponentInChildren<BoxCollider2D>(); 
    }

    
    void Update()
    {
       
        playerKeyCheck = player.PlayerKeyCheck();
        if (box2d.IsTouchingLayers(LayerMask.GetMask("Player")) && playerKeyCheck)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                OpenDoor = true;
            }
        }
        DoorCheck();
    }

    private void DoorCheck()
    {
        if (!OpenDoor)
        {
            return;
        }
        if (transform.localRotation.eulerAngles.y <= 90)
        {
            transform.Rotate(Vector3.up * speed * Time.deltaTime);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}