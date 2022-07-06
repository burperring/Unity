using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyObjs;
    public Transform[] spawnPoints;

    public float maxSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;

    Animator anim;

    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if(curSpawnDelay > maxSpawnDelay)
        {
            SpawnEnemy();
            maxSpawnDelay = Random.Range(0.5f,3f);
            curSpawnDelay = 0;
        }
    }

    void SpawnEnemy()
    {
        // Random Enemy
        int ranEnemy = Random.Range(0, 3);
        int ranPoint = Random.Range(0, 9);
        GameObject enemy = Instantiate(enemyObjs[ranEnemy], 
                                       spawnPoints[ranPoint].position, 
                                       spawnPoints[ranPoint].rotation);

        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        // �������� ���� ������Ʈ�� ������ player�� ������ �� ����. �׷��Ƿ� GameManager�� ���� ������ ���ÿ� player ������ �����ϴ� ����� ����Ѵ�.
        enemyLogic.player = player;     

        // Enemy Spawn
        if (ranPoint == 5 || ranPoint == 7)         // Right
        {
            enemy.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), -1);
        }
        else if (ranPoint == 6 || ranPoint == 8)    // Left
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enemyLogic.speed, -1);
        }
        else                                        // Front
            rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));
    }

    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExe", 2);
    }

    void RespawnPlayerExe()
    {
        // Player Respawn Point
        player.transform.position = Vector3.down * 4;
        player.SetActive(true);
    }
}
