using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NpcMove
{
    // Npc Move Direction setting
    public string[] direction;
}

public class NpcQuestMove : MonoBehaviour
{
    [SerializeField]
    public NpcMove npcMove;
    public int questNumber;
    public int questTrigger;
    public bool isNpcTransY;
    public NpcManager npcManager;

    private QuestManager questManager;
    private GameManager gameManager;

    private void Awake()
    {
        questManager = FindObjectOfType<QuestManager>();
        gameManager = FindObjectOfType<GameManager>();

        // Check Finish Quest Destroy
        if(gameManager.doQuestNumber == questNumber)
            NpcQuestMove.Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (questManager.questId == questTrigger)
        {
            StartCoroutine(MoveCoroutine());
            Debug.Log(questManager.GetQuestTalkIndex(questTrigger));
        }
    }

    IEnumerator MoveCoroutine()
    {
        if (npcMove.direction.Length != 0)
        {
            for (int i = 0; i < npcMove.direction.Length; i++)
            {
                // 실질적 이동구간
                yield return new WaitUntil(() => npcManager.isStopTalk);
                yield return new WaitUntil(() => npcManager.npcCanMove);
                npcManager.Move(npcMove.direction[i]);
            }

            // Set Finish Quest Number
            gameManager.doQuestNumber = questNumber;
            NpcQuestMove.Destroy(this.gameObject);
        }
    }
}
