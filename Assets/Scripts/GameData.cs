using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameData", order = 5)]
public class GameData : ScriptableObject
{
    public ClientData[] clientDataList;
    public SMS[] smsDataList;
    public SMS lastSMS;
}
