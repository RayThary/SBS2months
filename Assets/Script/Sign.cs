using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sign : MonoBehaviour
{
    private BoxCollider2D box2d;

    private TextMeshPro signText;
    [SerializeField] private bool tutorialUICheck = false;
    private bool tutorialUISet;
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

            signText.text = "표지판 열기 z키";
            checkTutorialUI();
        }
        else
        {
            tutorialUISet = false;
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
        else if (Input.GetKeyDown(KeyCode.Z) && tutorialUICheck)
        {
            tutorialUI.SetActive(false);
            tutorialUICheck = false;
        }
    }
}
