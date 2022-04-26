using UnityEngine;

[System.Serializable]
public class Sound 
{
    public string name;
    
    public AudioClip clip;

    [Range(0,1)]
    public float volume;

    public bool loop;

    [Range(-3,3)]
    public float pitch;

    [HideInInspector]
    public AudioSource source; 
}
