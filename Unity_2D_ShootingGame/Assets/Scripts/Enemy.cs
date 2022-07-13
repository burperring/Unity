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

    public ObjectManager objectManager;
    public GameManager gameManager;

    Animator anim;
    SpriteRenderer spriteRenderer;

    public int patternIndex;
    public int curPatternCount;
    public int[] maxPatternCount;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (enemyName == "B")
            anim = GetComponent<Animator>();
    }

    void OnEnable()     // 컴포넌트가 활성화 될 때 호출되는 생명주기함수
    {
        switch(enemyName)
        {
            case "B":
                health = 3000;
                Invoke("Stop", 2);
                break;
            case "L":
                health = 50;
                break;
            case "M":
                health = 10;
                break;
            case "S":
                health = 2;
                break;
        }
    }

    void Stop()
    {
        if (!gameObject.activeSelf)
            return;

        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;

        Invoke("Think", 2);
    }

    void Think()
    {
        patternIndex = patternIndex == 3 ? 1 : patternIndex + 1;
        curPatternCount = 0;

        switch (patternIndex)
        {
            case 0:
                FireForward();
                break;
            case 1:
                FireShot();
                break;
            case 2:
                FireArc();
                break;
            case 3:
                FireRound();
                break;
        }
    }

    void FireForward()
    {
        if (health <= 0)
            return;

        // Fire 5 Bullet Forward
        GameObject bulletR = objectManager.MakeObj("BulletBossA");
        GameObject bulletRR = objectManager.MakeObj("BulletBossA");
        GameObject bulletC = objectManager.MakeObj("BulletEnemyB");
        GameObject bulletL = objectManager.MakeObj("BulletBossA");
        GameObject bulletLL = objectManager.MakeObj("BulletBossA");
        bulletR.transform.position = transform.position + Vector3.right * 0.65f;
        bulletRR.transform.position = transform.position + Vector3.right * 0.85f;
        bulletC.transform.position = transform.position;
        bulletL.transform.position = transform.position + Vector3.left * 0.65f;
        bulletLL.transform.position = transform.position + Vector3.left * 0.85f;
        Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidC = bulletC.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();

        rigidR.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidRR.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidC.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidL.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidLL.AddForce(Vector2.down * 8, ForceMode2D.Impulse);

        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireForward", 1);
        else
            Invoke("Think", 3);
    }

    void FireShot()
    {
        if (health <= 0)
            return;

        // Fire ShotGun
        for (int i = 0; i < 5; i++)
        {
            GameObject bullet = objectManager.MakeObj("BulletEnemyB");
            bullet.transform.position = transform.position;
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            Vector2 dirVec = player.transform.position - transform.position;
            Vector2 ranVec = new Vector2(Random.Range(-1f, 1f), Random.Range(0f, 2f));
            dirVec += ranVec;
            rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);
        }

        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireShot", 1.5f);
        else
            Invoke("Think", 3);
    }

    void FireArc()
    {
        if (health <= 0)
            return;

        // Fire Arc Continue Fire
        GameObject bulletR = objectManager.MakeObj("BulletEnemyA");
        bulletR.transform.position = transform.position;
        bulletR.transform.rotation = Quaternion.identity;
        Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
        GameObject bulletRR = objectManager.MakeObj("BulletEnemyA");
        bulletRR.transform.position = transform.position;
        bulletRR.transform.rotation = Quaternion.identity;
        Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
        GameObject bulletL = objectManager.MakeObj("BulletEnemyA");
        bulletL.transform.position = transform.position;
        bulletL.transform.rotation = Quaternion.identity;
        Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
        GameObject bulletLL = objectManager.MakeObj("BulletEnemyA");
        bulletLL.transform.position = transform.position;
        bulletLL.transform.rotation = Quaternion.identity;
        Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();

        Vector2 dirVecR = new Vector2(Mathf.Sin(Mathf.PI * 3 * curPatternCount/maxPatternCount[patternIndex]), -1);
        rigidR.AddForce(dirVecR.normalized * 5, ForceMode2D.Impulse);
        Vector2 dirVecRR = new Vector2(Mathf.Sin(Mathf.PI * 10 * curPatternCount / maxPatternCount[patternIndex]), -1);
        rigidRR.AddForce(dirVecRR.normalized * 5, ForceMode2D.Impulse);
        Vector2 dirVecL = new Vector2(Mathf.Sin(Mathf.PI * -3 * curPatternCount / maxPatternCount[patternIndex]), -1);
        rigidL.AddForce(dirVecL.normalized * 5, ForceMode2D.Impulse);
        Vector2 dirVecLL = new Vector2(Mathf.Sin(Mathf.PI * -10 * curPatternCount / maxPatternCount[patternIndex]), -1);
        rigidLL.AddForce(dirVecLL.normalized * 5, ForceMode2D.Impulse);

        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireArc", 0.15f);
        else
            Invoke("Think", 3);
    }

    void FireRound()
    {
        if (health <= 0)
            return;

        // Fire Around
        int roundNumA = 35;
        int roundNumB = 45;
        int roundNum = curPatternCount % 2 == 0 ? roundNumA : roundNumB;

        for (int i = 0; i < roundNum; i++)
        {
            GameObject bullet = objectManager.MakeObj("BulletBossB");
            bullet.transform.position = transform.position;
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / roundNum), Mathf.Sin(Mathf.PI * 2 * i / roundNum));
            rigid.AddForce(dirVec.normalized * 1, ForceMode2D.Impulse);

            Vector3 rotVec = Vector3.forward * 360 * i / roundNum + Vector3.forward * 90;
            bullet.transform.Rotate(rotVec);
        }

        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireRound", 0.7f);
        else
            Invoke("Think", 10);
    }

    void Update()
    {
        if (enemyName == "B")
            return;

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
            GameObject bullet = objectManager.MakeObj("BulletEnemyA");
            bullet.transform.position = transform.position;
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            // Target(player) Vector = Target Position - Enemy Position
            Vector3 dirVec = player.transform.position - transform.position;
            rigid.AddForce(dirVec.normalized * 6, ForceMode2D.Impulse);
        }
        else if(enemyName == "L")
        {
            GameObject bulletR = objectManager.MakeObj("BulletEnemyB");
            GameObject bulletL = objectManager.MakeObj("BulletEnemyB");
            bulletR.transform.position = transform.position + Vector3.right * 0.3f;
            bulletL.transform.position = transform.position + Vector3.left * 0.3f;
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
        if(enemyName == "B")
        {
            anim.SetTrigger("OnHit");
        }
        else
        {
            spriteRenderer.sprite = sprites[1];
            Invoke("ReturnSprite", 0.1f);
        }

        if(health <= 0)
        {
            // Player Get Score
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += enemyScore;

            // Random Ratio Item Drop
            int ran = enemyName == "B" ? 19 : Random.Range(0, 20);
            if(ran < 5)         // Coin
            {
                GameObject itemCoin = objectManager.MakeObj("ItemCoin");
                itemCoin.transform.position = transform.position;
            }
            else if(ran < 7)    // Power
            {
                GameObject itemPower = objectManager.MakeObj("ItemPower");
                itemPower.transform.position = transform.position;
            }
            else if(ran < 8)    // Boom
            {
                GameObject itemBoom = objectManager.MakeObj("ItemBoom");
                itemBoom.transform.position = transform.position;
            }
            else
            {
                Debug.Log("Not Item");
            }

            // Delete Boss Bullet
            if (enemyName == "B")
            {
                GameObject[] enemyBulletA = objectManager.GetPool("BulletEnemyA");
                GameObject[] enemyBulletB = objectManager.GetPool("BulletEnemyB");
                GameObject[] bossBulletA = objectManager.GetPool("BulletBossA");
                GameObject[] bossBulletB = objectManager.GetPool("BulletBossB");

                for (int i = 0; i < enemyBulletA.Length; i++)
                {
                    if (enemyBulletA[i].activeSelf)
                        enemyBulletA[i].SetActive(false);
                }
                for (int i = 0; i < enemyBulletB.Length; i++)
                {
                    if (enemyBulletB[i].activeSelf)
                        enemyBulletB[i].SetActive(false);
                }
                for (int i = 0; i < bossBulletA.Length; i++)
                {
                    if (bossBulletA[i].activeSelf)
                        bossBulletA[i].SetActive(false);
                }
                for (int i = 0; i < bossBulletB.Length; i++)
                {
                    if (bossBulletB[i].activeSelf)
                        bossBulletB[i].SetActive(false);
                }
            }

            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;   // 기본ㄴ 회전값 = 0으로 되돌려 놓기
            gameManager.CallExplosion(transform.position, enemyName);

            // Boss Kill
            if(enemyName == "B")
                gameManager.StageEnd();
        }
    }

    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderBullet" && enemyName != "B") {
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }
        else if(collision.gameObject.tag == "PlayerBullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);

            // Destroy Player Bullet
            collision.gameObject.SetActive(false);
        }
    }
}
