using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class WaterControl : MonoBehaviour
{
    [SerializeField] private AnimatorController m_WaterAnim2d;
    private Animator m_anim2d;
    void Start()
    {
        m_anim2d.SetFloat("WaterSpeed", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
