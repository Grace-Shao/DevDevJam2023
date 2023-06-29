using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingSceneLogic : MonoBehaviour {

    [SerializeField] private float waitTime;
    [SerializeField] private Image targetBackground;
    [SerializeField] private Sprite[] backgroundSprites;
    private ScoreData scoreData;

    // Start is called before the first frame update
    void Start() {
        scoreData = HighscoreLogic.Instance.GetScoreData();
        if (HighscoreLogic.Instance.GetHighscore() <= scoreData.score) {
            targetBackground.sprite = backgroundSprites[0];
            AudioManager.Instance.PlayMusic("Win Theme");
        } else {
            targetBackground.sprite = backgroundSprites[1];
            AudioManager.Instance.PlayMusic("Lose Theme");
        } StartCoroutine(BeginStatDisplay());
    }

    IEnumerator BeginStatDisplay() {
        yield return new WaitForSeconds(waitTime);
        GetComponent<FinalResult>().BeginDisplay(scoreData);
    }
}
