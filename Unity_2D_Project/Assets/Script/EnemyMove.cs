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
        Debug.DrawRay(frontVec, Vector3.down, new Color(1, 0, 0));   // DrawRay(��� ��ġ, ��� ����, ����) : ������ �󿡼��� Ray�� �׷��ִ� �Լ�
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 3, LayerMask.GetMask("Platform"));    // Raycast(��� ��ġ, ��� ����, ��� �Ÿ�, ���� Layer ����)

        if (rayHit.collider == null)  // rayHit�� ��������̹Ƿ� collider�� ����, ������� �ʴ´�.
        {
            // Debug.LogWarning("��� �������� �߰�!!");
            nextMove *= -1;
            CancelInvoke();   // CancelInvoke : ���� �۵� ���� ��� Invoke�Լ��� ���ߴ� �Լ�
            EnemyInvoke();
        }
    }

    // ����Լ� : �ڽ��� ������ ȣ���ϴ� �Լ�
    void EnemyNextMove()
    {
        // Set Next Move
        nextMove = Random.Range(-1, 2);   // Random.Range(�ּҰ�, �ִ밪) : �ּ� ~ �ִ� ������ ���� �� ����(�ִ� ����)

        // Sprite Animation
        ani.SetInteger("EnemyWalkSpeed", nextMove);  // SetInteger("�� �ִϸ��̼� �Լ�", ��)

        EnemyInvoke();

        // EnemyNextMove();     // ������ ���� ����Լ��� ����ϴ� ���� �����ϴ�.
    }

    void EnemyInvoke()
    {
        float nextInvokeTime = Random.Range(1.5f, 3.5f);

        // Filp Sprite
        if (nextMove != 0)
            sRenderer.flipX = nextMove == 1;

        // Recursive
        // Invoek : �־��� �ð��� ���� ��, ������ �Լ��� �����ϴ� �Լ�
        Invoke("EnemyNextMove", nextInvokeTime);     // 5�� �����̸� �� ����Լ�
    }
}
