using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endHack : MonoBehaviour
{

    public bool end = false;
    private bool hasBeenTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (end && !hasBeenTrigger)
        {
            End();
            hasBeenTrigger = true;
        }
    }

    public void End()
    {
        GameManager.instance.End();
    }
}
