using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Client", order = 1)]
public class ClientData : ScriptableObject
{
    public Sprite sprite;

    public ItemData[] items;
    public int[] test;
    public Punchline[] lines;
    public string byeLine;
    public Sprite swap;
}
