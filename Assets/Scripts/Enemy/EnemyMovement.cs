using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] private float moveSpeed = 1f;
    private SpaceBounds bounds;
    private Vector3 roamingPoint;

    private Transform customerSprite;
    private float timer = 3;
    private float yVariation = 0.075f;
    private bool isMoving;

    // Start is called before the first frame update
    void Start() {
        customerSprite = GetComponentInChildren<SpriteRenderer>().transform;
        bounds = transform.parent.GetComponentInChildren<GenericSpace>().GetBounds();
        ChangeRoamingPoint();
    }

    void Update() {
        transform.position = Vector3.MoveTowards(transform.position, roamingPoint, moveSpeed / 100f);
        if (timer <= 0) { ChangeRoamingPoint(); timer = Random.Range(1f, 3f); } 
        else timer -= Time.deltaTime;
        var yVar = Mathf.Sin(Time.time * 20f) * yVariation;
        customerSprite.position = new Vector3(transform.position.x, transform.position.y + yVar, transform.position.z);
    }

    private void ChangeRoamingPoint() {
        roamingPoint = new Vector3(Random.Range(bounds.leftX, bounds.rightX), Random.Range(bounds.bottomY, bounds.topY), transform.position.z);
    }
}
