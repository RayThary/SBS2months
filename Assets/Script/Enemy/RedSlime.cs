using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSlime : MonoBehaviour
{
    public enum SlimeType
    {
        Random,
        Grass,
        Slime,
    }
    [SerializeField] private SlimeType eSlimeType;
    private int checkSlime;

    [SerializeField] private Sprite m_SprHideing;
    private SpriteRenderer m_Spr;

    private bool PlayerCheck = false;
    private bool playerHitCheck;

    //플레이어가 인식범위로나갔을때 몇초뒤에 돌아갈지 체크해주는부분
    private bool m_timerStart = false;
    private float m_timer = 0.0f;
    [SerializeField] private float m_forgetPlayerTime = 3f;
    private bool returnStart;

    //플레이어를인식했을때의자리
    private Vector3 m_vecStartPoint;

    private bool enemyDeathCheck = false;

    private Transform m_playerTrs;
    private BoxCollider2D m_box2d;//플레이어의 인식범위 박스크기로 밖에서조절해주면됨
    private Rigidbody2D m_rig2d;

    private Player player;
    private EnemyHitBox hitBox;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
