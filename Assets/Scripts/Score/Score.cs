using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    #region Singleton
    private static Score m_instance;
    public static Score Instance
    {
        get 
        { 
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<Score>();
            }
            return m_instance; 
        }
    }
    #endregion

    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private int _scoreNum;

    public int SCORE 
    {
        get { return _scoreNum; }
        set { _scoreNum = value; }
    }

    // Update is called once per frame
    void Update()
    {
        if (_scoreNum > 0) _scoreText.color = Color.green;
        else if (_scoreNum < 0) _scoreText.color = Color.red;
        else _scoreText.color = Color.white;
        _scoreText.SetText($"{_scoreNum}");
    }
}
