using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelection : MonoBehaviour
{
    [SerializeField] private Image[] foodList;
    [SerializeField] private Sprite hasAmmoSprite;
    [SerializeField] private Sprite noAmmoSprite;
    private int currIdx;
    
    private void Start()
    {
        MarkFoodItem(0);
    }
    private void Update()
    {
        // Scroll Wheel Function
        float scrollWheelVal = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheelVal < 0)
        {
            currIdx = (currIdx + 1) % foodList.Length;
            MarkFoodItem(currIdx);
        }
        else if (scrollWheelVal > 0)
        {
            currIdx = (currIdx - 1) < 0 ? foodList.Length - 1 : currIdx - 1;
            MarkFoodItem(currIdx);
        }

        if (FoodStorage.Instance.FoodList[currIdx].ammo <= 0) foodList[currIdx].sprite = noAmmoSprite;
        else foodList[currIdx].sprite = hasAmmoSprite;

        //// Keybind Function
        //for (int i = 0; i < foodList.Length; i++)
        //{
        //    if (Input.GetKeyDown((i + 1).ToString()))
        //    {
        //        MarkFoodItem(i);
        //        break;
        //    }
        //}   
    }

    private void MarkFoodItem(int idx)
    {
        foreach (var food in foodList) food.enabled = false;
        currIdx = idx;
        foodList[idx].enabled = true;
    }
}
