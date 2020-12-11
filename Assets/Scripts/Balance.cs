using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Balance : MonoBehaviour
{
    private GameObject currentItem;
    private float currentScanningTime;
    public float scanTime = 0.5f;

    public Text displayText;

    public Animation overlayBalance;
    public Animation overlayScanner;
    public bool overlayPlayed = false;

    public AudioClip[] balance;

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
                AudioSource.PlayClipAtPoint(balance[Random.Range(0, balance.Length - 1)], new Vector3(0, 0, -10));
                overlayPlayed = false;
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
                displayText.text = (collider.GetComponent<Item>().data.price + Random.Range(0,5)) + "g";
                displayText.color = Color.green;

                Scanner.instance.displayName.text = collider.GetComponent<Item>().data.name;
                Scanner.instance.displayName.color = Color.green;

                Scanner.instance.displayPrice.text = "" + collider.GetComponent<Item>().data.price + "€";
                Scanner.instance.displayPrice.color = Color.green;


                if (!overlayPlayed)
                {
                    overlayPlayed = true;
                    overlayBalance.Play();
                    overlayScanner.Play();
                }
            }
            else
            {
                displayText.text = "" + Random.Range(0, 100) + "g";
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        Scanner.instance.displayName.text = "-";
        Scanner.instance.displayName.color = Color.red;

        Scanner.instance.displayPrice.text = "-";
        Scanner.instance.displayPrice.color = Color.red;
        displayText.text = "0g";
        displayText.color = Color.red;
        currentScanningTime = 0f;
        currentItem = null;
        overlayPlayed = false;
    }
}
