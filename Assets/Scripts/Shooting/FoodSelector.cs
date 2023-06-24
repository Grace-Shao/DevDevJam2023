using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSelector : MonoBehaviour {

    public event Action<Food> OnActiveFoodChange;

    private FoodStorage foodScript;
    private List<Food> foodList;

    void Start() {
        foodScript = GetComponent<FoodStorage>();
        foodList = foodScript.GetFoodList();
        foreach (Food food in foodList) {
            if (!food.unlocked) foodList.Remove(food);
        }
    }

    private void Update() {
        for (int i = 1; i <= 9; i++) {
            if (i <= foodList.Count && Input.GetKey((i).ToString())) OnActiveFoodChange?.Invoke(foodList[i - 1]);
        }
    }
}
