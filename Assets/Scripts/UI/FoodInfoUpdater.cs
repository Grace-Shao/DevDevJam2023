using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FoodInfoUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] m_textFields;
    [SerializeField] private Image[] m_imageFields;
    private Color cooldown_color;
    private void Start()
    {
        cooldown_color = m_imageFields[0].color;
    }

    private void Update()
    {
        for (int i = 0; i < m_textFields.Length; i++)
        {
            m_textFields[i].text = FoodStorage.Instance.FoodList[i].ammo.ToString();

            if (FoodStorage.Instance.FoodList[i].ammo <= 0 && FoodStorage.Instance.FoodList[i].currCooldown <= 0)
            {
                m_imageFields[i].color = Color.gray;
                m_imageFields[i].fillAmount = 1;
            }       
            else
            {
                m_imageFields[i].color = cooldown_color;
                m_imageFields[i].fillAmount = FoodStorage.Instance.FoodList[i].currCooldown / FoodStorage.Instance.FoodList[i].cooldown;
            }
                
        }
    }
}
