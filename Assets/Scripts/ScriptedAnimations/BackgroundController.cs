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

    private float width;
    private float startAnchor;
    public float speedDamp;

    void Start() {
        width = GetComponent<SpriteRenderer>().bounds.size.x;
        startAnchor = transform.position.x;
    }
}
