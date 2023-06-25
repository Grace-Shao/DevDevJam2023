using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodStorage : MonoBehaviour {

    [SerializeField] private GameObject genericFood;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private List<FoodData> foodList;
    private FoodData activeFood;

    // Start is called before the first frame update
    void Start() {
        GetComponent<WeaponController>().OnShootFood += FoodStorage_OnShootFood;
        GetComponent<FoodSelectorLogic>().OnActiveFoodChange += FoodStorage_OnActiveFoodChange;
        activeFood = foodList[0];
    }

    private void FoodStorage_OnActiveFoodChange(FoodData food) {
        SetActiveFood(food);
    }

    private void FoodStorage_OnShootFood(Vector3 spawnPosition, Vector3 rotation) {
        ShootFood(spawnPosition, rotation);
    }

    private FoodData FindFood(string name) {
        foreach (FoodData food in foodList) {
            if (food.name.ToLower() == name.ToLower()) return food;
        } Debug.LogWarning("No food named " + name + " was found.");
        return null;
    }

    private void ShootFood(Vector3 spawnPosition, Vector3 rotation) {
        var projectile = Instantiate(genericFood, spawnPosition, Quaternion.Euler(rotation));
        var projectileScript = projectile.AddComponent<FoodProjectile>();
        projectileScript.Launch(projectileSpeed, activeFood);
    }

    public List<FoodData> GetFoodList() {
        return foodList;
    }

    public void SetActiveFood(FoodData activeFood) {
        this.activeFood = activeFood;
    }

    // Update is called once per frame
    void Update() {
    }
}

[System.Serializable]
public class Food {
    public string name;
    public Sprite sprite;
    public bool unlocked;
}
