using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Door : MonoBehaviour
{
    private Player player;

    [SerializeField] private bool tutorialText;
    private TextMeshPro doorText;
    private Transform doorTextTrs;

    [SerializeField] private Transform doorPillarTrs;
    private BoxCollider2D doorPillarBox2d;

    private bool playerKeyCheck;
    [SerializeField] private bool OpenDoor = false;
    private bool DoorKeyCheck=false;
    [SerializeField] private BoxCollider2D box2d;
    [SerializeField] private BoxCollider2D box2dchile;

    [SerializeField] private float speed = 4;

    void Start()
    {
        doorText = GetComponentInChildren<TextMeshPro>();
        doorTextTrs = GetComponentInChildren<Transform>().Find("DoorText");

        box2d = GetComponent<BoxCollider2D>();
        doorPillarBox2d = doorPillarTrs.GetComponent<BoxCollider2D>();
        Transform playerTrs = GameManager.instance.GetPlayerTransform();
        player = playerTrs.GetComponent<Player>();
        box2dchile = GetComponentInChildren<BoxCollider2D>();

    }


    void Update()
    {
        doorTutorialText();
        playerKeyCheck = player.PlayerKeyCheck();

        if (playerKeyCheck)
        {
            DoorKeyCheck = true;
        }

        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (DoorKeyCheck)
            {
                if (box2d.IsTouchingLayers(LayerMask.GetMask("Player")))
                {
                    OpenDoor = true;
                    DoorKeyCheck = false;
                }
            }
        }
        DoorCheck();
    }

    private void doorTutorialText()
    {
        if (tutorialText == false)
        {
            doorTextTrs.gameObject.SetActive(false);
            return;
        }

        if (box2d.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            doorText.text = "문열기 z키\n(열쇠있을때만가능)";
        }
        else
        {
            doorText.text = "";
        }
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
            doorPillarBox2d.enabled = false;
        }
    }
}