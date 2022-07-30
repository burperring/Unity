using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNpcTrigger : MonoBehaviour
{
    public int iQuestTrigger;
    public GameObject Npc;

    private QuestManager questManager;

    private void Awake()
    {
        questManager = FindObjectOfType<QuestManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(iQuestTrigger == questManager.questId)
        {
            Npc.SetActive(true);
        }
        else
        {
            Npc.SetActive(false);
        }
    }
}
