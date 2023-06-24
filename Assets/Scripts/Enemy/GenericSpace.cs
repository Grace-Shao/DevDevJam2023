using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSpace : MonoBehaviour {

    [SerializeField] private float width;
    [SerializeField] private float height;

    // Update is called once per frame
    void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + width, transform.position.y, transform.position.z));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y + height, transform.position.z));
        Gizmos.DrawLine(new Vector3(transform.position.x + width, transform.position.y, transform.position.z),
                        new Vector3(transform.position.x + width, transform.position.y + height, transform.position.z));
        Gizmos.DrawLine(new Vector3(transform.position.x, transform.position.y + height, transform.position.z),
                        new Vector3(transform.position.x + width, transform.position.y + height, transform.position.z));
    }

    public SpaceBounds GetBounds() {
        var bounds = new SpaceBounds();
        bounds.leftX = transform.position.x;
        bounds.rightX = transform.position.x + width;
        bounds.bottomY = transform.position.y;
        bounds.topY = transform.position.y + height;
        return bounds;
    }
}

public class SpaceBounds {
    public float leftX;
    public float rightX;
    public float bottomY;
    public float topY;
}
