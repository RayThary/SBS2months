using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sign : MonoBehaviour
{
    private BoxCollider2D box2d;

    private TextMeshPro signText;
    private bool tutorialUICheck = false;
    [SerializeField] private GameObject tutorialUI;

    void Start()
    {
        box2d = GetComponent<BoxCollider2D>();
        signText = GetComponentInChildren<TextMeshPro>();
    }


    void Update()
    {
        if (box2d.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            checkTutorialUI();
            signText.text = "표지판 열기 z키";
        }
        else
        {
            signText.text = "";
            tutorialUI.SetActive(false);
            tutorialUICheck = false;
        }

    }

    private void checkTutorialUI()
    {
        
        if (Input.GetKeyDown(KeyCode.Z) && !tutorialUICheck)
        {
            tutorialUI.SetActive(true);
            tutorialUICheck = true;
        }
        else if (tutorialUICheck && Input.GetKeyDown(KeyCode.Z))
        {
            tutorialUI.SetActive(false);
            tutorialUICheck = false;
        }
    }
}
