using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcQuestTrigger : MonoBehaviour
{
    public NpcQuestMove npcQuestMove;

    private Player player;
    private QuestManager questManager;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        questManager = FindObjectOfType<QuestManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (questManager.questId + questManager.questActionIndex == npcQuestMove.questTrigger)
        {
            player.isQuestNpcMove = true;
            StartCoroutine(npcQuestMove.MoveCoroutine());
            Debug.Log(questManager.GetQuestTalkIndex(npcQuestMove.questTrigger));
        }
    }
}
