using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NpcMove
{
    // Npc Move Check
    public bool npcMove;

    // Don't Stop Npc Check
    public bool repeatNpc;

    // Npc Move Direction setting
    public string[] direction;
}

public class NpcManager : MonoBehaviour
{
    [SerializeField]
    public NpcMove npc;
    public float speed;
    public int walkCount;

    private int currentWalkCount;
    protected bool npcCanMove = true;

    Animator anim;

    Vector3 dirVec;

    private void Start()
    {
        StartCoroutine(MoveCoroutine());
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Move(string dir)
    {
        StartCoroutine(MoveNpc(dir));
    }

    IEnumerator MoveNpc(string dir)
    {
        dirVec.Set(0, 0, dirVec.z);

        // While 무한정 도는 것을 막기 위한 장치
        npcCanMove = false;

        switch (dir)
        {
            case "Up":
                dirVec.y = 1f;
                break;
            case "Down":
                dirVec.y = -1f;
                break;
            case "Right":
                dirVec.x = 1f;
                break;
            case "Left":
                dirVec.x = -1f;
                break;
            case "Walk":
                npc.npcMove = true;
                break;
            case "Stop":
                npc.npcMove = false;
                break;
        }

        anim.SetFloat("vAxisRaw", dirVec.x);
        anim.SetFloat("hAxisRaw", dirVec.y);
        anim.SetBool("isWalking", npc.npcMove);

        while (currentWalkCount < walkCount)
        {
            if (npc.npcMove)
                transform.Translate(dirVec.x * speed, dirVec.y * speed, dirVec.z);
            else
                transform.Translate(dirVec.x * 0, dirVec.y * 0, dirVec.z);

            currentWalkCount++;

            yield return new WaitForSeconds(0.01f);
        }

        currentWalkCount = 0;

        anim.SetBool("isWalking", npc.npcMove);

        // While 무한정 도는 것을 막기 위한 장치
        npcCanMove = true;
    }

    IEnumerator MoveCoroutine()
    {
        if(npc.direction.Length != 0)
        {
            for(int i = 0; i < npc.direction.Length; i++)
            {
                // 실질적 이동구간
                yield return new WaitUntil(() => npcCanMove);
                Move(npc.direction[i]);
                
                // Check Repeat Npc
                if (npc.repeatNpc)
                    if (i == npc.direction.Length - 1)
                        i = -1;
            }
        }
    }
}
