using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public float maxSpeed;
    Rigidbody2D rigid;
    SpriteRenderer sRender;
    Animator ani;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sRender = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        // ReleaseKey By Stop Speed
        if(Input.GetButtonUp("Horizontal"))
        {
            // normalized : ���� ũ�⸦ 1�� ���� ����(��������)
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        // Direction Sprite
        if (Input.GetButtonDown("Horizontal"))
            sRender.flipX = Input.GetAxisRaw("Horizontal") == -1;

        // Character Move Animation Set
        if (Mathf.Abs(rigid.velocity.x) < 0.3)     // Mathf : ���� ���� �Լ��� �����ϴ� Ŭ����(Abs : ���밪)
            ani.SetBool("isWalking", false);    // SetBool : Animator Parameters �Ű������� bool�� ��� �ִ� ��
        else
            ani.SetBool("isWalking", true);
    }

    void FixedUpdate()
    {
        // ĳ���Ͱ� �������� ������� Rigidbody���� Constraints�� Freeze Rotation Z�� üũ����� �Ѵ�.

        // KeyControl By Move Speed
        float x = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * x, ForceMode2D.Impulse);

        // velocity : ����ӵ�
        if(rigid.velocity.x > maxSpeed)   // Right Max Speed
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if(rigid.velocity.x < maxSpeed*(-1))   // Left Max Speed
            rigid.velocity = new Vector2(maxSpeed*(-1), rigid.velocity.y);
    }
}
