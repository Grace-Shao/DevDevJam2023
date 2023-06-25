using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour {
    [SerializeField] private float rotationSpeed = -2f;
    [SerializeField] private Transform[] wheels;
    private float yCarAnchor;
    private float yWheelAnchor;

    // Start is called before the first frame update
    void Start() {
        yCarAnchor = transform.position.y;
        yWheelAnchor = wheels[0].position.y;
    }

    // Update is called once per frame
    void Update() {
        transform.position = new Vector2(transform.position.x, yCarAnchor + Mathf.Sin(Time.time * 20f) * 0.05f);
        foreach (Transform t in wheels) {
            t.position = new Vector2(t.position.x, yWheelAnchor + Mathf.Sin(Time.time * 20f) * 0.025f);
            t.Rotate(new Vector3(0, 0, rotationSpeed));
        }
    }
}
