using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static public GameManager gameManager;

    public TalkManager talkManager;
    public QuestManager questManager;

    public Text talkText;
    public GameObject canvas;
    public GameObject talkPanel;
    public GameObject scanObject;

    public bool isAction;
    public int talkIndex;
    public int stage;
    public int count;
    public int doQuestNumber;

    private void Awake()
    {
        if (gameManager == null)
        {
            DontDestroyOnLoad(talkManager);
            DontDestroyOnLoad(questManager);
            DontDestroyOnLoad(canvas);

            gameManager = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Action(GameObject scanObj)
    {
        // Get Current Object
        isAction = true;      
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id);

        // Visible Talk for Action
        talkPanel.SetActive(isAction);
    }

    public void Action(int id)
    {
        // Bike Talk¿ª ¿ß«— Action
        isAction = true;
        Talk(id);

        talkPanel.SetActive(isAction);
    }

    void Talk(int id)
    {
        // Set Talk Data
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);

        // End Talk
        if (talkData == null)
        {
            talkIndex = 0;
            isAction = false;
            questManager.CheckQuest(id);
            return;
        }

        // Continue Talk
        talkText.text = talkData;

        // Next Talk
        isAction = true;
        talkIndex++;
    }
}
