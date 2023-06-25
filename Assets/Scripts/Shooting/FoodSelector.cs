using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSelector : MonoBehaviour {

    public event Action<FoodData> OnActiveFoodChange;

    private FoodStorage foodScript;
    private List<FoodData> foodList;

    private int currIdx;

    void Start() {
        foodScript = GetComponent<FoodStorage>();
        foodList = foodScript.GetFoodList();
        //foreach (FoodData food in foodList) {
        //    if (!food.unlocked) foodList.Remove(food);
        //}
    }

    private void Update() {
        float scrollWheelVal = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheelVal < 0)
        {
            currIdx = (currIdx + 1) % foodList.Count;
            OnActiveFoodChange(foodList[currIdx]);
        }
        else if (scrollWheelVal > 0)
        {
            currIdx = (currIdx - 1) % foodList.Count;
            OnActiveFoodChange(foodList[currIdx]);
        }

        for (int i = 1; i <= 9; i++) 
        {
            if (i <= foodList.Count && Input.GetKey((i).ToString())) 
            {
                currIdx = i - 1;
                OnActiveFoodChange?.Invoke(foodList[i - 1]);
            }
        }
    }
}
