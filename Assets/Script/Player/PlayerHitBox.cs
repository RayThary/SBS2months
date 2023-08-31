using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    private Player player;
    [SerializeField] protected HitType hitType;

    public enum HitType 
    {
        Ground,
        Item,
    }

    public enum HitBoxType
    {
        Enter,
        Stay,
        Exit,
    }
    void Start()
    {
        //player = GameManager.instance.GetPlayerTransform().GetComponent<Player>();
        
        player = GetComponentInParent<Player>();
    }

 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.OnTriggerPlayer(HitBoxType.Enter, hitType, collision);

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        player.OnTriggerPlayer(HitBoxType.Stay, hitType, collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.OnTriggerPlayer(HitBoxType.Exit, hitType, collision);
    }
}
