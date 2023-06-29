using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveBroadcast : MonoBehaviour
{
    #region Singleton
    private static WaveBroadcast m_instance;
    public static WaveBroadcast Instance
    {
        get 
        { 
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<WaveBroadcast>();
            }
            return m_instance; 
        }  
    }
    #endregion

    public event Action OnWaveBroadcasted;
    private TextMeshProUGUI m_TextMeshProUGUI;

    private float targetAlpha = 1f;

    private void Start()
    {
        m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
        m_TextMeshProUGUI.alpha = 0f;
        BroadcastWave("Wave 1");
    }

    public void BroadcastWave(string waveString)
    {
        OnWaveBroadcasted?.Invoke();
        if (m_TextMeshProUGUI) {
            m_TextMeshProUGUI.text = waveString;
            StartCoroutine(FancyDisplay());
        }
    }

    private IEnumerator FancyDisplay()
    {
        targetAlpha = 1f;
        while (m_TextMeshProUGUI.alpha != targetAlpha) {
            m_TextMeshProUGUI.alpha = Mathf.MoveTowards(m_TextMeshProUGUI.alpha, targetAlpha, Time.deltaTime * 2f);
            yield return null;
        } yield return new WaitForSeconds(3f);
        targetAlpha = 0f;
        while (m_TextMeshProUGUI.alpha != targetAlpha) {
            m_TextMeshProUGUI.alpha = Mathf.MoveTowards(m_TextMeshProUGUI.alpha, targetAlpha, Time.deltaTime * 2f);
            yield return null;
        }
    }
}
