﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scanner : MonoBehaviour
{
    private GameObject currentItem;
    private float currentScanningTime;
    public float scanTime = 1f;

    public Text displayName;
    public Text displayPrice;

    static public Scanner instance;

    public Animation overlay;

    public bool overlayPlayed = false;

    public AudioClip[] scanner;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        currentScanningTime = 0f;   
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
                displayName.text = collider.GetComponent<Item>().data.name;
                displayName.color = Color.green;

                displayPrice.text = "" + collider.GetComponent<Item>().data.price + "€";
                displayPrice.color = Color.green;

                if (!overlayPlayed)
                {
                    overlayPlayed = true;
                    overlay.Play();
                    AudioSource.PlayClipAtPoint(scanner[Random.Range(0, scanner.Length - 1)], new Vector3(0, 0, -10));
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        currentScanningTime = 0f;
        currentItem = null;

        displayName.text = "-";
        displayName.color = Color.red;
        displayPrice.text = "-";
        displayPrice.color = Color.red;
        overlayPlayed = false;
    }
}
