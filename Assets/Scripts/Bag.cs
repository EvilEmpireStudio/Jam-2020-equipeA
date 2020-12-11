using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    private GameObject currentItem;
    public float scanTime = 1f;
    public Animation bagAnim;

    private void Update()
    {
        if (currentItem != null)
        {
            // item tell to the client that a item has been purchased
            Client client = currentItem.GetComponent<Item>().GetClient();
            client.ItemBought();
            GameObject.Destroy(currentItem);
            currentItem = null;
            bagAnim.Play();
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.layer == GameManager.ITEM_LAYER)
        {
            Item item = collider.gameObject.GetComponent<Item>();
            if (currentItem == null && currentItem != collider.gameObject && item.state == Item.State.Scanned)
            {
                currentItem = collider.gameObject;
            }
            else
            {
                currentItem = null;
            }
        }
    }
}
