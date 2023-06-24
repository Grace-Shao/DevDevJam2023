using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] private FoodData m_foodData;
    [SerializeField] private CustomerData m_customerData;

    public CustomerData CustomerData
    {
        get { return m_customerData; }
        set { m_customerData = value; }
    }

    public FoodData FoodData
    {
        get { return m_foodData; }
        set { m_foodData = value; }
    }
}
