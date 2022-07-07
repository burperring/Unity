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

    public GameManager manager;

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        manager.UpdateBoomIcon(boomCount);
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
                GameObject bullet1 = Instantiate(bulletObjA, transform.position, transform.rotation);
                Rigidbody2D rigid1 = bullet1.GetComponent<Rigidbody2D>();
                rigid1.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 2: // Power two
                GameObject bullet2R = Instantiate(bulletObjA, transform.position + Vector3.right * 0.1f, transform.rotation);
                GameObject bullet2L = Instantiate(bulletObjA, transform.position + Vector3.left * 0.1f, transform.rotation);
                Rigidbody2D rigid2R = bullet2R.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid2L = bullet2L.GetComponent<Rigidbody2D>();
                rigid2R.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigid2L.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 3: // Power three
                GameObject bullet3R = Instantiate(bulletObjA, transform.position + Vector3.right * 0.2f, transform.rotation);
                GameObject bullet3C = Instantiate(bulletObjA, transform.position + Vector3.up * 0.1f, transform.rotation);
                GameObject bullet3L = Instantiate(bulletObjA, transform.position + Vector3.left * 0.2f, transform.rotation);
                Rigidbody2D rigid3R = bullet3R.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid3C = bullet3C.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid3L = bullet3L.GetComponent<Rigidbody2D>();
                rigid3R.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigid3C.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigid3L.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 4: // Power four
                GameObject bullet4R = Instantiate(bulletObjA, transform.position + Vector3.right * 0.3f, transform.rotation);
                GameObject bullet4C = Instantiate(bulletObjB, transform.position, transform.rotation);
                GameObject bullet4L = Instantiate(bulletObjA, transform.position + Vector3.left * 0.3f, transform.rotation);
                Rigidbody2D rigid4R = bullet4R.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid4C = bullet4C.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid4L = bullet4L.GetComponent<Rigidbody2D>();
                rigid4R.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigid4C.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigid4L.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 5: // Power five
                GameObject bullet5R = Instantiate(bulletObjB, transform.position + Vector3.right * 0.23f, transform.rotation);
                GameObject bullet5RS = Instantiate(bulletObjA, transform.position + Vector3.right * 0.6f, transform.rotation);
                GameObject bullet5L = Instantiate(bulletObjB, transform.position + Vector3.left * 0.23f, transform.rotation);
                GameObject bullet5LS = Instantiate(bulletObjA, transform.position + Vector3.left * 0.6f, transform.rotation);
                Rigidbody2D rigid5R = bullet5R.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid5RS = bullet5RS.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid5L = bullet5L.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid5LS = bullet5LS.GetComponent<Rigidbody2D>();
                rigid5R.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigid5RS.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigid5L.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigid5LS.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 6: // Power six
                GameObject bullet6R = Instantiate(bulletObjB, transform.position + Vector3.right * 0.75f, transform.rotation);
                GameObject bullet6CR = Instantiate(bulletObjB, transform.position + Vector3.right * 0.23f + Vector3.up * 0.2f, transform.rotation);
                GameObject bullet6CL = Instantiate(bulletObjB, transform.position + Vector3.left * 0.23f + Vector3.up * 0.2f, transform.rotation);
                GameObject bullet6L = Instantiate(bulletObjB, transform.position + Vector3.left * 0.75f, transform.rotation);
                Rigidbody2D rigid6R = bullet6R.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid6CR = bullet6CR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid6CL = bullet6CL.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid6L = bullet6L.GetComponent<Rigidbody2D>();
                rigid6R.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigid6CR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigid6CL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigid6L.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
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
        manager.UpdateBoomIcon(boomCount);

        // Effect Visible
        boomEffect.SetActive(true);
        Invoke("OffBoomEffect", 2f);

        // Remove Enemy
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemyLogic = enemies[i].GetComponent<Enemy>();
            enemyLogic.OnHit(1000);
        }

        // Remove Enemy Bullet
        GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        for (int i = 0; i < enemyBullets.Length; i++)
        {
            Destroy(enemyBullets[i]);
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
            manager.UpdateLifeIcon(life);

            if(life == 0)
            {
                manager.GameOver();
            }
            else
            {
                // Respawn Player
                manager.RespawnPlayer();
            }

            gameObject.SetActive(false);
            Destroy(collision.gameObject);
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
                    else
                        power++;
                    break;
                case "Boom":
                    if (boomCount == maxBoomCount)
                        score += 300;
                    else
                    {
                        boomCount++;
                        manager.UpdateBoomIcon(boomCount);
                    }
                    break;
            }
            // Eat Item
            Destroy(collision.gameObject);
        }
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
