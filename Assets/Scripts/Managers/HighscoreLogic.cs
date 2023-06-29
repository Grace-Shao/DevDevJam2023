using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighscoreLogic : MonoBehaviour {

    public static HighscoreLogic Instance;

    private float gameTime;
    private int highestScore;
    private bool gameStarted;
    private ScoreData scoreData;

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
        scoreData = new ScoreData();
        if (SceneManager.GetActiveScene().buildIndex == 2) {
            WaveBroadcast.Instance.OnWaveBroadcasted += HighscoreLogic_OnWaveBroadcasted;
            FoodStorage.Instance.OnShootFood += HighscoreLogic_OnShootFood;
            GameManager.Instance.OnEndingSceneLoad += HighscoreLogic_OnEndingSceneLoad;
        }
        TransitionManager.Instance.OnSceneChange += HighscoreLogic_OnSceneChange;
    }

    private void HighscoreLogic_OnSceneChange(int sceneID) {
        if (sceneID == 2) gameStarted = true;
    }

    private void HighscoreLogic_OnWaveBroadcasted() {
        scoreData.wavesPassed++;
    }

    private void HighscoreLogic_OnShootFood() {
        scoreData.foodsShot++;
    }
    private void HighscoreLogic_OnEndingSceneLoad() {
        CalculateHighscore();
    }

    public void CustomerFeedReaction() {
        scoreData.missedSales++;
    }

    public void CustomerFeedReaction(bool satisfied) {
        if (satisfied) scoreData.goodSales++;
        else scoreData.wrongSales++;
    }

    private void CalculateHighscore() {
        scoreData.score = Mathf.CeilToInt(scoreData.gameTime.x / 2f) + scoreData.wavesPassed * 20 
                                                 + scoreData.goodSales * 5 + Score.Instance.HighScore * 10;
        scoreData.gameTime = new Vector2Int((int) (gameTime / 60f), (int) (gameTime % 60));
        if (scoreData.score > highestScore) highestScore = scoreData.score;
    }

    public ScoreData GetScoreData() {
        return scoreData;
    }

    public int GetHighscore() {
        return highestScore;
    }

    // Update is called once per frame
    void Update() {
        if (SceneManager.GetActiveScene().buildIndex == 2) {
            if (gameStarted) {
                WaveBroadcast.Instance.OnWaveBroadcasted += HighscoreLogic_OnWaveBroadcasted;
                FoodStorage.Instance.OnShootFood += HighscoreLogic_OnShootFood;
                GameManager.Instance.OnEndingSceneLoad += HighscoreLogic_OnEndingSceneLoad;
                scoreData = new ScoreData();
                gameStarted = false;
            } gameTime += Time.deltaTime;
        }
    }
}

[System.Serializable]
public class ScoreData {
    public int score;
    public Vector2Int gameTime;
    public int wavesPassed;
    public int goodSales;
    public int wrongSales;
    public int missedSales;
    public int foodsShot;
}
