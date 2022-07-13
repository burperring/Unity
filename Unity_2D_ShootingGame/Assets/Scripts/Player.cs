using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchLeft;
    public bool isTouchRight;

    public int life;
    public int score;

    public int boomCount;
    public int maxBoomCount;
    public float maxBoomDelay;  // 최대
    public float curBoomDelay;  // 현재
    public int power;
    public int maxPower;
    public float speed;
    public float maxShotDelay;  // 최대
    public float curShotDelay;  // 현재
    public float maxLifeDelay;  // 최대
    public float curLifeDelay;  // 현재

    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject boomEffect;

    public GameManager gameManager;
    public ObjectManager objectManager;

    public GameObject[] followers;

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        gameManager.UpdateBoomIcon(boomCount);
    }

    void Update()
    {
        // Encapsulation(캡슐화)
        Move();
        Fire();
        Boom();
        Reload();

        if (curLifeDelay < maxLifeDelay)
            anim.SetBool("isDmg", true);
        else if (curLifeDelay >= maxLifeDelay)
            anim.SetBool("isDmg", false);
    }

    void Move()
    {
        // Block Left Rignt Border
        float h = Input.GetAxisRaw("Horizontal");
        if ((isTouchRight && h == 1) || (isTouchLeft && h == -1))
            h = 0;

        // Block Top Bottom Border
        float v = Input.GetAxisRaw("Vertical");
        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1))
            v = 0;

        // Player Move
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;    // transform 이동에는 deltaTime 꼭 사용하기
        transform.position = curPos + nextPos;

        // Player Animation
        if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
        {
            anim.SetInteger("Input", (int)h);
        }
    }

    void Fire()
    {
        if (!Input.GetButton("Fire1"))
            return;

        // maxDelay까지 기다리지 않았다면 Fire를 실행하지 않는다.
        if (curShotDelay < maxShotDelay)
            return;

        switch (power)
        {
            case 1: // Power One
                GameObject bullet1 = objectManager.MakeObj("BulletPlayerA");
                bullet1.transform.position = transform.position;

                Rigidbody2D rigid1 = bullet1.GetComponent<Rigidbody2D>();
                rigid1.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 2: // Power two
                GameObject bullet2R = objectManager.MakeObj("BulletPlayerA");
                GameObject bullet2L = objectManager.MakeObj("BulletPlayerA");
                bullet2R.transform.position = transform.position + Vector3.right * 0.1f;
                bullet2L.transform.position = transform.position + Vector3.left * 0.1f;

                Rigidbody2D rigid2R = bullet2R.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid2L = bullet2L.GetComponent<Rigidbody2D>();
                rigid2R.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigid2L.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 3: // Power three
                GameObject bullet3R = objectManager.MakeObj("BulletPlayerA");
                GameObject bullet3C = objectManager.MakeObj("BulletPlayerA");
                GameObject bullet3L = objectManager.MakeObj("BulletPlayerA");
                bullet3R.transform.position = transform.position + Vector3.right * 0.2f;
                bullet3C.transform.position = transform.position + Vector3.up * 0.1f;
                bullet3L.transform.position = transform.position + Vector3.left * 0.2f;

                Rigidbody2D rigid3R = bullet3R.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid3C = bullet3C.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid3L = bullet3L.GetComponent<Rigidbody2D>();
                rigid3R.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigid3C.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigid3L.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            default: // Power four
                GameObject bullet4R = objectManager.MakeObj("BulletPlayerA");
                GameObject bullet4C = objectManager.MakeObj("BulletPlayerB");
                GameObject bullet4L = objectManager.MakeObj("BulletPlayerA");
                bullet4R.transform.position = transform.position + Vector3.right * 0.3f;
                bullet4C.transform.position = transform.position;
                bullet4L.transform.position = transform.position + Vector3.left * 0.3f;

                Rigidbody2D rigid4R = bullet4R.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid4C = bullet4C.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid4L = bullet4L.GetComponent<Rigidbody2D>();
                rigid4R.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigid4C.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigid4L.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
        }

        curShotDelay = 0;
    }

    void Boom()
    {
        if (!Input.GetButton("Fire2") || boomCount <= 0)
            return;

        // maxDelay까지 기다리지 않았다면 Boom을 실행하지 않는다.
        if (curBoomDelay < maxBoomDelay)
            return;

        boomCount--;
        gameManager.UpdateBoomIcon(boomCount);

        // Effect Visible
        boomEffect.SetActive(true);
        Invoke("OffBoomEffect", 2f);

        // Remove Enemy
        GameObject[] enemieL = objectManager.GetPool("EnemyL");
        GameObject[] enemieM = objectManager.GetPool("EnemyM");
        GameObject[] enemieS = objectManager.GetPool("EnemyS");

        for (int i = 0; i < enemieL.Length; i++)
        {
            if (enemieL[i].activeSelf) {
                Enemy enemyLogic = enemieL[i].GetComponent<Enemy>();
                enemyLogic.OnHit(1000);
            }
        }
        for (int i = 0; i < enemieM.Length; i++)
        {
            if (enemieM[i].activeSelf) {
                Enemy enemyLogic = enemieM[i].GetComponent<Enemy>();
                enemyLogic.OnHit(1000);
            }
        }
        for (int i = 0; i < enemieS.Length; i++)
        {
            if (enemieS[i].activeSelf) {
                Enemy enemyLogic = enemieS[i].GetComponent<Enemy>();
                enemyLogic.OnHit(1000);
            }
        }

        // Remove Enemy Bullet
        GameObject[] enemyBulletA = objectManager.GetPool("BulletEnemyA");
        GameObject[] enemyBulletB = objectManager.GetPool("BulletEnemyB");
        GameObject[] bossBulletA = objectManager.GetPool("BulletBossA");
        GameObject[] bossBulletB = objectManager.GetPool("BulletBossB");

        for (int i = 0; i < enemyBulletA.Length; i++) {
            if(enemyBulletA[i].activeSelf)
                enemyBulletA[i].SetActive(false);
        }
        for (int i = 0; i < enemyBulletB.Length; i++){
            if (enemyBulletB[i].activeSelf)
                enemyBulletB[i].SetActive(false);
        }
        for (int i = 0; i < bossBulletA.Length; i++){
            if (bossBulletA[i].activeSelf)
                bossBulletA[i].SetActive(false);
        }
        for (int i = 0; i < bossBulletB.Length; i++){
            if (bossBulletB[i].activeSelf)
            {
                bossBulletB[i].SetActive(false);
                bossBulletB[i].transform.rotation = Quaternion.identity;
            }
        }

        curBoomDelay = 0;
    }

    void Reload()
    {
        // 실시간 Delay 추가
        curShotDelay += Time.deltaTime;
        curLifeDelay += Time.deltaTime;
        curBoomDelay += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Player State
        if(collision.gameObject.tag == "Border")
        {
            switch(collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = true;
                    break;
                case "Bottom":
                    isTouchBottom = true;
                    break;
                case "Left":
                    isTouchLeft = true;
                    break;
                case "Right":
                    isTouchRight = true;
                    break;
            }
        }
        else if ((collision.gameObject.tag == "Enemy") || (collision.gameObject.tag == "EnemyBullet"))
        {
            // Player Invincibility(무적) Time
            if (curLifeDelay < maxLifeDelay)
                return;

            // Player Hit
            life--;
            gameManager.UpdateLifeIcon(life);
            gameManager.CallExplosion(transform.position, "P");

            if(life == 0)
            {
                gameManager.GameOver();
            }
            else
            {
                // Respawn Player
                gameManager.RespawnPlayer();
            }

            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
            curLifeDelay = 0;
        }
        else if(collision.gameObject.tag == "Item")
        {
            Item item = collision.GetComponent<Item>();

            switch(item.type)
            {
                case "Coin":
                    score += 50;
                    break;
                case "Power":
                    if (power == maxPower)
                        score += 300;
                    else {
                        power++;
                        AddFollower();
                    }
                    break;
                case "Boom":
                    if (boomCount == maxBoomCount)
                        score += 300;
                    else
                    {
                        boomCount++;
                        gameManager.UpdateBoomIcon(boomCount);
                    }
                    break;
            }

            // Eat Item
            collision.gameObject.SetActive(false);
        }
    }

    void AddFollower()
    {
        if (power == 5)
            followers[0].SetActive(true);
        if (power == 6)
            followers[1].SetActive(true);
        if (power == 7)
            followers[2].SetActive(true);
    }

    void OffBoomEffect()
    {
        boomEffect.SetActive(false);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = false;
                    break;
                case "Bottom":
                    isTouchBottom = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;
            }
        }
    }
}
