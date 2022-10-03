using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;
    public static AudioManager instance;

    // Start is called before the first frame update
    void Start()
    {
        Play("background");
        Play("marketAmbiente");
    }

    // Awake is called before the Start method
    void Awake()
    {
        if ( instance == null )
            instance = this;
            
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach ( Sound s in sounds )
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find( sounds, sound => sound.name == name );

        if ( s == null )
        {
            Debug.LogError("AudioManager:: s is NULL. Sound: "+name+" not found!");
            return;
        }

        if ( !s.source.isPlaying )
            s.source.Play();
    }

    public void stopPlaying(string name)
    {
        Sound s = Array.Find( sounds, sound => sound.name == name );

        if ( s == null )
        {
            Debug.LogError("AudioManager:: s is NULL. Sound: "+name+" not found!");
            return;
        }

        if ( s.source.isPlaying )
            s.source.Stop();
    }
}
