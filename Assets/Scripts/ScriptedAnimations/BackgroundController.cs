using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class BackgroundController : MonoBehaviour
{
    public enum BackgroundType {
        Back,
        Front
    } [SerializeField] private BackgroundType backgroundType;

    [SerializeField] private float speedDamp;
    [SerializeField] private int skipClusters = 1;

    private float width;
    private float startAnchor;
    private float resetPointX;
    private Rigidbody2D rb;
    private float globalSpeed = -500f;
    

    void Start() {
        width = GetComponent<SpriteRenderer>().bounds.size.x;
        startAnchor = transform.position.x;
        resetPointX = startAnchor - width * skipClusters;
        rb = GetComponent<Rigidbody2D>();
        rb.AddRelativeForce(new Vector2(globalSpeed * speedDamp, 0));
    }

    private void Update() {
        if (transform.position.x <= resetPointX) transform.position = new Vector2(startAnchor, transform.position.y);
    }
}