using UnityEngine;
using UnityEngine.UI;

public class UISelection : MonoBehaviour
{
    [SerializeField] private Image[] foodList;
    private void Update()
    {
        for (int i = 0; i < foodList.Length; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                MarkFoodItem(i);
                break;
            }
        }
    }

    private void MarkFoodItem(int idx)
    {
        foreach (var food in foodList) food.enabled = false;

        foodList[idx].enabled = true;
    }
}
