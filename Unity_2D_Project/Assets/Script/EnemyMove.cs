using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator ani;
    SpriteRenderer sRenderer;
    public int nextMove;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        sRenderer = GetComponent<SpriteRenderer>();

        EnemyNextMove();
    }

    void FixedUpdate()
    {
        // Enemy Move
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        // Find Cliff Enemy
        Vector2 frontVec = new Vector2(rigid.position.x + (nextMove * 0.3f), rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(1, 0, 0));   // DrawRay(쏘는 위치, 쏘는 방향, 색깔) : 에디터 상에서만 Ray를 그려주는 함수
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 3, LayerMask.GetMask("Platform"));    // Raycast(쏘는 위치, 쏘는 방향, 쏘는 거리, 닿을 Layer 선택)

        if (rayHit.collider == null)  // rayHit는 물리기반이므로 collider가 들어간다, 관통되지 않는다.
        {
            // Debug.LogWarning("경고 낭떠러지 발견!!");
            nextMove *= -1;
            CancelInvoke();   // CancelInvoke : 현재 작동 중인 모든 Invoke함수를 멈추는 함수
            EnemyInvoke();
        }
    }

    // 재귀함수 : 자신을 스스로 호출하는 함수
    void EnemyNextMove()
    {
        // Set Next Move
        nextMove = Random.Range(-1, 2);   // Random.Range(최소값, 최대값) : 최소 ~ 최대 범위의 랜덤 수 생성(최대 제외)

        // Sprite Animation
        ani.SetInteger("EnemyWalkSpeed", nextMove);  // SetInteger("들어간 애니메이션 함수", 값)

        EnemyInvoke();

        // EnemyNextMove();     // 딜레이 없이 재귀함수를 사용하는 것은 위험하다.
    }

    void EnemyInvoke()
    {
        float nextInvokeTime = Random.Range(1.5f, 3.5f);

        // Filp Sprite
        if (nextMove != 0)
            sRenderer.flipX = nextMove == 1;

        // Recursive
        // Invoek : 주어진 시간이 지난 뒤, 지정된 함수를 실행하는 함수
        Invoke("EnemyNextMove", nextInvokeTime);     // 5초 딜레이를 준 재귀함수
    }
}
