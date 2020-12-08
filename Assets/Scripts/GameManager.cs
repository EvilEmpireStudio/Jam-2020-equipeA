using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int ITEM_LAYER = 8;
    public static GameManager instance;

    public GameObject itemSpawner;
    public GameObject itemPrefab;

    //Client
    public GameObject clientSpawner;
    public GameObject clientDialoguePos;
    public GameObject clientUnspawn;
    public GameObject clientPrefab;
    public ClientData[] clientDataList;
    public Client currentClient;

    private int currentClientIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        /*if (client != null)
        {
            SpawnClientItems();
        }*/
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentClient == null) //time to spawn a client
        {
            Vector3 spawnPos = clientSpawner.transform.position;
            GameObject clientGO = GameObject.Instantiate(clientPrefab, spawnPos, Quaternion.identity);
            currentClient = clientGO.GetComponent<Client>();
            currentClient.data = clientDataList[currentClientIndex];
            currentClient.init();
            currentClient.spawnClientItems();
        }
    }
}
