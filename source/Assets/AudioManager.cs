using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;
    public AudioMixer Mixer;

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            Debug.Log("Adding " + s.name);
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.outputAudioMixerGroup = Mixer.FindMatchingGroups("Master")[0];
            s.source.loop = s.loop;
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            if (s.playOnAwake) Play(s.name);
        }
    }

    private void Start()
    {
        //Play("MainMenuTheme");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        Debug.Log("Playing " + s.name);
        s.source.Play();
    }
}