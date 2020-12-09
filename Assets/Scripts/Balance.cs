﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Balance : MonoBehaviour
{
    private GameObject currentItem;
    private float currentScanningTime;
    public float scanTime = 0.5f;

    public Text displayText;

    // Start is called before the first frame update
    void Start()
    {
        currentScanningTime = 0f;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == GameManager.ITEM_LAYER)
        {
            Item item = collider.gameObject.GetComponent<Item>();
            if (currentItem == null && currentItem != collider.gameObject && item.state == Item.State.ReadyToWeight)
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
                collider.GetComponent<Item>().setState(Item.State.ReadyToScan);
                displayText.text = (collider.GetComponent<Item>().data.price + Random.Range(0,5)) + "g";
                displayText.color = Color.green;
                 
            }
            else
            {
                displayText.text = "" + Random.Range(0, 100) + "g";
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        displayText.text = "0g";
        displayText.color = Color.red;
        currentScanningTime = 0f;
        currentItem = null;
    }
}
