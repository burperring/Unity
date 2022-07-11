using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject enemyBPrefab;
    public GameObject enemyLPrefab;
    public GameObject enemyMPrefab;
    public GameObject enemySPrefab;
    public GameObject itemCoinPrefab;
    public GameObject itemPowerPrefab;
    public GameObject itemBoomPrefab;
    public GameObject bulletPlayerAPrefab;
    public GameObject bulletPlayerBPrefab;
    public GameObject bulletEnemyAPrefab;
    public GameObject bulletEnemyBPrefab;
    public GameObject bulletBossAPrefab;
    public GameObject bulletBossBPrefab;
    public GameObject bulletFollowerPrefab;

    GameObject[] enemyB;
    GameObject[] enemyL;
    GameObject[] enemyM;
    GameObject[] enemyS;

    GameObject[] itemCoin; 
    GameObject[] itemPower;
    GameObject[] itemBoom;

    GameObject[] bulletPlayerA;
    GameObject[] bulletPlayerB;
    GameObject[] bulletEnemyA;
    GameObject[] bulletEnemyB;
    GameObject[] bulletBossA;
    GameObject[] bulletBossB;
    GameObject[] bulletFollower;

    GameObject[] targetPool;

    void Awake()
    {
        // Create Object Pool
        enemyB = new GameObject[1];
        enemyL = new GameObject[10];
        enemyM = new GameObject[10];
        enemyS = new GameObject[20];

        itemCoin = new GameObject[20];
        itemPower = new GameObject[10];
        itemBoom = new GameObject[10];

        bulletPlayerA = new GameObject[100];
        bulletPlayerB = new GameObject[100];
        bulletEnemyA = new GameObject[100];
        bulletEnemyB = new GameObject[100];
        bulletBossA = new GameObject[100];
        bulletBossB = new GameObject[1000];
        bulletFollower = new GameObject[100];

        Generate();
    }

    void Generate()
    {
        // Enemy Generate
        for (int i = 0; i < enemyB.Length; i++)
        {
            enemyB[i] = Instantiate(enemyBPrefab);
            enemyB[i].SetActive(false);
        }

        for (int i = 0; i < enemyL.Length; i++){
            enemyL[i] = Instantiate(enemyLPrefab);
            enemyL[i].SetActive(false);
        }

        for (int i = 0; i < enemyM.Length; i++){
            enemyM[i] = Instantiate(enemyMPrefab);
            enemyM[i].SetActive(false);
        }

        for (int i = 0; i < enemyS.Length; i++){
            enemyS[i] = Instantiate(enemySPrefab);
            enemyS[i].SetActive(false);
        }

        // Item Generate
        for (int i = 0; i < itemCoin.Length; i++){
            itemCoin[i] = Instantiate(itemCoinPrefab);
            itemCoin[i].SetActive(false);
        }

        for (int i = 0; i < itemPower.Length; i++){
            itemPower[i] = Instantiate(itemPowerPrefab);
            itemPower[i].SetActive(false);
        }

        for (int i = 0; i < itemBoom.Length; i++){
            itemBoom[i] = Instantiate(itemBoomPrefab);
            itemBoom[i].SetActive(false);
        }

        // Bullet Generate
        for (int i = 0; i < bulletPlayerA.Length; i++){
            bulletPlayerA[i] = Instantiate(bulletPlayerAPrefab);
            bulletPlayerA[i].SetActive(false);
        }

        for (int i = 0; i < bulletPlayerB.Length; i++){
            bulletPlayerB[i] = Instantiate(bulletPlayerBPrefab);
            bulletPlayerB[i].SetActive(false);
        }

        for (int i = 0; i < bulletEnemyA.Length; i++){
            bulletEnemyA[i] = Instantiate(bulletEnemyAPrefab);
            bulletEnemyA[i].SetActive(false);
        }

        for (int i = 0; i < bulletEnemyB.Length; i++){
            bulletEnemyB[i] = Instantiate(bulletEnemyBPrefab);
            bulletEnemyB[i].SetActive(false);
        }

        for (int i = 0; i < bulletBossA.Length; i++)
        {
            bulletBossA[i] = Instantiate(bulletBossAPrefab);
            bulletBossA[i].SetActive(false);
        }

        for (int i = 0; i < bulletBossB.Length; i++)
        {
            bulletBossB[i] = Instantiate(bulletBossBPrefab);
            bulletBossB[i].SetActive(false);
        }

        for (int i = 0; i < bulletFollower.Length; i++)
        {
            bulletFollower[i] = Instantiate(bulletFollowerPrefab);
            bulletFollower[i].SetActive(false);
        }
    }

    public GameObject MakeObj(string type)
    {
        switch(type)
        {
            case "EnemyB":
                targetPool = enemyB;
                break;
            case "EnemyL":
                targetPool = enemyL;
                break;
            case "EnemyM":
                targetPool = enemyM;
                break;
            case "EnemyS":
                targetPool = enemyS;
                break;
            case "ItemCoin":
                targetPool = itemCoin;
                break;
            case "ItemPower":
                targetPool = itemPower;
                break;
            case "ItemBoom":
                targetPool = itemBoom;
                break;
            case "BulletPlayerA":
                targetPool = bulletPlayerA;
                break;
            case "BulletPlayerB":
                targetPool = bulletPlayerB;
                break;
            case "BulletEnemyA":
                targetPool = bulletEnemyA;
                break;
            case "BulletEnemyB":
                targetPool = bulletEnemyB;
                break;
            case "BulletBossA":
                targetPool = bulletBossA;
                break;
            case "BulletBossB":
                targetPool = bulletBossB;
                break;
            case "BulletFollower":
                targetPool = bulletFollower;
                break;
        }

        for (int i = 0; i < targetPool.Length; i++) {
            if (!targetPool[i].activeSelf)
            {
                targetPool[i].SetActive(true);
                return targetPool[i];
            }
        }

        return null;
    }

    public GameObject[] GetPool(string type)
    {
        switch (type)
        {
            case "EnemyB":
                targetPool = enemyB;
                break;
            case "EnemyL":
                targetPool = enemyL;
                break;
            case "EnemyM":
                targetPool = enemyM;
                break;
            case "EnemyS":
                targetPool = enemyS;
                break;
            case "ItemCoin":
                targetPool = itemCoin;
                break;
            case "ItemPower":
                targetPool = itemPower;
                break;
            case "ItemBoom":
                targetPool = itemBoom;
                break;
            case "BulletPlayerA":
                targetPool = bulletPlayerA;
                break;
            case "BulletPlayerB":
                targetPool = bulletPlayerB;
                break;
            case "BulletEnemyA":
                targetPool = bulletEnemyA;
                break;
            case "BulletEnemyB":
                targetPool = bulletEnemyB;
                break;
            case "BulletBossA":
                targetPool = bulletBossA;
                break;
            case "BulletBossB":
                targetPool = bulletBossB;
                break;
            case "BulletFollower":
                targetPool = bulletFollower;
                break;
        }

        return targetPool;
    }
}
