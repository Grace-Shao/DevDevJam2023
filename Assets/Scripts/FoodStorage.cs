using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodStorage : MonoBehaviour {

    [SerializeField] private GameObject genericFood;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private List<Food> foodList;
    private Food activeFood;

    // Start is called before the first frame update
    void Start() {
        GetComponent<WeaponController>().OnShootFood += FoodStorage_OnShootFood;
        GetComponent<FoodSelector>().OnActiveFoodChange += FoodStorage_OnActiveFoodChange;
        activeFood = foodList[0];
    }

    private void FoodStorage_OnActiveFoodChange(Food food) {
        SetActiveFood(food);
    }

    private void FoodStorage_OnShootFood(Vector3 spawnPosition, Vector3 rotation) {
        ShootFood(spawnPosition, rotation);
    }

    private Food FindFood(string name) {
        foreach (Food food in foodList) {
            if (food.name.ToLower() == name.ToLower()) return food;
        } Debug.LogWarning("No food named " + name + " was found.");
        return null;
    }

    private void ShootFood(Vector3 spawnPosition, Vector3 rotation) {
        var projectile = Instantiate(genericFood, spawnPosition, Quaternion.Euler(rotation));
        var sprRenderer = projectile.GetComponentInChildren<SpriteRenderer>();
        sprRenderer.transform.Rotate(new Vector3(0, 0, -90));
        sprRenderer.sprite = activeFood.sprite;
        var projectileScript = projectile.AddComponent<FoodProjectile>();
        projectileScript.Launch(projectileSpeed);
    }

    public List<Food> GetFoodList() {
        return foodList;
    }

    public void SetActiveFood(Food activeFood) {
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
