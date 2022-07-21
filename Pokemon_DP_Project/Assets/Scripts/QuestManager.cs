using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;

    Dictionary<int, QuestData> questList;

    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        // 1000000 : Player Mom
        questList.Add(10, new QuestData("용식이와 만나기", new int[] { 1000000, 1000000, 2000000, 2000000 }));

        // Dummy Data
        questList.Add(20, new QuestData("퀘스트 올 클리어", new int[] { 0 }));
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public void CheckQuest(int id)
    {
        if(id == questList[questId].npcId[questActionIndex])
            questActionIndex++;

        if (questActionIndex == questList[questId].npcId.Length)
            NextQuest();
    }

    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }
}
