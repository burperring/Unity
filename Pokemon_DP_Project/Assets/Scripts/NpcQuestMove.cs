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
    public NpcManager npcManager;
    public GameObject NextQuest;

    private Player player;
    private GameManager gameManager;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        gameManager = FindObjectOfType<GameManager>();

        // Check Finish Quest Destroy
        if(gameManager.doQuestNumber == questNumber)
            NpcQuestMove.Destroy(this.gameObject);
    }

    public IEnumerator MoveCoroutine()
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

            if(NextQuest == null)
                player.beforeNpc = null;

            player.isQuestNpcMove = false;

            // Set Finish Quest Number
            gameManager.doQuestNumber = questNumber;

            // Delete Qeust Trigger
            NpcQuestMove.Destroy(this.gameObject);
        }
    }
}
