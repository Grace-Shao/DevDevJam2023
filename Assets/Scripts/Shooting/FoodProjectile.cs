using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodProjectile : MonoBehaviour {

    public FoodData foodData;
    private Rigidbody2D rigidBody;
    private Transform target;
    private float rotationSpeed = 10;

    // Start is called before the first frame update
    public void Launch(float speed, FoodData foodData) {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.AddRelativeForce(Vector2.right * speed);
        var sprRenderer = GetComponentInChildren<SpriteRenderer>();
        sprRenderer.transform.Rotate(new Vector3(0, 0, -90));
        sprRenderer.sprite = foodData.foodSprite;
        this.foodData = foodData;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        target = collision.gameObject.transform;
        rigidBody.velocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update() {
        if (rigidBody.position.x > 24.1 || rigidBody.position.y > 12.1) {
            Score.Instance.SCORE -= 2;
            Destroy(gameObject);
        } if (target) {
            transform.position = Vector3.MoveTowards(transform.position, target.position, 0.25f);
            transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(0, 0, transform.localScale.z), 0.025f);
            if (transform.localScale == new Vector3(0, 0, transform.localScale.z)) {
                // Add particle effects;
                Destroy(gameObject);
            }
        } else transform.Rotate(new Vector3(0, 0, rotationSpeed));
    }
}
