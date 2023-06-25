using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private RequireFood[] requireFoods;
    private Dictionary<FoodData, int> foodRequirements = new Dictionary<FoodData, int>();

    private void Start()
    {
        foreach (var requiredFood in requireFoods) 
        {
            foodRequirements.Add(requiredFood.food, requiredFood.requiredAmount);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var foodProj = collision.GetComponent<FoodProjectile>();
        if (foodProj != null)
        {
            StartCoroutine(ReactToFood(foodProj));
        }
    }
    private IEnumerator ReactToFood(FoodProjectile foodProj)
    {
        if (foodRequirements.ContainsKey(foodProj.foodData))
        {
            // Increment points to pointsystem based on m_foodData.value
            Score.Instance.SCORE += foodProj.foodData.value;
            foodRequirements[foodProj.foodData]--;

            if (foodRequirements[foodProj.foodData] <= 0)
            {
                foodRequirements.Remove(foodProj.foodData);
            }
        }
        else
        {
            // Decrement points to pointsystem based on m_foodData.value
            Score.Instance.SCORE -= foodProj.foodData.value;
        }

        if (foodRequirements.Count <= 0)
        {
            Destroy(gameObject.GetComponent<Collider2D>());
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
        }   
    }
}

[Serializable]
public struct RequireFood
{
    [SerializeField] public FoodData food;
    [SerializeField] public int requiredAmount;
} 