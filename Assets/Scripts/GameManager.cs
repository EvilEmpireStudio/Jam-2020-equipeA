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
    public GameData data;
    public Client currentClient;

    public int currentClientIndex = 0;
    private int currentSMSIndex = 0;

    //Dialogue UI
    public Image clientDialogueBubble;
    public Text clientDialogueText;

    public Button[] answerButtons;
    public Text[] answerTexts;
    public GameObject[] censored;

    public Image clientBye;
    public Text clientByeText;

    //phone
    public GameObject phoneGO;

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
            currentClient.data = data.clientDataList[currentClientIndex++];
            currentClient.init();
            currentClient.spawnClientItems();

            if (data.smsDataList.Length > currentSMSIndex && data.smsDataList[currentSMSIndex].EventIndex == currentClientIndex)
            {
                if(!phoneGO.activeSelf)
                {
                    phoneGO.SetActive(true);
                }

                PhoneManager.instance.data = data.smsDataList[currentSMSIndex++];
                PhoneManager.instance.state = PhoneManager.State.ReceiveSMS;
            }
        }
    }

    public void Answer(int id)
    {
        GameManager.instance.clientDialogueBubble.GetComponent<Animation>().Play("bubbleAnimExit");

        if (currentClient.isAnswerCensored(id))
        {
            censored[id].SetActive(true);
            return;
        }

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

        foreach (GameObject go in censored)
        {
            go.SetActive(false);
        }
    }

    public void ActivatePhone()
    {
        mode = Mode.Phone;
        PhoneManager.instance.UpdateDisplay();
    }

    public void UnactivatePhone()
    {
        mode = Mode.Shop;
        PhoneManager.instance.UpdateDisplay();
    }
}
