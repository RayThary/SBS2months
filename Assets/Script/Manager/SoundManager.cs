using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [Range(0, 1)] public float BackGroundVolume = 1f;
    [Range(0, 1)] public float SFXVolume = 1f;//조절하면 0부터 최대까지 설정가능
    [SerializeField]private AudioSource m_backGroundSound;
    [SerializeField] private AudioClip m_backGroundClip;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine("bgStart");
        
    }
    IEnumerator bgStart()
    {
        yield return new WaitForSeconds(3);
        bgSoundPlay(m_backGroundClip);
    }



    public void SoundPlayer(string soundName,AudioClip clip,float _soundVolume)
    {
        GameObject go = new GameObject(soundName + "Sound");
        AudioSource audiosoutce = go.AddComponent<AudioSource>();
        audiosoutce.clip = clip;
        audiosoutce.volume = _soundVolume* SFXVolume;
        audiosoutce.Play();

        Destroy(go, clip.length);
    }

    public void bgSoundPlay(AudioClip clip)
    {
        m_backGroundSound.clip = clip;
        m_backGroundSound.loop = true;
        m_backGroundSound.volume = BackGroundVolume;
        m_backGroundSound.Play();
    }
}
