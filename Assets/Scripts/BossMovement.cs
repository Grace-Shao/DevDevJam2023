using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float sideToSideSpeed = 2f;
    public float rushSpeed = 5f;
    public float minRushInterval = 2f;
    public float maxRushInterval = 5f;
    public float sideMovementMultiplier = 4f;
    public GameObject rushPointObject;

    private bool isRushing = false;
    private bool isMovingSideToSide = true;
    private Vector3 startingPosition;
    private Vector3 rushPoint;
    private float rushInterval;
    private float waitTime;

    private void Start()
    {
        startingPosition = transform.position;

        if (rushPointObject != null)
        {
            rushPoint = rushPointObject.transform.position;
        }
        else
        {
            Debug.LogError("Rush Point Object not assigned to ObjectMovement script!");
        }

        SetNewRushInterval();
    }

    private void Update()
    {
        if (isRushing)
        {
            // Move towards the rush point with rush speed
            transform.position = Vector3.MoveTowards(transform.position, rushPoint, rushSpeed * Time.deltaTime);

            // Check if we have reached the rush point
            if (transform.position == rushPoint)
            {
                isRushing = false;
                Invoke("SetNewWaitTime", waitTime);
            }
        }
        else if (isMovingSideToSide)
        {
            // Move side to side using sine wave
            transform.position = new Vector3(Mathf.Sin(Time.time * sideToSideSpeed) * sideMovementMultiplier, transform.position.y, transform.position.z);
        }
        else
        {
            // Move back to starting position
            transform.position = Vector3.MoveTowards(transform.position, startingPosition, rushSpeed * Time.deltaTime);

            // Check if we have reached the starting position
            if (transform.position == startingPosition)
            {
                isMovingSideToSide = true;
                Invoke("SetNewRushInterval", rushInterval);
            }
        }
    }

    private void SetNewRushInterval()
    {
        isMovingSideToSide = false;
        isRushing = true;
        SetNewWaitTime();
    }

    private void SetNewWaitTime()
    {
        rushInterval = Random.Range(minRushInterval, maxRushInterval);
        waitTime = Random.Range(minRushInterval, maxRushInterval);
    }
}
