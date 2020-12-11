using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum State
    {
        Scanned,
        ReadyToScan,
        ReadyToWeight
    }
    public State state;

    public ItemData data;
    public Client client;

    void Start()
    {
        /*if (data != null)
        {
            applyData();
        }*/
    }

    public Client GetClient()
    {
        return client;
    }

    public void applyData()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = data.sprite;

        switch (data.type)
        {
            case ItemData.Type.Fruit:
                state = State.ReadyToWeight;
                spriteRenderer.color = Color.yellow;
                break;
            default:
                state = State.ReadyToScan;
                spriteRenderer.color = Color.white;
                break;
        }

        gameObject.AddComponent<BoxCollider2D>();

        gameObject.name = data.sprite.name;
    }

    public void setState(State st)
    {
        state = st;

        switch (state)
        {
            case State.Scanned:
                transform.GetChild(0).gameObject.SetActive(true);
                break;
            default:
                //GetComponent<SpriteRenderer>().color = Color.white;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
