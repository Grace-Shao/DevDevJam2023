using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] private float moveSpeed = 1f;
    private SpaceDimensions dimensions;
    private Vector3 roamingPoint;
    private Customer customerScript;

    private Transform customerSprite;
    private float timer = 3;
    private float yVariation = 0.075f;
    private bool started;

    // Start is called before the first frame update
    void Start() {
        customerScript = GetComponentInChildren<Customer>();
        customerSprite = GetComponentInChildren<SpriteRenderer>().transform;
        dimensions = transform.parent.GetComponentInChildren<GenericSpace>().GetDimensions();
    }

    void Update() {
        if (!started) { ChangeRoamingPoint(); started = true; }
        transform.position = Vector3.MoveTowards(transform.position, roamingPoint, moveSpeed / 100f);
        if (timer <= 0) { ChangeRoamingPoint(); timer = Random.Range(1f, 3f); } 
        else timer -= Time.deltaTime;
        var yVar = Mathf.Sin(Time.time * 20f) * yVariation;
        customerSprite.position = new Vector3(transform.position.x, transform.position.y + yVar, transform.position.z);
    }

    private void ChangeRoamingPoint() {
        var timerPercent = customerScript ? 1 - customerScript.TimeLeftTillAngry / customerScript.CustomerData.timeTillAngry : 1;
        roamingPoint = new Vector3(dimensions.origin.x + Random.Range(dimensions.width * 0.15f * timerPercent,
                                                                      dimensions.width - dimensions.width * 0.15f * timerPercent),
                                   dimensions.origin.y + Random.Range(dimensions.height * 0.85f - dimensions.height * 0.85f,
                                                                      dimensions.height - dimensions.height * 0.65f * timerPercent),
                                                                      transform.position.z);
    }
}
