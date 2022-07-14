using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public float[] bikeSpeed;
    public int bikeCount;

    Rigidbody2D rigid;
    Animator anim;

    float h;
    float v;
    bool isHorizonMove;
    bool isRunMove;
    Vector3 dirVec;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move Value
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        isRunMove = Input.GetButton("Run");

        // Check Button Down & Up
        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");

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
        if(Input.GetButtonDown("Bike"))
        {
            if(!anim.GetBool("isBike"))
                anim.SetBool("isBike", true);
            else
                anim.SetBool("isBike", false);
        }

        // Bike Count Check
        if(anim.GetBool("isBike"))
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

    }
}
