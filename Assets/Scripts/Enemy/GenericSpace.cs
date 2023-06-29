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

    public SpaceDimensions GetDimensions() {
        var dimensions = new SpaceDimensions();
        dimensions.origin = transform.position;
        dimensions.width = width;
        dimensions.height = height;
        return dimensions;
    }
}

public class SpaceDimensions {
    public Vector2 origin;
    public float width;
    public float height;
}
