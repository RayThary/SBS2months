using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerType : MonoBehaviour
{
    private Player player;

    private Transform GreenBackGround;
    private Transform BlueBackGround;
    private Transform RedBackGround;

    private Image green;
    private Image blue;
    private Image red;

    private Color alphaZero;
    private Color alphaHalf;



    void Start()
    {
        player = GameManager.instance.GetPlayerTransform().GetComponent<Player>();

        GreenBackGround = GetComponentInChildren<Transform>().Find("GreenBackGround");
        BlueBackGround = GetComponentInChildren<Transform>().Find("BlueBackGround");
        RedBackGround = GetComponentInChildren<Transform>().Find("RedBackGround");

        green = GreenBackGround.GetComponent<Image>();
        blue = BlueBackGround.GetComponent<Image>();
        red = RedBackGround.GetComponent<Image>();

        

        green.color = alphaZero;
        alphaZero.a = 0;
        alphaHalf.a = 0.7f;

    }

    void Update()
    {
        if (player.GetPlayerType() == Player.eType.Green)
        {
            green.color = alphaHalf;
            blue.color = alphaZero;
            red.color = alphaZero;
        }
        else if (player.GetPlayerType() == Player.eType.Blue)
        {
            green.color = alphaZero;
            blue.color = alphaHalf;
            red.color = alphaZero;
        }
        else if (player.GetPlayerType() == Player.eType.Red)
        {
            green.color = alphaZero;
            blue.color = alphaZero;
            red.color = alphaHalf;
        }
    }
}
