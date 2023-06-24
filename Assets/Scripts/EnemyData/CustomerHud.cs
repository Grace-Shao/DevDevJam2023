using UnityEngine;
using UnityEngine.UI;

public class CustomerHud : MonoBehaviour
{
    [SerializeField] private Customer attachedCustomer;
    [SerializeField] private Image foodImage;
    [SerializeField] private Sprite satisfied;
    [SerializeField] private Sprite unsatisfied;

    // Just so that the image stays the same no matter what
    // This can be updated on impact of food given with a check mark or smthn
    private void Start()
    {
        foodImage.sprite = attachedCustomer.FoodChoice.foodSprite;
    }

    public void Satisfied()
    {
        foodImage.sprite = satisfied;
    }

    public void Unsatisfied()
    {
        foodImage.sprite = unsatisfied;
    }
}
