using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    public enum State
    {
        Arriving,
        Dialogue,
        Leaving,
        Unspawn
    }
    public State state;

    public ClientData data;

    public GameObject clientSpawner;
    public GameObject clientDialoguePos;
    public GameObject clientUnspawn;

    public int itemsToBuyNb = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (data != null)
        {
            applyData();
        }
    }

    private void Update()
    {
        if (state == State.Dialogue && itemsToBuyNb <= 0)
        {
            Animator animator = gameObject.GetComponent<Animator>();
            animator.SetBool("CanLeave",true);
        } else if (state == State.Unspawn)
        {
            //warn the game manager
            GameObject.Destroy(this);
        }
    }

    public void init()
    {
        clientSpawner = GameManager.instance.clientSpawner;
        clientDialoguePos = GameManager.instance.clientDialoguePos;
        clientUnspawn = GameManager.instance.clientUnspawn;

        applyData();
    }

    public void applyData()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = data.sprite;

        gameObject.name = data.sprite.name;
    }

    public void spawnClientItems()
    {
        foreach (ItemData itemData in data.items)
        {
            Vector3 spawnPos = generateItemSpawnPos();
            GameObject itemGO = GameObject.Instantiate(GameManager.instance.itemPrefab, spawnPos, Quaternion.identity);
            Item item = itemGO.GetComponent<Item>();
            item.data = itemData;
            item.client = this;
            item.applyData();
        }
        itemsToBuyNb = data.items.Length;
    }

    public void ItemBought()
    {
        itemsToBuyNb--;
    }

    private Vector3 generateItemSpawnPos()
    {
        RectTransform rect = GameManager.instance.itemSpawner.GetComponent<RectTransform>();
        return new Vector3(Random.Range(rect.transform.position.x, rect.transform.position.x + rect.sizeDelta.x), Random.Range(rect.transform.position.y, rect.transform.position.y + rect.sizeDelta.y), 0);
    }
}
