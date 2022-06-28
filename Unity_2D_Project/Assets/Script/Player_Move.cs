using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public float jumpPower;
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
        // Jump
        if (Input.GetButtonDown("Jump") && !ani.GetBool("isJumping"))    // GetBool : ���� �������� animation�� �� �޾ƿ���
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            ani.SetBool("isJumping", true);
        }

        // ReleaseKey By Stop Speed
        if (Input.GetButtonUp("Horizontal"))
        {
            // normalized : ���� ũ�⸦ 1�� ���� ����(��������)
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        // Direction Sprite
        if (Input.GetButton("Horizontal"))
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
        // RayCast : Object �˻��� ���� Ray�� ��� ���(������ ������)

        // KeyControl By Move Speed
        float x = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * x, ForceMode2D.Impulse);

        // velocity : ����ӵ�
        if(rigid.velocity.x > maxSpeed)   // Right Max Speed
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if(rigid.velocity.x < maxSpeed*(-1))   // Left Max Speed
            rigid.velocity = new Vector2(maxSpeed*(-1), rigid.velocity.y);
     
        // Landing Player
        Debug.DrawRay(rigid.position, Vector3.down, new Color(1, 0, 0));   // DrawRay(��� ��ġ, ��� ����, ����) : ������ �󿡼��� Ray�� �׷��ִ� �Լ�

        // RaycastHit : Ray�� ���� ������Ʈ, RaycastHit ������ �ݶ��̴��� �˻� Ȯ���� �����ϴ�.
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));    // Raycast(��� ��ġ, ��� ����, ��� �Ÿ�, ���� Layer ����)

        if(rayHit.collider != null)  // rayHit�� ��������̹Ƿ� collider�� ����, ������� �ʴ´�.
        {
            // Debug.Log(rayHit.collider.name);
            if (rigid.velocity.y < 0)
            {
                //if(rayHit.distance < 0.5f)
                ani.SetBool("isJumping", false);
            }
        }
    }
}
