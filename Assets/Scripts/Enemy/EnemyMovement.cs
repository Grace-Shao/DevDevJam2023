using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private SpaceBounds bounds;
    private Vector3 roamingPoint;
    public float moveSpeed = 1f;
    private float timer = 3;
    private bool isMoving;
    // Start is called before the first frame update
    void Start() {
        bounds = transform.parent.GetComponentInChildren<GenericSpace>().GetBounds();
        ChangeRoamingPoint();
    }

    void Update() {
        transform.position = Vector3.MoveTowards(transform.position, roamingPoint, moveSpeed / 100f);
        if (timer <= 0) { ChangeRoamingPoint(); timer = Random.Range(1f, 3f); } 
        else timer -= Time.deltaTime;
    }

    private void ChangeRoamingPoint() {
        roamingPoint = new Vector3(Random.Range(bounds.leftX, bounds.rightX), Random.Range(bounds.bottomY, bounds.topY), transform.position.z);
    }
}
