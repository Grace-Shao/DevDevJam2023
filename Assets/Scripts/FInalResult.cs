using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FInalResult : MonoBehaviour
{
    private void Start()
    {
        GetComponent<TextMeshProUGUI>().text = Score.Instance.HighScore.ToString();
        Destroy(Score.Instance.gameObject);
    }
}
