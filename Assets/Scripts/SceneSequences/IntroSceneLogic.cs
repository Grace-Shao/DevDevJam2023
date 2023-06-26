using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IntroSceneLogic : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    private VideoPlayer videoPlayer;
    private bool justOnce = false;

    // Start is called before the first frame update
    void Start() {
        AudioManager.Instance.PlayMusic("Boss Theme");
        videoPlayer = GetComponentInChildren<VideoPlayer>();
    }

    // Update is called once per frame
    void Update() {
        if (videoPlayer.clockTime > 1f && !videoPlayer.isPlaying && !justOnce)
        {
            justOnce = true;
            TransitionManager.Instance.LoadScene(sceneToLoad);
        }
    }
}
