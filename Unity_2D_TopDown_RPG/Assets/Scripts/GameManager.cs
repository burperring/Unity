using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    public TypeEffect talk;
    public Image portraitImg;
    public GameObject menuSet;
    public GameObject scanObject;
    public GameObject player;
    public Animator PortraitAnim;
    public Animator talkPanel;
    public Sprite prevProtrait;
    public Text questText;
    public bool isActionPanel;
    public int talkIndex;

    bool isNameUi;

    void Start()
    {
        GameLoad();
        questText.text = questManager.CheckQuest();
    }

    void Update()
    {
        // Sub Menu
        if (Input.GetButtonDown("Cancel"))
        {
            if (menuSet.activeSelf)
                menuSet.SetActive(false);
            else
                menuSet.SetActive(true);
        }
    }

    public void Action(GameObject Object)
    {
        // Get Current Object
        scanObject = Object;
        ObjData objData = scanObject.GetComponent<ObjData>();

        // Set Talk Text
        Talk(objData.id, objData.isNpc);

        // Set NPC Name
        if (isNameUi)
        {
            Text name = talk.nameUi.GetComponentInChildren<Text>();
            name.text = Object.name;
        }

        // Visible Talk for Action
        talkPanel.SetBool("isShow", isActionPanel);
    }

    void Talk(int id, bool isNpc)
    {
        int questTalkIndex = 0;
        string talkData = "";

        // Set Talk Data
        if (talk.isAnimation)
        {
            talk.SetMsg("");
            return;
        }
        else
        {
            questTalkIndex = questManager.GetQuestTalkIndex(id);
            talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);
        }

        // End Talk
        if(talkData == null)
        {
            isActionPanel = false;
            talkIndex = 0;
            questText.text = questManager.CheckQuest(id);
            return;  // void return은 강제종료 역할
        }

        // Find Npc Data And Countinue Talk
        if (isNpc)
        {
            talk.SetMsg(talkData.Split(':')[0]);

            // Show Portrait
            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));    // Parse() : 문자열을 해당 타입으로 변환해주는 함수
            portraitImg.color = new Color(1, 1, 1, 1);

            // Check NPC Name
            isNameUi = true;
            talk.nameUi.SetActive(isNameUi);

            // Animation Portrait
            if (prevProtrait != portraitImg.sprite)
            {
                PortraitAnim.SetTrigger("doEffect");
                prevProtrait = portraitImg.sprite;
            }
        }
        else
        {
            talk.SetMsg(talkData);

            // Check NPC Name
            isNameUi = false;
            talk.nameUi.SetActive(isNameUi);

            // Hide Portrait
            portraitImg.color = new Color(1, 1, 1, 0);
        }

        // Next Talk
        isActionPanel = true;
        talkIndex++;
    }

    public void GameSave()
    {
        // Save
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", player.transform.position.z);
        PlayerPrefs.SetInt("QuestId", questManager.questId);
        PlayerPrefs.SetInt("QuestActionIndex", questManager.questActionIndex);
        PlayerPrefs.Save();

        menuSet.SetActive(false);
    }

    public void GameLoad()
    {
        if (!PlayerPrefs.HasKey("PlayerX"))
            return;

        // Load
        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        float z = PlayerPrefs.GetFloat("PlayerZ");
        int questId = PlayerPrefs.GetInt("QuestId");
        int questActionIndex = PlayerPrefs.GetInt("QuestActionIndex");

        player.transform.position = new Vector3(x, y, z);
        questManager.questId = questId;
        questManager.questActionIndex = questActionIndex;
        questManager.ControlObject();
    }

    public void GameExit()
    {
        // Game Exit : 에디터에서는 실행되지 않는다.
        Application.Quit();
    }
}
