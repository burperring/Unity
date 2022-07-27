using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NpcData
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
    public NpcData npc;
    public float speed;
    public int walkCount;
    public bool moveCheck;
    public bool isStopTalk = true;      // Npc�� ��ȭ�� ������ ��� Npc�� ���߰� �ϴ� ��
    public bool npcCanMove = true;

    private Player player;
    private int currentWalkCount;

    Animator anim;
    SpriteRenderer sprite;

    Vector3 dirVec;
    Vector3 beforeDir;

    private void Start()
    {
        // Quest ���� Npc�� �ƴ� �ݺ������� �����̴� Npc�� ��� �ٷ� �����̰� ������ش�.
        if(npc.repeatNpc)
            StartCoroutine(MoveCoroutine());
    }

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // ��ȭ �������� ���� Npc �������� ���������� ���� ��� �ش� ��ȭ�� ������ �� �ٽ� �����̰� ������ش�.
        if (!player.gameManager.isAction && !isStopTalk)
        {
            isStopTalk = true;
            player.gameManager.isAction = true;
            player.isQuestTalk = false;
        }
    }

    public void Move(string dir)
    {
        StartCoroutine(MoveNpc(dir));
    }

    void PlayerLook()
    {
        // Set Player Look, RatHit Position
        if (player.npcObject != null)
        {
            isStopTalk = false;

            if (player.npcObject.transform.position.y > player.transform.position.y)
            {
                dirVec.y = -1;
                player.anim.SetInteger("vAxisRaw", 1);
                player.anim.SetBool("isChange", true);
                player.dirVec = Vector3.up;
            }
            else if (player.npcObject.transform.position.y < player.transform.position.y)
            {
                dirVec.y = 1;
                player.anim.SetInteger("vAxisRaw", -1);
                player.anim.SetBool("isChange", true);
                player.dirVec = Vector3.down;
            }
            else if (player.npcObject.transform.position.x > player.transform.position.x)
            {
                dirVec.x = -1;
                player.anim.SetInteger("hAxisRaw", 1);
                player.anim.SetBool("isChange", true);
                player.dirVec = Vector3.right;
            }
            else if (player.npcObject.transform.position.x < player.transform.position.x)
            {
                dirVec.x = 1;
                player.anim.SetInteger("hAxisRaw", -1);
                player.anim.SetBool("isChange", true);
                player.dirVec = Vector3.left;
            }
        }
    }

    void LongPlayerLook()
    {
        // Set Player Look, RatHit Position
        if (player.beforeNpc != null)
        {
            isStopTalk = false;

            if (player.beforeNpc.transform.position.y > player.transform.position.y)
            {
                dirVec.y = -1;
                player.anim.SetInteger("vAxisRaw", 1);
                player.anim.SetBool("isChange", true);
                player.dirVec = Vector3.up;
            }
            else if (player.beforeNpc.transform.position.y < player.transform.position.y)
            {
                dirVec.y = 1;
                player.anim.SetInteger("vAxisRaw", -1);
                player.anim.SetBool("isChange", true);
                player.dirVec = Vector3.down;
            }
            else if (player.beforeNpc.transform.position.x > player.transform.position.x)
            {
                dirVec.x = -1;
                player.anim.SetInteger("hAxisRaw", 1);
                player.anim.SetBool("isChange", true);
                player.dirVec = Vector3.right;
            }
            else if (player.beforeNpc.transform.position.x < player.transform.position.x)
            {
                dirVec.x = 1;
                player.anim.SetInteger("hAxisRaw", -1);
                player.anim.SetBool("isChange", true);
                player.dirVec = Vector3.left;
            }
        }
    }

    void PlayerTalk()
    {
        // ���������� ���ֺ� ĳ���Ϳ� ��ȭ�ϰ� �����.
        if(player.scanObject != null)
        {
            player.gameManager.Action(player.scanObject);
            player.beforeNpc = player.scanObject;
        }
    }

    void LongPlayerTalk()
    {
        if (player.beforeNpc != null)
        {
            player.gameManager.Action(player.beforeNpc);
        }
    }

    IEnumerator MoveNpc(string dir)
    {
        dirVec.Set(0, 0, dirVec.z);

        // While ������ ���� ���� ���� ���� ��ġ
        npcCanMove = false;

        switch (dir)
        {
            case "Start":
                player.gameManager.isAction = true;
                break;
            case "Finish":
                player.gameManager.isAction = false;
                break;
            case "Appear":
                sprite.color = new Color(1, 1, 1, 1);
                break;
            case "Disappear":
                sprite.color = new Color(1, 1, 1, 0);
                break;
            case "Up":
                moveCheck = npc.npcMove ? true : false;
                dirVec.y = 1f;
                beforeDir.y = 1f;
                beforeDir.x = 0;
                break;
            case "Down":
                moveCheck = npc.npcMove ? true : false;
                dirVec.y = -1f;
                beforeDir.y = -1f;
                beforeDir.x = 0;
                break;
            case "Right":
                moveCheck = npc.npcMove ? true : false;
                dirVec.x = 1f;
                beforeDir.y = 0;
                beforeDir.x = 1f;
                break;
            case "Left":
                moveCheck = npc.npcMove ? true : false;
                dirVec.x = -1f;
                beforeDir.y = 0;
                beforeDir.x = -1f;
                break;
            case "Walk":
                npc.npcMove = true;
                moveCheck = false;
                dirVec = beforeDir;
                break;
            case "Stop":
                npc.npcMove = false;
                moveCheck = false;
                dirVec = beforeDir;
                break;
            case "Talk":
                player.isQuestTalk = true;
                PlayerLook();
                Invoke("PlayerTalk", 0.1f);
                break;
            case "LongTalk":
                player.isQuestTalk = true;
                LongPlayerLook();
                Invoke("LongPlayerTalk", 0.1f);
                break;
        }

        anim.SetFloat("vAxisRaw", dirVec.x);
        anim.SetFloat("hAxisRaw", dirVec.y);
        anim.SetBool("isWalking", moveCheck);

        while (currentWalkCount < walkCount)
        {
            if (npc.npcMove && moveCheck)
                transform.Translate(dirVec.x * speed, dirVec.y * speed, dirVec.z);
            else
                transform.Translate(dirVec.x * 0, dirVec.y * 0, dirVec.z);

            if (npc.npcMove)
                currentWalkCount++;
            else
                currentWalkCount += 5;

            yield return new WaitForSeconds(0.01f);
        }

        currentWalkCount = 0;

        anim.SetBool("isWalking", moveCheck);

        // While ������ ���� ���� ���� ���� ��ġ
        npcCanMove = true;
    }

    IEnumerator MoveCoroutine()
    {
        if (npc.direction.Length != 0)
        {
            for (int i = 0; i < npc.direction.Length; i++)
            {
                // ������ �̵�����
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
