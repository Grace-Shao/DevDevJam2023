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
    private TextMeshProUGUI m_TextMeshProUGUI;

    private void Start()
    {
        m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
        m_TextMeshProUGUI.alpha = 0f;
    }

    public void BroadcastWave(string waveString)
    {
        m_TextMeshProUGUI.text = waveString;
        StartCoroutine(FancyDisplay());
    }

    private IEnumerator FancyDisplay()
    {
        m_TextMeshProUGUI.alpha = 1.0f;
        yield return new WaitForSeconds(3f);
        m_TextMeshProUGUI.alpha = 0f;
    }
}
