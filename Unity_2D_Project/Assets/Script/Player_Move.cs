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
            // normalized : 벡터 크기를 1로 만든 상태(단위벡터)
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        // Direction Sprite
        if (Input.GetButtonDown("Horizontal"))
            sRender.flipX = Input.GetAxisRaw("Horizontal") == -1;

        // Character Move Animation Set
        if (Mathf.Abs(rigid.velocity.x) < 0.3)     // Mathf : 수학 관련 함수를 제공하는 클래스(Abs : 절대값)
            ani.SetBool("isWalking", false);    // SetBool : Animator Parameters 매개변수가 bool인 경우 주는 값
        else
            ani.SetBool("isWalking", true);
    }

    void FixedUpdate()
    {
        // 캐릭터가 데굴데굴 구를경우 Rigidbody에서 Constraints에 Freeze Rotation Z를 체크해줘야 한다.

        // KeyControl By Move Speed
        float x = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * x, ForceMode2D.Impulse);

        // velocity : 현재속도
        if(rigid.velocity.x > maxSpeed)   // Right Max Speed
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if(rigid.velocity.x < maxSpeed*(-1))   // Left Max Speed
            rigid.velocity = new Vector2(maxSpeed*(-1), rigid.velocity.y);
    }
}
