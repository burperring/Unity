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

    Rigidbody2D rigid;
    Animator anim;
    GameObject scanObject;

    float h;
    float v;
    bool isHorizonMove;
    bool isRunMove;
    Vector3 dirVec;

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
            if (!anim.GetBool("isBike"))
                anim.SetBool("isBike", true);
            else
                anim.SetBool("isBike", false);
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

        // Scan Object
        if (Input.GetButtonDown("Jump") && scanObject != null)
        {
            gameManager.Action(scanObject);
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
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 1.0f, LayerMask.GetMask("Object"));

        if(rayHit.collider != null)
            scanObject = rayHit.collider.gameObject;
        else
            scanObject = null;
    }

    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.tag == "StairDown")
    //    {
    //       startPos += 10;
    //        SceneManager.LoadScene("Stage " + gameManager.stage + "-" + (gameManager.floor - 1));
    //    }
    //    else if (collision.tag == "StairUp")
    //    {
    //        startPos += 20;
    //        SceneManager.LoadScene("Stage " + gameManager.stage + "-" + (gameManager.floor + 1));
    //    }
    //}
}
