using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    public event Action<Vector3, Vector3> OnShootFood;
    public event Action OnReloadFood;

    [Range(0,360)]
    [SerializeField] float rotation;
    [SerializeField] Transform gunPoint;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (PauseScript.Instance.IsPaused) return;

        AlignToCursor(Input.mousePosition, Camera.main);

        if (Input.GetMouseButtonDown(0)) {
            OnShootFood?.Invoke(gunPoint.position, transform.eulerAngles);
        }

        if (Input.GetMouseButtonDown(1))
        {
            OnReloadFood?.Invoke();
        }
    }

    private void AlignToCursor(Vector2 mousePosition, Camera camera) {
        var cursorVector = camera.ScreenToWorldPoint(mousePosition) - transform.position;
        cursorVector.Normalize();
        var rotation = Vector2.Angle(cursorVector, Vector2.right);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rotation);
    }
}
