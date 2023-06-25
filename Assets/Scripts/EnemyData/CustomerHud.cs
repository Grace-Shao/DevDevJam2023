using UnityEngine;
using UnityEngine.UI;

public class CustomerHud : MonoBehaviour
{
    [SerializeField] private Customer attachedCustomer;
    [SerializeField] private Image foodImage;
    [SerializeField] private Sprite satisfied;
    [SerializeField] private Sprite unsatisfied;

    private Vector3 imageScale;
    private float scaleSpeed = 0.25f;
    private Sprite targetSprite;

    private enum State {
        Idle,
        Downscale,
        Overscale,
        Normalize
    } private State state;

    // Just so that the image stays the same no matter what
    // This can be updated on impact of food given with a check mark or smthn
    private void Start()
    {
        foodImage.sprite = attachedCustomer.FoodChoice.foodSprite;
        imageScale = foodImage.transform.localScale;
    }

    private void Update() {
        switch (state) {
            case State.Downscale:
                foodImage.transform.localScale = Vector3.MoveTowards(foodImage.transform.localScale, Vector3.zero, scaleSpeed);
                if (foodImage.transform.localScale == Vector3.zero) {
                    foodImage.sprite = targetSprite;
                    state = State.Overscale;
                } break;
            case State.Overscale:
                foodImage.transform.localScale = Vector3.MoveTowards(foodImage.transform.localScale, imageScale + imageScale * 0.05f, scaleSpeed);
                if (foodImage.transform.localScale == imageScale + imageScale * 0.05f) {
                    state = State.Normalize;
                } break;
            case State.Normalize:
                foodImage.transform.localScale = Vector3.MoveTowards(foodImage.transform.localScale, imageScale, scaleSpeed);
                if (foodImage.transform.localScale == imageScale) state = State.Idle;
                break;
        }
    }

    public void Satisfied()
    {
        targetSprite = satisfied;
        state = State.Downscale;
    }

    public void Unsatisfied()
    {
        targetSprite = unsatisfied;
        state = State.Downscale;
    }
}
