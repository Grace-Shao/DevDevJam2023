using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] private FoodData m_foodData;
    [SerializeField] private CustomerData m_customerData;
    [SerializeField] private CustomerHud m_customerHud;
    [SerializeField] private SpriteRenderer m_customerRenderer;

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

    private void Update()
    {
        m_customerRenderer.sprite = m_customerData.customerSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var foodProj = collision.GetComponent<FoodProjectile>();
        if (foodProj != null) 
        {
            Destroy(gameObject.GetComponent<Collider2D>());
            StartCoroutine(ReactToFood(foodProj));
        }
    }
    private IEnumerator ReactToFood(FoodProjectile foodProj)
    {
        if (foodProj.foodData.name == m_foodData.name) 
        {
            // Increment points to pointsystem based on m_foodData.value
            m_customerHud.Satisfied();
            Score.Instance.SCORE += foodProj.foodData.value;
        }
        else
        {
            // Decrement points to pointsystem based on m_foodData.value
            m_customerHud.Unsatisfied();
            Score.Instance.SCORE -= foodProj.foodData.value;
        }
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
