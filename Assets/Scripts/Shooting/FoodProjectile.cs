using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodProjectile : MonoBehaviour {

    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    public void Launch(float speed) {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.AddRelativeForce(Vector2.right * speed);
    }

    // Update is called once per frame
    void Update() {

    }
}
