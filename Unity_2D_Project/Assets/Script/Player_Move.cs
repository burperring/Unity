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

    void Update()   // 1회 입력을 해서 진행할 경우 그냥 Update 사용
    {
        // Jump
        if (Input.GetButtonDown("Jump") && !ani.GetBool("isJumping"))    // GetBool : 현재 진행중인 animation의 값 받아오기
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            ani.SetBool("isJumping", true);
        }

        // ReleaseKey By Stop Speed
        if (Input.GetButtonUp("Horizontal"))
        {
            // normalized : 벡터 크기를 1로 만든 상태(단위벡터)
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        // Direction Sprite
        if (Input.GetButton("Horizontal"))
            sRender.flipX = Input.GetAxisRaw("Horizontal") == -1;

        // Character Move Animation Set
        if (Mathf.Abs(rigid.velocity.x) < 0.3)     // Mathf : 수학 관련 함수를 제공하는 클래스(Abs : 절대값)
            ani.SetBool("isWalking", false);    // SetBool : Animator Parameters 매개변수가 bool인 경우 주는 값
        else
            ani.SetBool("isWalking", true);
    }

    void FixedUpdate()  // 물리기반 Update
    {
        // 캐릭터가 데굴데굴 구를경우 Rigidbody에서 Constraints에 Freeze Rotation Z를 체크해줘야 한다.
        // RayCast : Object 검색을 위해 Ray를 쏘는 방식(일종의 레이저)

        // KeyControl By Move Speed
        float x = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * x, ForceMode2D.Impulse);

        // velocity : 현재속도
        if(rigid.velocity.x > maxSpeed)   // Right Max Speed
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if(rigid.velocity.x < maxSpeed*(-1))   // Left Max Speed
            rigid.velocity = new Vector2(maxSpeed*(-1), rigid.velocity.y);
     
        // Landing Player
        Debug.DrawRay(rigid.position, Vector3.down, new Color(1, 0, 0));   // DrawRay(쏘는 위치, 쏘는 방향, 색깔) : 에디터 상에서만 Ray를 그려주는 함수

        // RaycastHit : Ray에 닿은 오브젝트, RaycastHit 변수의 콜라이더로 검색 확인이 가능하다.
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));    // Raycast(쏘는 위치, 쏘는 방향, 쏘는 거리, 닿을 Layer 선택)

        if(rayHit.collider != null)  // rayHit는 물리기반이므로 collider가 들어간다, 관통되지 않는다.
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
        sRender.color = new Color(1, 1, 1, 0.4f);   // Color의 4번째 값은 투명도를 나타낸다.

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
