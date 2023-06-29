using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuzzKitchen : MonoBehaviour {

    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform[] cookingPlates;
    [SerializeField] private CookingFood[] cookingFoods;
    private Queue<CookingFood> cookingQueue;
    private bool waitingForFood;
    
    private struct ActivePlate {
        public Transform plate;
        public int index;

        public ActivePlate(Transform plate, int index) {
            this.plate = plate;
            this.index = index;
        }
    } private ActivePlate activePlate;

    // Start is called before the first frame update
    void Start() {
        activePlate = new ActivePlate(cookingPlates[0], 0);
        cookingQueue = new Queue<CookingFood>();
        FoodStorage.Instance.OnFoodReload += BuzzKitchen_OnFoodReload;
        transform.eulerAngles = new Vector3(0, 0, 90);
    }

    private void BuzzKitchen_OnFoodReload(FoodData foodData) {
        foreach (CookingFood cookingFood in cookingFoods) {
            if (cookingFood.foodData == foodData) {
                cookingQueue.Enqueue(cookingFood);
                return;
            }
        } Debug.LogWarning("Buzz doesn't know how to cook a " + foodData.name);
    }

    // Update is called once per frame
    void Update() {
        if (cookingQueue.Count > 0) {
            if (AlignToPlate(activePlate.plate) && !waitingForFood) {
                SpawnFood(cookingQueue.Dequeue(), activePlate.plate);
                waitingForFood = true;
            }
        } else AlignToPlate(activePlate.plate);

        transform.eulerAngles += new Vector3(0, 0, 1) * Mathf.Sin(Time.time * 10f) * 3f;
        transform.eulerAngles += new Vector3(0, 1, 0) * Mathf.Sin(Time.time * 15f) * 1.1f;
        transform.eulerAngles += new Vector3(1, 0, 0) * Mathf.Sin(Time.time * 7.5f) * 1.05f;
    }

    private bool AlignToPlate(Transform plate) {
        var plateVector = plate.position - transform.position;
        plateVector.Normalize();
        var rotation = Vector2.Angle(plateVector, Vector2.right);
        transform.eulerAngles = Vector3.MoveTowards(transform.eulerAngles, new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rotation), rotationSpeed);
        return transform.eulerAngles == new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rotation);
    }

    private void SwitchActivePlate() {
        if (activePlate.index < cookingPlates.Length - 1) activePlate = new ActivePlate(cookingPlates[activePlate.index + 1], activePlate.index + 1);
        else activePlate = new ActivePlate(cookingPlates[0], 0);
    }

    private void SpawnFood(CookingFood cookingFood, Transform cookingPlate) {
        var miniFood = new GameObject("Smol " + cookingFood.foodData.name);
        var sprRenderer = miniFood.AddComponent<SpriteRenderer>();
        sprRenderer.sprite = cookingFood.sprite;
        sprRenderer.sortingOrder = 3;
        miniFood.AddComponent<MiniFood>().OnFoodCooked += BuzzKitchen_OnFoodCooked;
        miniFood.transform.position = cookingPlate.position;
        miniFood.transform.parent = cookingPlate;
    }

    private void BuzzKitchen_OnFoodCooked() {
        SwitchActivePlate();
        waitingForFood = false;
    }
}

[System.Serializable]
public class CookingFood {
    public FoodData foodData;
    public Sprite sprite;
}
