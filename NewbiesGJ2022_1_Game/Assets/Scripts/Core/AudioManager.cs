using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<Sound> soundList;
    public static AudioManager Instance; 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return; 
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in soundList)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch; 
        }
    }

    public void Play(string name)
    {
        Sound s = soundList.Find(sound => sound.name == name); 
        
        if(s == null)
        {
            Debug.Log("sound " + s.name + " not found!"); 
        }

        s.source.Play(); 
    }

    public void Stop(string name)
    {
        Sound s = soundList.Find(sound => sound.name == name);

        if (s == null)
        {
            Debug.Log("sound " + s.name + " not found!");
        }

        s.source.Stop();
    }
}
