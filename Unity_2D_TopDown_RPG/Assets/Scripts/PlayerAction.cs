using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float Speed;
    public GameManager manager;

    Rigidbody2D rigid;
    Animator anim;
    GameObject scanObject;
    float h;
    float v;
    bool isHorizonMove;
    Vector3 dirVec;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Move Value
        h = manager.isActionPanel ? 0 : Input.GetAxisRaw("Horizontal");
        v = manager.isActionPanel ? 0 : Input.GetAxisRaw("Vertical");

        // Check Button Down & Up
        bool hDown = manager.isActionPanel ? false : Input.GetButtonDown("Horizontal");
        bool vDown = manager.isActionPanel ? false : Input.GetButtonDown("Vertical");
        bool hUp = manager.isActionPanel ? false : Input.GetButtonUp("Horizontal");
        bool vUp = manager.isActionPanel ? false : Input.GetButtonUp("Vertical");

        // Check Horizontal Move
        if (hDown)
            isHorizonMove = true;
        else if (vDown)
            isHorizonMove = false;
        else if (hUp || vUp)
            isHorizonMove = h != 0;

        // Animation
        if(anim.GetInteger("hAxisRaw") != h)
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

        // Player Direction
        if (vDown && v == 1)
            dirVec = Vector3.up;
        else if (vDown && v == -1)
            dirVec = Vector3.down;
        else if(hDown && h == -1)
            dirVec = Vector3.left;
        else if(hDown && h == 1)
            dirVec = Vector3.right;

        // Scan Object
        if (Input.GetButtonDown("Jump") && scanObject != null)
        {
            manager.Action(scanObject);
        }
    }

    void FixedUpdate()
    {
        // Player Move
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * Speed;

        // Ray(스캔할 경우 대부분 Ray를 활용한다)
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0,1,0)); // DrawRay(쏘는 위치, 쏘는 길이, 색깔)
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 1.0f, LayerMask.GetMask("Object")); // Raycast(쏘는 위치, 쏘는 방향, 쏘는 길이, 찾는 값)

        // Find Object
        if (rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
            scanObject = null;
    }
}
