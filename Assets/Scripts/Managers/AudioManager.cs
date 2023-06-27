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
    private float timer;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start() {
        musicSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;
        TransitionManager.Instance.OnSceneChange += Instance_OnSceneChange;
    }

    private void Instance_OnSceneChange(int sceneID) {
        if (sceneID != 4) StopAllCoroutines();
        musicSource.volume = musicVolume;
        switch (sceneID) {
            case 0:
                if (musicSource.clip != musicList[0].clip 
                    && musicSource.clip != musicList[1].clip) PlayMusic("Title Intro", "Title Loop");
                break;
            case 2:
                PlayMusic("Theme Intro", "Theme Loop");
                break;
            case 3:
                PlayMusic("Lose");
                break;
        }
    }

    private void Update() {
        if (timer > 0) timer -= Time.deltaTime;
    }

    public void PlayMusic(string intro, string loop) {
        foreach (Sound sound in musicList) {
            if (sound.name == intro) {
                musicSource.clip = sound.clip;
                StartCoroutine(WaitForIntro(sound.clip.length, loop));
                musicSource.Play();
                return;
            }
        }
    }

    public void PlayMusic(string name, bool canLoop = true) {
        foreach (Sound sound in musicList) {
            if (sound.name == name) {
                musicSource.loop = canLoop;
                musicSource.clip = sound.clip;
                musicSource.Play();
                return;
            } 
        } Debug.LogWarning("No music clip named " + name + " was found.");
    }

    IEnumerator WaitForIntro(float timer, string loop) {
        yield return new WaitForSeconds(timer);
        PlayMusic(loop);
    }

    public void PlaySFX(string name, float pitchVar = 0) {
        foreach (Sound sound in sfxList) {
            if (sound.name == name) {
                sfxSource.PlayOneShot(sound.clip);
                sfxSource.pitch = 1 + Random.Range(-pitchVar, pitchVar);
                return;
            } 
        } Debug.LogWarning("No sound clip named " + name + " was found.");
    }
    public void PauseVolume()
    {
        musicSource.volume = musicVolume * 0.4f;
        sfxSource.volume = 0;
    }
    public void UnPauseVolume()
    {
        musicSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;
    }
    public void SetMusicVolume(float musicVolume) {
        this.musicVolume = musicVolume;
        musicSource.volume = PauseScript.Instance.IsPaused ? musicVolume * 0.4f : musicVolume;
    }
    public void SetSFXVolume(float sfxVolume) {
        this.sfxVolume = sfxVolume;
        sfxSource.volume = PauseScript.Instance.IsPaused ? 0 : sfxVolume;
    }

    public void FadeMusic() {
        StartCoroutine(IFadeMusic());
    }

    IEnumerator IFadeMusic() {
        while (musicSource.volume > 0) {
            musicSource.volume -= Time.deltaTime * 2f;
            yield return null;
        }
    }
}

[System.Serializable]
public class Sound {
    public string name;
    public AudioClip clip;
}
