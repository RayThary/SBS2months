
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioSource m_backGroundSound;
    [SerializeField] private AudioMixer m_mixer;
    [SerializeField] private AudioClip m_backGroundClip;
    [SerializeField] private AudioSource m_btnAudioSource;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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

    public void BackGroundVolume(float _volume)
    {
        m_mixer.SetFloat("BackGround", Mathf.Log10(_volume) * 20);
    }

    public void SFXVolume(float _volume)
    {
        m_mixer.SetFloat("SFX", Mathf.Log10(_volume) * 20);
    }


    public void SoundPlayer(string soundName, AudioClip clip)
    {
        GameObject go = new GameObject(soundName + "Sound");
        AudioSource audiosoutce = go.AddComponent<AudioSource>();
        audiosoutce.outputAudioMixerGroup = m_mixer.FindMatchingGroups("SFX")[0];
        audiosoutce.clip = clip;
        audiosoutce.volume = 0.5f;
        audiosoutce.Play();

        Destroy(go, clip.length);
    }

    public void ButtonPlay(AudioClip clip)
    {
        m_btnAudioSource.outputAudioMixerGroup = m_mixer.FindMatchingGroups("SFX")[0];
        m_btnAudioSource.clip = clip;
        m_btnAudioSource.loop = false;
        m_btnAudioSource.volume = 0.5f;
        m_btnAudioSource.Play();
        
    }

    public void bgSoundPlay(AudioClip clip)
    {
        m_backGroundSound.outputAudioMixerGroup = m_mixer.FindMatchingGroups("BackGround")[0];
        m_backGroundSound.clip = clip;
        m_backGroundSound.loop = true;
        m_backGroundSound.volume = 1f;
        m_backGroundSound.Play();
    }
}
