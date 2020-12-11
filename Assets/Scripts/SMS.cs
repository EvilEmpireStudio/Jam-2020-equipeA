using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SMS", order = 4)]
public class SMS : ScriptableObject
{
    public string line;
    public string[] answers;
    public int EventIndex;
    public bool lastSMS = false;
}
