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
    private enum RecoilState {
        Idle,
        Backwards,
        Forward,
        Reset
    } private RecoilState recoilState;
    private int activeRecoilUnits = 0;
    private int maxRecoilUnits = 20;
    private int minRecoilUnits = -5;
    private float recoilUnitValue = 0.05f;

    // Update is called once per frame
    void Update() {
        if (PauseScript.Instance.IsPaused) return;

        AlignToCursor(Input.mousePosition, Camera.main);

        if (Input.GetMouseButtonDown(0)) {
            if (FoodStorage.Instance.GetActiveFood().ammo > 0) recoilState = RecoilState.Backwards;
            OnShootFood?.Invoke(gunPoint.position, transform.eulerAngles);
        }

        if (Input.GetMouseButtonDown(1))
        {
            OnReloadFood?.Invoke();
        }

        // World record for lazy implementation, lets go anti-patteeeeern!
        switch (recoilState) {
            case RecoilState.Backwards:
                if (activeRecoilUnits < maxRecoilUnits) {
                    transform.localPosition -= transform.right * recoilUnitValue;
                    activeRecoilUnits += 4;
                } else recoilState = RecoilState.Forward;
                break;
            case RecoilState.Forward:
                if (activeRecoilUnits > minRecoilUnits) {
                    transform.localPosition += transform.right * recoilUnitValue / 2f;
                    activeRecoilUnits -= 2;
                } else recoilState = RecoilState.Reset;
                break;
            case RecoilState.Reset:
                if (activeRecoilUnits < 0) {
                    transform.localPosition -= transform.right * recoilUnitValue / 4f;
                    activeRecoilUnits++;
                } else recoilState = RecoilState.Idle;
                break;
        }
    }

    private void AlignToCursor(Vector2 mousePosition, Camera camera) {
        var cursorVector = camera.ScreenToWorldPoint(mousePosition) - transform.position;
        cursorVector.Normalize();
        var rotation = Vector2.Angle(cursorVector, Vector2.right);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rotation);
    }
}
