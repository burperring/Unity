using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    static public Player player;

    public GameManager gameManager;
    public TalkManager talkManager;
    public BoxCollider2D boxCollider;
    
    public float walkSpeed;
    public float runSpeed;
    public float[] bikeSpeed;
    public int bikeCount;
    public int startPos;
    public string currentMapName;
    public bool isInHouse;                  // 현재 집 안, 밖을 체크하는 값
    public bool isBikeTalk;                 // 집안에서 자전거를 타려고 했을 경우 대화창
    public bool isBike;                     // 자전거를 타고 있을 경우 집 안으로 들어오지 못함
    public bool isQuestTalk = false;        // 퀘스트로 인한 강제적인 대화창인지 확인
    public bool isQuestNpcMove = false;     // 퀘스트로 인해 NPC가 강제적으로 움직이는 중인가

    Rigidbody2D rigid;
    public Animator anim;
    public GameObject scanObject;
    public GameObject npcObject;
    public GameObject beforeNpc;

    float h;
    float v;
    bool isHorizonMove;
    bool isRunMove;
    public Vector3 dirVec;

    private void Start()
    {
        if (player == null)
        {
            DontDestroyOnLoad(this.gameObject);
            DontDestroyOnLoad(gameManager);

            boxCollider = GetComponent<BoxCollider2D>();

            player = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move Value
        h = gameManager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        v = gameManager.isAction ? 0 : Input.GetAxisRaw("Vertical");
        isRunMove = Input.GetButton("Run");

        // Check Button Down & Up
        bool hDown = gameManager.isAction ? false : Input.GetButtonDown("Horizontal");
        bool vDown = gameManager.isAction ? false : Input.GetButtonDown("Vertical");
        bool hUp = gameManager.isAction ? false : Input.GetButtonUp("Horizontal");
        bool vUp = gameManager.isAction ? false : Input.GetButtonUp("Vertical");
        bool checkBike = gameManager.isAction ? false : Input.GetButtonDown("Bike");

        // Check Horizontal Move
        if (hDown)
            isHorizonMove = true;
        else if (vDown)
            isHorizonMove = false;
        else if (hUp || vUp)
            isHorizonMove = h != 0;

        // Animation
        if (anim.GetInteger("hAxisRaw") != h)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if (anim.GetInteger("vAxisRaw") != v)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else
        {
            anim.SetBool("isChange", false);
        }

        // Run Check
        if (Input.GetButtonDown("Run"))
        {
            anim.SetBool("isChange", true);
            anim.SetBool("isRun", true);
        }
        else if (Input.GetButtonUp("Run"))
        {
            anim.SetBool("isChange", true);
            anim.SetBool("isRun", false);
        }

        // Bike Check
        if (checkBike)
        {
            if (!isInHouse)
            {
                if (!anim.GetBool("isBike"))
                {
                    anim.SetBool("isBike", true);
                    isBike = true;
                }
                else
                {
                    anim.SetBool("isBike", false);
                    isBike = false;
                }
            }
            else
            {
                // Bike Talk
                gameManager.Action(1000);
                isBikeTalk = true;
            }
        }

        if(isBikeTalk && Input.GetButtonDown("Jump"))
        {
            gameManager.Action(1000);
        }
        else if(!gameManager.isAction)
        {
            isBikeTalk = false;
        }

        // Bike Count Check
        if (anim.GetBool("isBike"))
        {
            if (Input.GetButtonDown("Run"))
                bikeCount = bikeCount == 2 ? 0 : bikeCount + 1;
        }

        // Player Direction
        if (vDown && v == 1)
            dirVec = Vector3.up;
        else if (vDown && v == -1)
            dirVec = Vector3.down;
        else if (hDown && h == -1)
            dirVec = Vector3.left;
        else if (hDown && h == 1)
            dirVec = Vector3.right;

        if(npcObject != null)
        {
            if(npcObject.transform.position.y > player.transform.position.y)
                npcObject.transform.position = new Vector3(npcObject.transform.position.x, npcObject.transform.position.y, 1);
            else if(npcObject.transform.position.y < player.transform.position.y)
                npcObject.transform.position = new Vector3(npcObject.transform.position.x, npcObject.transform.position.y, -1);
        }

        // Scan Object
        if (Input.GetButtonDown("Jump") && scanObject != null && !isQuestTalk && !isQuestNpcMove)
        {
            gameManager.Action(scanObject); 
        }

        if (isQuestNpcMove)
        {
            if (Input.GetButtonDown("Jump") && scanObject != null && isQuestTalk)
            {
                gameManager.Action(scanObject);
            }
            if (Input.GetButtonDown("Jump") && scanObject == null && beforeNpc != null && isQuestTalk)
            {
                gameManager.Action(beforeNpc);
            }
        }
    }

    void FixedUpdate()
    {
        // Player Move
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);

        if(anim.GetBool("isBike"))
        {
            rigid.velocity = moveVec * bikeSpeed[bikeCount];
        }
        else
            rigid.velocity = isRunMove ? moveVec * runSpeed : moveVec * walkSpeed;

        // Ray
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 1.0f, LayerMask.GetMask("Object", "Npc"));

        if(rayHit.collider != null)
            scanObject = rayHit.collider.gameObject;
        else
            scanObject = null;

        // Npc Find
        RaycastHit2D npcHit1 = Physics2D.Raycast(rigid.position, new Vector3(-1,1,0), 1.0f, LayerMask.GetMask("Npc"));
        RaycastHit2D npcHit2 = Physics2D.Raycast(rigid.position, Vector3.up, 1.0f, LayerMask.GetMask("Npc"));
        RaycastHit2D npcHit3 = Physics2D.Raycast(rigid.position, new Vector3(1, 1, 0), 1.0f, LayerMask.GetMask("Npc"));
        RaycastHit2D npcHit4 = Physics2D.Raycast(rigid.position, Vector3.right, 1.0f, LayerMask.GetMask("Npc"));
        RaycastHit2D npcHit5 = Physics2D.Raycast(rigid.position, new Vector3(1, -1, 0), 1.0f, LayerMask.GetMask("Npc"));
        RaycastHit2D npcHit6 = Physics2D.Raycast(rigid.position, Vector3.down, 1.0f, LayerMask.GetMask("Npc"));
        RaycastHit2D npcHit7 = Physics2D.Raycast(rigid.position, new Vector3(-1, -1, 0), 1.0f, LayerMask.GetMask("Npc"));
        RaycastHit2D npcHit8 = Physics2D.Raycast(rigid.position, Vector3.left, 1.0f, LayerMask.GetMask("Npc"));

        if (npcHit1.collider != null)
            npcObject = npcHit1.collider.gameObject;
        else if (npcHit2.collider != null)
            npcObject = npcHit2.collider.gameObject;
        else if (npcHit3.collider != null)
            npcObject = npcHit3.collider.gameObject;
        else if (npcHit4.collider != null)
            npcObject = npcHit4.collider.gameObject;
        else if (npcHit5.collider != null)
            npcObject = npcHit5.collider.gameObject;
        else if (npcHit6.collider != null)
            npcObject = npcHit6.collider.gameObject;
        else if (npcHit7.collider != null)
            npcObject = npcHit7.collider.gameObject;
        else if (npcHit8.collider != null)
            npcObject = npcHit8.collider.gameObject;
        else
            npcObject = null;
    }
}
