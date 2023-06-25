using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Update()
    {
        if (Score.Instance.SCORE <= 0)
        {
            StartCoroutine(DangerZoneStart());
        }
    }

    private IEnumerator DangerZoneStart()
    {
        float timeTillDie = 15f;
        while (Score.Instance.SCORE <= 0 && timeTillDie > 0)
        {
            timeTillDie -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }

        if (Score.Instance.SCORE <= 0)
        {
            TransitionManager.Instance.LoadScene("GameOverScene");
        }
    }
}
