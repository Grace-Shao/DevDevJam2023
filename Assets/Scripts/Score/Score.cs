using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{  
    private TextMeshProUGUI _scoreText;
    public int scoreNum;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText = FindObjectOfType<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        _scoreText.SetText($"Money: ${scoreNum}");
    }
}
