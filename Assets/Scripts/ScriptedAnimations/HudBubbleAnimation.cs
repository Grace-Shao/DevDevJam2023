using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudBubbleAnimation : MonoBehaviour {

    private float timer;
    private float yAnchor;
    private float targetXScale = 1;
    private float targetYScale = 1;
    private float scaleSpeed = 0.5f;

    // Update is called once per frame
    void Update() {
        yAnchor = Mathf.Sin(Time.time * 10f) * 0.01f;
        transform.position = new Vector2(transform.position.x, transform.position.y + yAnchor);
        timer += Time.deltaTime;
        transform.eulerAngles = new Vector3(0, 0, Mathf.Sin(timer * 10f) * 2.5f);
        transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(targetXScale, targetYScale, 
                                                   transform.localScale.z), scaleSpeed * Time.deltaTime);
        if (transform.localScale.x == targetXScale) targetXScale = Random.Range(0.925f, 1.075f);
        if (transform.localScale.y == targetYScale) targetYScale = Random.Range(0.925f, 1.075f);
    }
}
