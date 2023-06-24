using UnityEngine;
using UnityEngine.UI;

public class CustomerHud : MonoBehaviour
{
    [SerializeField] private Customer attachedCustomer;
    [SerializeField] private Image foodImage;

    // Just so that the image stays the same no matter what
    // This can be updated on impact of food given with a check mark or smthn
    private void Update()
    {
        foodImage.sprite = attachedCustomer.FoodData.foodSprite;
    }
}
