using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BackgroundMultiplierTool : MonoBehaviour {

    [SerializeField] private int numberOfCopies;

    public void PopulateBackgrounds() {
        var sprRenderer = GetComponent<SpriteRenderer>();
        var width = sprRenderer.bounds.size.x;
        for (int i = 1; i <= numberOfCopies; i++) {
            var GO = new GameObject(gameObject.name);
            GO.transform.position = new Vector3(transform.position.x + i * width, transform.position.y, transform.position.z);
            GO.transform.SetParent(transform);
            var sr = GO.AddComponent<SpriteRenderer>();
            sr.sprite = sprRenderer.sprite;
            sr.sortingOrder = sprRenderer.sortingOrder;
        } //GetComponent<BackgroundController>().SetResetPointX(transform.position.x - width);
    }

    public void ClearBackgrounds() {
        foreach (Transform t in transform) DestroyImmediate(t.gameObject);
    }
}
