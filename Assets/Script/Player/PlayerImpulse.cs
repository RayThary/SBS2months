using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImpulse : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource _source;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            impulse();
        }
    }

    private void impulse()
    {
        _source.GenerateImpulse();
    }
}
