using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    private float timer=0.0f;
    private float time=2;

    void Start()
    {
        Time.timeScale = 1.0f;
        timer = 0.0f;
    }

    
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= time)
        {
            SceneManager.LoadSceneAsync(1);
            GameManager.instance.SetReStart(true);
        }
    }
}
