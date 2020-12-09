using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public GameObject phoneViewGO;
    public GameObject phoneExitButton;

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
                break;
            case State.SMSLoaded:
                // waiting for a answer
                // if an answered has been selected 
                // display it and switch to SMSAnswered
                break;
            case State.SMSAnswered:
                //waiting for a new sms
            default:
                break;
        }

    }

    public void UpdateDisplay()
    {
        switch (GameManager.instance.mode)
        {
            case GameManager.Mode.Phone:
                phoneViewGO.SetActive(true);
                phoneExitButton.SetActive(true);
                break;
            case GameManager.Mode.Shop:
                phoneViewGO.SetActive(false);
                phoneExitButton.SetActive(false);
                break;
        }
    }
}
