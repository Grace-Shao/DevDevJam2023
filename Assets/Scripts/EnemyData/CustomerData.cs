using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomerData")]
public class CustomerData : ScriptableObject
{
    public string name;
    public float timeTillAngry;
    public Sprite customerSprite;
    public bool isMale; 
}
