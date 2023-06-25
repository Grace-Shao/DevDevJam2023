using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager Instance;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private List<Sound> musicList;
    [SerializeField] private List<Sound> sfxList;

    [Tooltip("Value between 0 and 1;")]
    private float musicVolume = 1;
    [Tooltip("Value between 0 and 1;")]
    private float sfxVolume = 1;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start() {
        musicSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;
        PlayMusic("Main Theme");
    }

    public void PlayMusic(string name) {
        foreach (Sound sound in musicList) {
            if (sound.name == name) {
                musicSource.clip = sound.clip;
                musicSource.Play();
                return;
            } Debug.LogWarning("No music clip named " + name + " was found.");
        }
    }

    public void PlaySFX(string name, float pitchVar) {
        foreach (Sound sound in sfxList) {
            if (sound.name == name) {
                sfxSource.PlayOneShot(sound.clip);
                sfxSource.pitch = 1 + Random.Range(-pitchVar, pitchVar);
                return;
            } Debug.LogWarning("No sound clip named " + name + " was found.");
        }
    }

    public void SetMusicVolume(float musicVolume) {
        this.musicVolume = musicVolume;
        musicSource.volume = musicVolume;
    }

    public void SetSFXVolume(float sfxVolume) {
        this.sfxVolume = sfxVolume;
        sfxSource.volume = sfxVolume;
    }
}

[System.Serializable]
public class Sound {
    public string name;
    public AudioClip clip;
}
