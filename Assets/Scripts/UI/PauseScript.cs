using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    #region Singleton
    private static PauseScript m_instance;
    public static PauseScript Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<PauseScript>();
            }
            return m_instance;
        }
    }
    #endregion
    private CanvasGroup m_canvasGroup;

    private bool m_paused;
    public bool IsPaused => m_paused;

    #region Technical
    #endregion 
    private void Start()
    {
        m_canvasGroup = GetComponent<CanvasGroup>();
        UnPauseGame();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    public void PauseGame()
    {
        m_paused = true;
        Time.timeScale = 0f;
        m_canvasGroup.alpha = 1f;
        AudioManager.Instance.PauseVolume();
    }
    public void UnPauseGame()
    {
        m_paused = false;
        Time.timeScale = 1f;
        m_canvasGroup.alpha = 0f;
        AudioManager.Instance.UnPauseVolume();
    }
    public void TogglePause()
    {
        if (m_paused) UnPauseGame();
        else PauseGame();
    }
    public void BackToMainMenu()
    {
        TransitionManager.Instance.LoadScene("Title");
        UnPauseGame();
    }
}
