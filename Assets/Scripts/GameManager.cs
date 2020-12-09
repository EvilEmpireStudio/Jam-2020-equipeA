using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum Mode
    {
        Shop,
        Phone
    }
    public Mode mode = Mode.Shop;

    public static int ITEM_LAYER = 8;
    public static GameManager instance;

    public GameObject itemSpawner;
    public GameObject itemPrefab;

    //Client
    public Vector3 clientSpawner;
    public GameObject clientPrefab;
    public ClientData[] clientDataList;
    public Client currentClient;

    private int currentClientIndex = 0;

    //Dialogue UI
    public Image clientDialogueBubble;
    public Text clientDialogueText;

    public Button[] answerButtons;
    public Text[] answerTexts;

    public Image clientBye;
    public Text clientByeText;

    //Phone
    public GameObject phoneViewGO;
    public GameObject phoneExitButton;

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
            GameObject clientGO = GameObject.Instantiate(clientPrefab, clientSpawner, Quaternion.identity);
            currentClient = clientGO.GetComponent<Client>();
            currentClient.data = clientDataList[currentClientIndex];
            currentClient.init();
            currentClient.spawnClientItems();
        }
    }

    public void Answer(int id)
    {
        DesactiveAnswers();

        if (currentClient != null)
        {
            currentClient.Answer(id);
        }
    }

    public void DesactiveAnswers()
    {
        foreach (Button button in answerButtons)
        {
            button.gameObject.SetActive(false);
        }
    }

    public void ActivatePhone()
    {
        mode = Mode.Phone;
        UpdateMode();
    }

    public void UnactivatePhone()
    {
        mode = Mode.Shop;
        UpdateMode();
    }

    public void UpdateMode()
    {
        switch (mode)
        {
            case Mode.Phone:
                phoneViewGO.SetActive(true);
                phoneExitButton.SetActive(true);
                break;
            case Mode.Shop:
                phoneViewGO.SetActive(false);
                phoneExitButton.SetActive(false);
                break;
        }
    }
}
