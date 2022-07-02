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
    public GameObject scanObject;
    public Animator PortraitAnim;
    public Animator talkPanel;
    public Sprite prevProtrait;
    public bool isActionPanel;
    public int talkIndex;

    void Start()
    {
        Debug.Log(questManager.CheckQuest());
    }

    public void Action(GameObject Object)
    {
        // Get Current Object
        scanObject = Object;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);

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
            Debug.Log(questManager.CheckQuest(id));
            return;  // void return은 강제종료 역할
        }

        // Find Npc Data And Countinue Talk
        if (isNpc)
        {
            talk.SetMsg(talkData.Split(':')[0]);

            // Show Portrait
            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));    // Parse() : 문자열을 해당 타입으로 변환해주는 함수
            portraitImg.color = new Color(1, 1, 1, 1);

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

            // Hide Portrait
            portraitImg.color = new Color(1, 1, 1, 0);
        }

        // Next Talk
        isActionPanel = true;
        talkIndex++;
    }
}
