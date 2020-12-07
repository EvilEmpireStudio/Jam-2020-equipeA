using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Item", order = 1)]
public class ItemData : ScriptableObject
{
    public enum Type
    {
        Fruit,
        Basic,
        NoBarCode
    }

    public float price;
    public Type type;
    public Sprite sprite;
}
