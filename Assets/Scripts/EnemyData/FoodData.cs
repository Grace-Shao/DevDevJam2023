using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="FoodData")]
public class FoodData : ScriptableObject
{
    public string name;
    public int value;
    public Sprite foodSprite;
}
