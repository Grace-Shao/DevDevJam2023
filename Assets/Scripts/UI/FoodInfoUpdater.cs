using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FoodInfoUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] m_textFields;
    [SerializeField] private Image[] m_imageFields;

    private void Update()
    {
        for (int i = 0; i < m_textFields.Length; i++)
        {
            m_textFields[i].text = FoodStorage.Instance.FoodList[i].ammo.ToString();
            m_imageFields[i].fillAmount = FoodStorage.Instance.FoodList[i].currCooldown / FoodStorage.Instance.FoodList[i].cooldown;
        }
    }
}
