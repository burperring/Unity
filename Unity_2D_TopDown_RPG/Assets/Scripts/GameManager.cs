using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    public Text talkText;
    public Image portraitImg;
    public GameObject scanObject;
    public GameObject talkPanel;
    public bool isActionPanel;
    public int talkIndex;

    void Start()
    {
        Debug.Log(questManager.CheckQuest());
    }

    public void Action(GameObject Object)
    {
        scanObject = Object;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);

        talkPanel.SetActive(isActionPanel);
    }

    void Talk(int id, bool isNpc)
    {
        // Set Talk Data
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);

        // End Talk
        if(talkData == null)
        {
            isActionPanel = false;
            talkIndex = 0;
            Debug.Log(questManager.CheckQuest(id));
            return;  // void return�� �������� ����
        }

        // Find Npc Data And Countinue Talk
        if (isNpc)
        {
            talkText.text = talkData.Split(':')[0];

            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));    // Parse() : ���ڿ��� �ش� Ÿ������ ��ȯ���ִ� �Լ�
            portraitImg.color = new Color(1, 1, 1, 1);
        }
        else
        {
            talkText.text = talkData;
            portraitImg.color = new Color(1, 1, 1, 0);
        }

        isActionPanel = true;
        talkIndex++;
    }
}
