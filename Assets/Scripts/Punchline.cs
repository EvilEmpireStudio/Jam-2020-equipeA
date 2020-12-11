using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Punchline", order = 3)]
public class Punchline : ScriptableObject
{
    public string line;
    public string[] answers;
    public bool[] censored;
    public Reaction[] answerReaction;
    public float delay = 5f;


    public enum Reaction
    {
        None,
        Choc,
        Happy
    }
}
