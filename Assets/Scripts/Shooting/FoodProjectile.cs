using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodProjectile : MonoBehaviour {

    private FoodData foodData;
    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    public void Launch(float speed, FoodData foodData) {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.AddRelativeForce(Vector2.right * speed);
        var sprRenderer = GetComponentInChildren<SpriteRenderer>();
        sprRenderer.transform.Rotate(new Vector3(0, 0, -90));
        sprRenderer.sprite = foodData.foodSprite;
        this.foodData = foodData;
    }

    // Update is called once per frame
    void Update() {

    }
}
