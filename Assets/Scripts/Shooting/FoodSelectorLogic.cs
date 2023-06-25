using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSelectorLogic : MonoBehaviour {

    public event Action<FoodData> OnActiveFoodChange;

    private FoodStorage foodScript;
    private List<FoodData> foodList;

    void Start() {
        foodScript = GetComponent<FoodStorage>();
        foodList = foodScript.GetFoodList();
        //foreach (FoodData food in foodList) {
        //    if (!food.unlocked) foodList.Remove(food);
        //}
    }

    private void Update() {

        
    }
}
