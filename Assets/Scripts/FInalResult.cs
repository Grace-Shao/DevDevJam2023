using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class FInalResult : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayMusic("Lose Theme");
        GetComponent<TextMeshProUGUI>().text = Score.Instance.HighScore.ToString();

        Destroy(Score.Instance.gameObject);
    }
}
