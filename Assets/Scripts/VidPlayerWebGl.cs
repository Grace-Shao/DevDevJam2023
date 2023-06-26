using UnityEngine;
using UnityEngine.Video;

// https://www.youtube.com/watch?v=9UE3hLSHMTE
public class VidPlayerWebGl : MonoBehaviour
{
    [SerializeField] string videoFileName;

    void Start()
    {
        PlayVideo();
    }

    public void PlayVideo()
    {
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();

        // if obj attached to script has videoPlayer
        if (videoPlayer)
        {
            // videoPath = Filepath
            string videoPath = System.IO.Path.Join(Application.streamingAssetsPath, videoFileName);
            //videoPath = videoPath.Replace('\\', '/');
            Debug.Log(Application.streamingAssetsPath);
            Debug.Log(videoPath);
            videoPlayer.url = videoPath;
            videoPlayer.Play();
        }
    }
}
