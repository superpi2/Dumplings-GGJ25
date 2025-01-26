using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
    }

    public Sound[] soundsRef;
    static Dictionary<string, Sound> sounds;

    AudioSource source;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        source = GetComponent<AudioSource>();

        if (sounds == null)
        {
            sounds = new Dictionary<string, Sound>();

            foreach (Sound sound in soundsRef)
            {
                sounds.Add(sound.name, sound);
            }

            PlayBGM("bgMusic");
        }
    }

    public void PlayBGM(string soundName)
    {
        Sound target = sounds[soundName];

        if (source.clip != target.clip)
        {
            source.Stop();
            source.clip = target.clip;
            source.Play();
        }
    }

    public void PlaySFX(string soundName)
    {
        Sound target = sounds[soundName];

        source.PlayOneShot(target.clip);
    }
}