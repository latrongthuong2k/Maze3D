using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Linq;
using Unity.VisualScripting;

[Serializable]public class Sound
{
    public string soundName;
    [Range(0f, 1f)]
    public float volume = 1;
    [Range(0.1f, 3f)]
    public float pitch;
    public AudioClip clip;
    public bool loop;
    //
    [HideInInspector] public AudioSource source;
    //
}
public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance => instance;
    //
    public Sound[] Sounds;
    //

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in Sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            if(s.clip == null)
            {
                Debug.LogWarning("don't have any sound in list sounds ! ");
                return;
            }
            else
            {
                s.source.clip = s.clip;
            }
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        instance = this;
        PlayBGM("BGM");
    }

    private void Update()
    {
        
    }
    //-------------------------------------------------------------------
    public void PlayAnySoundSE(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.soundName == name);
        if (s == null || name == "BGM")
        {
            Debug.LogWarning("Sound " + name + " Not found!");
            Debug.LogWarning("Sound " + name + " can't play, pls in put SE name!");
            return;
        }
        s.source.PlayOneShot(s.source.clip);
    }
    public void PlayBGM(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.soundName == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " Not found!");
            return;
        }
        s.source.Play();
    }
    public void StopAnySound(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.soundName == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " Not found!");
            return;
        }

        s.source.Stop();
    }
    //------------------------------------------------------------------
    public bool CheckIsPlaying(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.soundName == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " Not found!");
        }
        return s.source.isPlaying;
    }
}