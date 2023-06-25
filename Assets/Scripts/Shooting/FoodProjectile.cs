using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodProjectile : MonoBehaviour {

    public FoodData foodData;
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
        if (rigidBody.position.x > 24.1 || rigidBody.position.y > 12.1) {
            Score.Instance.SCORE -= 2;
            Destroy(gameObject);
        }
    }
}
