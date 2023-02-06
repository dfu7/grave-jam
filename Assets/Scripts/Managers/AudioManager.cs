using UnityEngine.Audio;
using System; 
using UnityEngine;
using System.Collections;

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

    public void QuietLevelMusic()
    {
        StartCoroutine(Fade(1.0f, 0.1f));
    }

    public IEnumerator Fade(float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

    public void RaiseLevelMusic()
    {
        StartCoroutine(Fade(1.0f, 0.75f));
    }
}