using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private bool inDangerZone = false;
    [SerializeField] private TextMeshProUGUI m_timetilloverTMP;
    private void Start()
    {
        m_timetilloverTMP.alpha = 0f;
        AudioManager.Instance.PlayMusic("Main Theme");
    }
    private void Update()
    {
        if (Score.Instance.SCORE <= 0 && !inDangerZone)
        {
            inDangerZone = true;
            StartCoroutine(DangerZoneStart());
        }
    }

    private IEnumerator DangerZoneStart()
    {
        float timeTillDie = 10f;
        while (Score.Instance.SCORE <= 0 && timeTillDie > 0)
        {
            
            yield return new WaitForSeconds(0.05f);
            timeTillDie -= 0.05f;
            if (timeTillDie < 6f)
            {
                m_timetilloverTMP.alpha = 1f;
                m_timetilloverTMP.text = $"TIME TILL OVER: {(int)timeTillDie}"; 
            }
        }

        if (Score.Instance.SCORE <= 0)
        {
            Debug.Log("Should Have loaded by now");
            TransitionManager.Instance.LoadScene("GameOver");
        }
        else
        {
            inDangerZone = false;
        }   
    }
}
