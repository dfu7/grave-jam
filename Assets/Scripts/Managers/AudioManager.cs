using UnityEngine.Audio;
using System; 
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    //Audio manager. Also handles randomizing the background music at start. 
    public Sound[] sounds;

    [SerializeField] private AudioClip MenuClip;
    [SerializeField] private AudioClip LevelClip;

    public AudioSource audioSource;
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
        /*foreach(Sound s in sounds)
        {
            Debug.Log("We are creating new sources");
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = false;
            s.source.outputAudioMixerGroup = s.mixerGroup; 
        }*/


    }

    public void PlayMenuMusic()
    {
        audioSource.clip = MenuClip;
        audioSource.Play();
    }

    public void PlayLevelMusic()
    {
        audioSource.clip = LevelClip;
        audioSource.Play();
    }
    
    public void Play(string name)
    {
        /*Debug.Log("Play is getting called");
        Sound s = Array.Find(sounds, sound => sound.name == name); 
        if(s == null)
        {
            return; 
        }
        if (!s.source.isPlaying)
        {
            audioSource.clip = s.source.clip;
            audioSource.Play();
        }*/
    }

    /*public void StopPlaying(string name)
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
    }*/

}