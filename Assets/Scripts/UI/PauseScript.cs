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
    [SerializeField] private GameObject m_pauseGameObject;

    private bool m_paused;
    public bool IsPaused => m_paused;

    #region Technical
    #endregion 
    private void Start()
    {
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
        m_pauseGameObject.SetActive(true);
        m_paused = true;
        Time.timeScale = 0f;  
        AudioManager.Instance.PauseVolume();
    }
    public void UnPauseGame()
    {
        m_pauseGameObject.SetActive(false);
        m_paused = false;
        Time.timeScale = 1f;
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
