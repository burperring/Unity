using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Player Status Setting
    public float walkSpeed;
    public float runSpeed;
    public float jumpPower;

    // Player Input Button Value
    private float hAxis;        // Horizontal
    private float vAxis;        // Vertical
    private bool runDown;       // Run : Left Shift
    private bool jumpDown;      // Jump : Spacebar

    // Check Player Action
    float jumpDelay = 0.8f;
    float jumpTime = 0.8f;
    bool isMove;

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
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        runDown = Input.GetButton("Run");
        jumpDown = Input.GetButtonDown("Jump");
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
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * (runDown ? runSpeed : walkSpeed) * Time.deltaTime;

        anim.SetFloat("Horizontal", hAxis);
        anim.SetFloat("Vertical", vAxis);
        anim.SetFloat("Speed", isMove ? runDown ? runSpeed : walkSpeed : 0);
    }

    void Jump()
    {
        if (jumpDown && jumpTime > jumpDelay)
        {
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            anim.SetTrigger("doJump");
            jumpTime = 0f;
        }
    }
}
