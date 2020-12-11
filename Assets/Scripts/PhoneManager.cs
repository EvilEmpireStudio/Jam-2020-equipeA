using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PhoneManager : MonoBehaviour
{
    public SMS data;
    
    public enum State
    {
        ReceiveSMS,
        SMSLoaded,
        SMSAnswered,
        Nothing
    }
    public State state = State.Nothing;

    public bool notif = false;
    public bool open = false;

    //display
    public GameObject phoneNotif;
    public GameObject phoneViewGO;
    public GameObject phoneExitButton;
    public GameObject mainSMSGO;
    public Text mainSMS;

    //answers
    public GameObject[] options;
    public Text[] optionText;

    public GameObject answerGO;
    public Text answer;
    public int answerId = -1;

    public static PhoneManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (data == null)
        {
            return;
        }

        switch(state)
        {
            case State.ReceiveSMS:
                //load notification
                //load text
                //load answers
                // switch to SMSLoaded
                notif = true;
                mainSMSGO.SetActive(true);
                mainSMS.text = data.line;

                for (int i = 0; i < data.answers.Length; i++)
                {
                    options[i].SetActive(true);
                    optionText[i].text = data.answers[i];
                }

                answer.gameObject.SetActive(false);
                answerGO.SetActive(false);

                state = State.SMSLoaded;

                break;
            case State.SMSLoaded:
                // waiting for a answer
                // if an answered has been selected 
                // display it and switch to SMSAnswered
                if (answerId >= 0)
                {
                    answer.text = data.answers[answerId];
                    answerGO.SetActive(true);
                    answer.gameObject.SetActive(true);

                    for (int i = 0; i < data.answers.Length; i++)
                    {
                        options[i].SetActive(false);
                    }

                    answerId = -1;
                    state = State.SMSAnswered;
                }
                break;
            case State.SMSAnswered:
                //waiting for a new sms
            default:
                break;
        }

        if (notif != phoneNotif.activeSelf)
        {
            phoneNotif.SetActive(notif);
        }

    }

    public void UpdateDisplay()
    {
        switch (GameManager.instance.mode)
        {
            case GameManager.Mode.Phone:
                phoneViewGO.SetActive(true);
                phoneExitButton.SetActive(true);
                open = true;
                if (notif)
                {
                    notif = false;
                }
                break;
            case GameManager.Mode.Shop:
                phoneViewGO.SetActive(false);
                phoneExitButton.SetActive(false);
                open = false;
                break;
        }
    }

    public void Answer(int id)
    {
        answerId = id;

        if (data.lastSMS)
        {
            SceneManager.LoadScene("EndScene");
        }
    }
}
