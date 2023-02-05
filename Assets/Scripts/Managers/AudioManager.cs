using UnityEngine.Audio;
using System; 
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    //Audio manager. Also handles randomizing the background music at start. 
    public Sound[] sounds;
    public static AudioManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this; 
        }
        else
        {
            Destroy(gameObject);
            return; 
        }
        DontDestroyOnLoad(gameObject); 
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = 1;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.mixerGroup; 
        }
    }
    
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); 
        if(s == null)
        {
            return; 
        }
        if (!s.source.isPlaying)
        {
            s.source.Play();
        }
    }

    public void StopPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        if (s.source.isPlaying)
        {
            s.source.Stop();
        }
    }

}