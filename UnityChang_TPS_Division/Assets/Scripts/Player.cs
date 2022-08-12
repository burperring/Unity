using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Player Status Setting
    public float donwSpeed;
    public float walkSpeed;
    public float runSpeed;
    public float jumpPower;

    // Player Input Button Value
    private float hAxis;        // Horizontal
    private float vAxis;        // Vertical
    private bool runDown;       // Run : Left Shift
    private bool jumpDown;      // Jump : Spacebar
    private bool getDown;       // DownUp : Ctrl
    private bool sDown1;        // Weapon1 : 1
    private bool sDown2;        // Weapon2 : 2
    private bool sDown3;        // Hand : 3

    // Check Player Action
    float jumpDelay = 0.9f;
    float jumpTime = 0.9f;
    bool isMove;
    bool isDown;

    Vector3 moveVec;

    Rigidbody rigid;
    Animator anim;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        jumpTime += Time.deltaTime;

        GetInput();
        MoveCheck();
        Move();
        Jump();
        Swap();
        UpDown();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        runDown = Input.GetButton("Run");
        jumpDown = Input.GetButtonDown("Jump");
        getDown = Input.GetButtonDown("DownUp");
        sDown1 = Input.GetButtonDown("Weapon1");
        sDown2 = Input.GetButtonDown("Weapon2");
        sDown3 = Input.GetButtonDown("Weapon3");
    }

    void MoveCheck()
    {
        if (Mathf.Abs(hAxis) > 0 || Mathf.Abs(vAxis) > 0)
            isMove = true;
        else
            isMove = false;
    }

    void Move()
    {
        if (isDown && vAxis == 0)
        {
            anim.SetFloat("Vertical", vAxis);
            return;
        }

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * (isDown ? donwSpeed : runDown ? runSpeed : walkSpeed) * Time.deltaTime;

        anim.SetFloat("Horizontal", hAxis);
        anim.SetFloat("Vertical", vAxis);
        anim.SetFloat("Speed", isMove ? isDown ? donwSpeed : runDown ? runSpeed : walkSpeed : 0);
    }

    void Jump()
    {
        if (!isDown && jumpDown && jumpTime > jumpDelay)
        {
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            anim.SetTrigger("doJump");
            jumpTime = 0f;
        }
    }

    void Swap()
    {
        if (sDown1 || sDown2)
        {
            anim.SetBool("isStrafing", true);
            jumpDelay = 1.5f;
        }
        else if (sDown3)
        {
            anim.SetBool("isStrafing", false);
            jumpDelay = 0.9f;
        }
    }

    void UpDown()
    {
        if (getDown && !isDown)
        {
            anim.SetBool("isDown", true);
            isDown = true;
        }
        else if (getDown && isDown)
        {
            anim.SetBool("isDown", false);
            isDown = false;
        }
    }
}
