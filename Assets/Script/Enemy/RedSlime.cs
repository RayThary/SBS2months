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

    //�÷��̾ �νĹ����γ������� ���ʵڿ� ���ư��� üũ���ִºκ�
    private bool m_timerStart = false;
    private float m_timer = 0.0f;
    [SerializeField] private float m_forgetPlayerTime = 3f;
    private bool returnStart;

    //�÷��̾�ν����������ڸ�
    private Vector3 m_vecStartPoint;

    private bool enemyDeathCheck = false;

    private Transform m_playerTrs;
    private BoxCollider2D m_box2d;//�÷��̾��� �νĹ��� �ڽ�ũ��� �ۿ����������ָ��
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
