using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    public float speed;
    public int health;
    public int enemyScore;

    public float maxShotDelay;  // 최대
    public float curShotDelay;  // 현재

    public Sprite[] sprites;
    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject itemCoin;
    public GameObject itemPower;
    public GameObject itemBoom;
    public GameObject player;

    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Fire();
        Reload();
    }

    void Fire()
    {
        // maxDelay까지 기다리지 않았다면 Fire를 실행하지 않는다.
        if (curShotDelay < maxShotDelay)
            return;

        if(enemyName == "S")
        {
            GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            // Target(player) Vector = Target Position - Enemy Position
            Vector3 dirVec = player.transform.position - transform.position;
            rigid.AddForce(dirVec.normalized * 6, ForceMode2D.Impulse);
        }
        else if(enemyName == "L")
        {
            GameObject bulletR = Instantiate(bulletObjB, transform.position + Vector3.right * 0.3f, transform.rotation);
            GameObject bulletL = Instantiate(bulletObjB, transform.position + Vector3.left * 0.3f, transform.rotation);
            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();

            // Target(player) Vector = Target Position - Enemy Position
            Vector3 dirVecR = player.transform.position - (transform.position + Vector3.right * 0.3f);
            Vector3 dirVecL = player.transform.position - (transform.position + Vector3.left * 0.3f);

            rigidR.AddForce(dirVecR.normalized * 4, ForceMode2D.Impulse);
            rigidL.AddForce(dirVecL.normalized * 4, ForceMode2D.Impulse);
        }

        curShotDelay = 0;
    }

    void Reload()
    {
        // 실시간 Delay 추가
        curShotDelay += Time.deltaTime;
    }

    public void OnHit(int dmg)
    {
        if (health <= 0)
            return;

        health -= dmg;
        spriteRenderer.sprite = sprites[1];
        Invoke("ReturnSprite", 0.1f);

        if(health <= 0)
        {
            // Player Get Score
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += enemyScore;

            // Random Ratio Item Drop
            int ran = Random.Range(0, 20);
            if(ran < 5)         // Coin
            {
                Instantiate(itemCoin, transform.position, itemCoin.transform.rotation);
            }
            else if(ran < 7)    // Power
            {
                Instantiate(itemPower, transform.position, itemCoin.transform.rotation);
            }
            else if(ran < 8)    // Boom
            {
                Instantiate(itemBoom, transform.position, itemCoin.transform.rotation);
            }
            else
            {
                Debug.Log("Not Item");
            }

            Destroy(gameObject);
        }
    }

    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderBullet")
            Destroy(gameObject);
        else if(collision.gameObject.tag == "PlayerBullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);

            // Destroy Player Bullet
            Destroy(collision.gameObject);
        }
    }
}
