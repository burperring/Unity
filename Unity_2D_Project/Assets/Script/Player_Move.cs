using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public GameManager gameManager;
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

    void Update()   // 1ȸ �Է��� �ؼ� ������ ��� �׳� Update ���
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

    void FixedUpdate()  // ������� Update
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // Attack
            if (rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
                OnAttack(collision.transform);
            else // Damaged
                OnDamaged(collision.transform.position);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Items")
        {
            // Point
            bool isBronze = collision.gameObject.name.Contains("Bronze");
            bool isSilver = collision.gameObject.name.Contains("Silver");
            bool isGold = collision.gameObject.name.Contains("Gold");

            if(isBronze)
                gameManager.stagePoint += 10;
            else if(isSilver)
                gameManager.stagePoint += 50;
            else if(isGold)
                gameManager.stagePoint += 100;

            // Deactive Item
            collision.gameObject.SetActive(false);
        }
        else if(collision.gameObject.tag == "Finish")
        {
            // Next Stage
            gameManager.NextStage();
        }
    }

    void OnAttack(Transform enemy)
    {
        // Point
        gameManager.stagePoint += 100;

        // Reaction Force
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

        // Enemy Die
        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        enemyMove.OnDamaged();
    }

    void OnDamaged(Vector2 targetPos)
    {
        // Animation
        ani.SetTrigger("doDamaged");

        // Change Layer (Immortal Active)
        gameObject.layer = 11;

        // View Alpha
        sRender.color = new Color(1, 1, 1, 0.4f);   // Color�� 4��° ���� ������ ��Ÿ����.

        // Reaction Force
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1)*9, ForceMode2D.Impulse);

        Invoke("OffDamaged", 2);
    }

    void OffDamaged()
    {
        // Change Layer (Immortal Active)
        gameObject.layer = 10;

        // View Alpha
        sRender.color = new Color(1, 1, 1, 1);
    }
}
