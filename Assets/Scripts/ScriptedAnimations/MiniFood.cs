using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniFood : MonoBehaviour {

    public event Action OnFoodCooked;

    private enum State {
        Growing,
        Normalizing,
        Waiting,
        Traveling
    } private State state = State.Growing;

    private Vector3 targetSize;
    private float growSpeed = 5f;
    private float timer = 0.5f;

    void Start() {
        targetSize = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    void Update() {
        switch (state) {
            case State.Growing:
                if (transform.localScale != targetSize * 5f / 4f) {
                    transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize * 5f / 4f, growSpeed * Time.deltaTime);
                } else state = State.Normalizing;
                break;
            case State.Normalizing:
                if (transform.localScale != targetSize) {
                    transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime * 0.5f);
                } else {
                    OnFoodCooked?.Invoke();
                    //Spawn particles;
                    state = State.Waiting;
                } break;
            case State.Waiting:
                if (timer > 0) timer -= Time.deltaTime;
                else state = State.Traveling;
                break;
            case State.Traveling:
                transform.position += Vector3.up * 2f * Time.deltaTime;
                transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, Time.deltaTime / 1.5f);
                if (transform.localScale == Vector3.zero) Destroy(gameObject);
                break;
        }
    }
}
