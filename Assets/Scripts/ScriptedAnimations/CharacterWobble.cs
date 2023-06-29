using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWobble : MonoBehaviour {

    [SerializeField] Vector3 wobbleSpeed;
    [SerializeField] Vector3 wobbleStrength;

    void Update() {
        transform.eulerAngles += new Vector3(0, 0, 1) * Mathf.Sin(Time.time * wobbleSpeed.z) * wobbleStrength.z;
        transform.eulerAngles += new Vector3(0, 1, 0) * Mathf.Sin(Time.time * wobbleSpeed.y) * wobbleStrength.y;
        transform.eulerAngles += new Vector3(1, 0, 0) * Mathf.Sin(Time.time * wobbleSpeed.x) * wobbleStrength.x;
    }
}
