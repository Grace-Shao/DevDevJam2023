using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] private FoodData m_foodData;
    [SerializeField] private CustomerData m_customerData;
    [SerializeField] private CustomerHud m_customerHud;

    public static Score _score;
    

    public CustomerData CustomerData
    {
        get { return m_customerData; }
        set { m_customerData = value; }
    }
    public FoodData FoodChoice
    {
        get { return m_foodData; }
        set { m_foodData = value; }
    }

    private void Start() {
        _score = FindObjectOfType<Score>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var foodProj = collision.GetComponent<FoodProjectile>();
        if (foodProj != null) 
        {
            StartCoroutine(ReactToFood(foodProj));
        }
    }
    // TODO: Adjust Player Points using the Point System script based on the value of the food
    private IEnumerator ReactToFood(FoodProjectile foodProj)
    {
        if (foodProj.foodData.name == m_foodData.name) 
        {
            // Increment points to pointsystem based on m_foodData.value
            m_customerHud.Satisfied();
            Destroy(gameObject.GetComponent<Collider2D>());
            _score.scoreNum += 10;
        }
        else
        {
            // Decrement points to pointsystem based on m_foodData.value
            m_customerHud.Unsatisfied();
            Destroy(gameObject.GetComponent<Collider2D>());
            _score.scoreNum -= 10;
        }
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
