using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Customer
{
    private Dictionary<FoodData, int> foodRequirements = new Dictionary<FoodData, int>();
    public Dictionary<FoodData, int> FoodRequirements => foodRequirements;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        var foodProj = collision.GetComponent<FoodProjectile>();
        if (foodProj != null)
        {
            foreach (Collider2D comp in GetComponents<Collider2D>()) Destroy(comp);
            if (!reacted)
            {
                StartCoroutine(ReactToFood(foodProj));
            }
        }
    }
    protected override IEnumerator ReactToFood(FoodProjectile foodProj)
    {
        Debug.Log("Reacted to " + foodProj.name);
        if (foodRequirements.ContainsKey(foodProj.foodData))
        {
            // Increment points to pointsystem based on m_foodData.value
            PlayVoiceline(true);
            Score.Instance.SCORE += foodProj.foodData.value;
            foodRequirements[foodProj.foodData]--;

            if (foodRequirements[foodProj.foodData] <= 0)
            {
                foodRequirements.Remove(foodProj.foodData);
            }
        }
        else
        {
            PlayVoiceline(false);
            Score.Instance.SCORE -= foodProj.foodData.value;
        }

        if (foodRequirements.Count <= 0)
        {
            Debug.Log("DIES");
            reacted = true;
            m_customerHud.Satisfied();
            yield return new WaitForSeconds(0.9f);
            while (transform.localScale != Vector3.zero)
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, 0.15f);
                yield return null;
            }
            var parSystem = GetComponentInChildren<ParticleSystem>();
            var main = parSystem.main;
            main.startColor = GetComponentInChildren<SpriteRenderer>().sprite.texture.GetPixel(50, 50);
            parSystem.Play();
            Destroy(gameObject, main.startLifetime.constant);
        }   
    }
}

[Serializable]
public struct RequireFood
{
    [SerializeField] public FoodData food;
    [SerializeField] public int requiredAmount;
} 