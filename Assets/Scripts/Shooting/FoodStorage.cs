using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FoodStorage : MonoBehaviour {

    #region Singleton
    private static FoodStorage m_instance;
    public static FoodStorage Instance
    {
        get 
        { 
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<FoodStorage>();
            }
            return m_instance; 
        }
    }
    #endregion
    public event Action OnShootFood;
    public event Action<FoodData> OnFoodReload;

    [SerializeField] private GameObject genericFood;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private List<Food> foodList;
    private Food activeFood;

    public List<Food> FoodList { get { return foodList; } }

    // Start is called before the first frame update
    void Start() {
        GetComponent<WeaponController>().OnShootFood += FoodStorage_OnShootFood;
        GetComponent<WeaponController>().OnReloadFood += FoodStorage_OnShootReload;
        GetComponent<FoodSelectorLogic>().OnActiveFoodChange += FoodStorage_OnActiveFoodChange;
        
        activeFood = foodList[0];
    }

    private void FoodStorage_OnActiveFoodChange(FoodData foodData) {
        Food activeFood = null;
        foreach (var chosenFood in foodList)
        {
            if (chosenFood.foodData == foodData)
            {
                activeFood = chosenFood;
                break;
            }
        }
        SetActiveFood(activeFood);
    }

    private void FoodStorage_OnShootFood(Vector3 spawnPosition, Vector3 rotation) {
        if (activeFood.ammo <= 0)
        {
            // Play Sound effect of being empty
            return;
        }

        activeFood.ammo--;
        OnShootFood?.Invoke();
        var fxNumber = UnityEngine.Random.Range(1, 9);
        AudioManager.Instance.PlaySFX("Launch" + fxNumber);
        ShootFood(spawnPosition, rotation);
    }

    private void FoodStorage_OnShootReload()
    {
        if (activeFood.currCooldown <= 0)
        {
            activeFood.currCooldown = activeFood.cooldown;
            OnFoodReload?.Invoke(activeFood.foodData);
            StartCoroutine(ReloadFood(activeFood));
        }
    }

    private IEnumerator ReloadFood(Food chosenFood)
    {
        float tick = 0.01f;
        while (chosenFood.currCooldown > 0)
        {
            yield return new WaitForSeconds(tick);
            chosenFood.currCooldown -= tick;
        }
        chosenFood.ammo++;
    }

    private FoodData FindFood(string name) {
        foreach (Food food in foodList) {
            if (food.foodData.name.ToLower() == name.ToLower()) return food.foodData;
        } Debug.LogWarning("No food named " + name + " was found.");
        return null;
    }

    private void ShootFood(Vector3 spawnPosition, Vector3 rotation) {
        var projectile = Instantiate(genericFood, spawnPosition, Quaternion.Euler(rotation));
        var projectileScript = projectile.AddComponent<FoodProjectile>();
        projectileScript.Launch(projectileSpeed, activeFood.foodData);
    }

    public List<FoodData> GetFoodList() {
        List<FoodData> newList = new List<FoodData>();

        foreach (var food in foodList) 
        {
            newList.Add(food.foodData);
        }

        return newList;
    }

    public void SetActiveFood(Food activeFood) {
        this.activeFood = activeFood;
    }

    public Food GetActiveFood() {
        return activeFood;
    }
}

[System.Serializable]
public class Food {
    public FoodData foodData;
    public int ammo;
    public float cooldown;
    public float currCooldown;
}
