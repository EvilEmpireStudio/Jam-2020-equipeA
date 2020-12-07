using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private GameObject currentItem;
    private float currentScanningTime;
    public float scanTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        currentScanningTime = 0f;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == GameManager.ITEM_LAYER)
        {
            Item item = collider.gameObject.GetComponent<Item>();
            if (currentItem == null && currentItem != collider.gameObject && item.state == Item.State.ReadyToScan)
            {
                currentItem = collider.gameObject;
            }
            else
            {
                currentItem = null;
            }
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.layer == GameManager.ITEM_LAYER && currentItem != null)
        {
            //Debug.Log("item : " + collider.gameObject.name);
            currentScanningTime += Time.fixedDeltaTime;
            if (currentScanningTime > scanTime)
            {
                collider.GetComponent<Item>().setState(Item.State.Scanned);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        currentScanningTime = 0f;
    }
}
