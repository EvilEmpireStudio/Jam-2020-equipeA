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

    public int itemsToBuyNb = 0;

    //dialogue
    private int currentLine = 0;
    private float lineCooldown = 0f;
    private enum LineState
    {
        NotDisplayed,
        InProgress,
        Finished
    }
    private LineState currentLineState = LineState.NotDisplayed;
    private bool answersLoaded = false;
    private bool answerDone = false;
    private int currentAnswer = -1;


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
        switch (state)
        {
            case State.Dialogue :
                if (currentLine >= data.lines.Length && itemsToBuyNb <= 0)
                {
                    Animator animator = gameObject.GetComponent<Animator>();
                    animator.SetBool("CanLeave", true);
                }

                UpdateDialogue();
                break;
            case State.Leaving:
                ResetDialogue();
                if (data.byeLine.Length > 2)
                {
                    GameManager.instance.clientBye.gameObject.SetActive(true);
                    GameManager.instance.clientByeText.text = data.byeLine;
                }
                break;
            case State.Unspawn:
                GameManager.instance.clientBye.gameObject.SetActive(false);
                GameObject.Destroy(this);
                break;
        }
    }

    private void ResetDialogue()
    {
        currentLineState = LineState.NotDisplayed;
        answersLoaded = false;
        answerDone = false;
        currentAnswer = -1;
        GameManager.instance.clientDialogueBubble.gameObject.SetActive(false);
        GameManager.instance.DesactiveAnswers();
        currentLine = 0;
    }

    private void UpdateDialogue()
    {
        if (data.lines.Length > currentLine)
        {
            switch (currentLineState)
            {
                case LineState.NotDisplayed:
                    //load the ui
                    //put in InProgress state;
                    GameManager.instance.clientDialogueText.text = data.lines[currentLine].line;
                    GameManager.instance.clientDialogueBubble.gameObject.SetActive(true);
                    currentLineState = LineState.InProgress;
                    break;
                case LineState.InProgress:
                    //wait for the end
                    // could be a dealy
                    // could be waiting for an input
                    // switch to Finished state

                    if (lineCooldown > 0f)
                    {
                        lineCooldown += Time.deltaTime;
                        if (lineCooldown > data.lines[currentLine].delay)
                        {
                            currentLineState = LineState.Finished;
                            lineCooldown = 0f;
                            break;
                        }
                    }

                    if (data.lines[currentLine].answers.Length <= 0 && lineCooldown <= 0f)
                    {
                        lineCooldown = 0.01f;
                    }
                    else if (!answersLoaded)
                    {
                        //load answers
                        int i = 0;
                        foreach (string answer in data.lines[currentLine].answers)
                        {
                            GameManager.instance.answerButtons[i].gameObject.SetActive(true);
                            GameManager.instance.answerTexts[i].text = answer;
                            i++;
                        }
                        answersLoaded = true;
                    }
                    else if (answersLoaded && answerDone)
                    {
                        // can use currentAnswer info
                        currentLineState = LineState.Finished;
                    }
                    break;
                case LineState.Finished:
                    // switch to next line if possible
                    // switch to NotDisplayed or nothing
                    GameManager.instance.clientDialogueBubble.gameObject.SetActive(false);
                    GameManager.instance.DesactiveAnswers();
                    currentLine++;
                    currentLineState = LineState.NotDisplayed;
                    answersLoaded = false;
                    answerDone = false;
                    currentAnswer = -1;
                    break;
            }
        }
    }

    public void Answer (int id)
    {
        currentAnswer = id;
        answerDone = true;
    }

    public bool isAnswerCensored(int id)
    {
        return data.lines[currentLine].censored.Length > id && data.lines[currentLine].censored[id];
    }

    public void init()
    {
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
            //float yOffset = itemData.sprite.rect.height / (2 * itemData.sprite.pixelsPerUnit);
            //spawnPos.Set(spawnPos.x, spawnPos.y + yOffset, spawnPos.z);
            GameObject itemGO = GameObject.Instantiate(GameManager.instance.itemPrefab, spawnPos, Quaternion.identity);
            itemGO.transform.SetParent(GameManager.instance.itemSpawner.transform);
            Item item = itemGO.GetComponent<Item>();
            item.data = itemData;
            item.client = this;
            item.applyData();
        }
        GameManager.instance.itemSpawner.GetComponent<Animation>().Play();
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
