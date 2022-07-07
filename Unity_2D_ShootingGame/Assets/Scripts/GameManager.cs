using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyObjs;
    public Transform[] spawnPoints;

    public float maxSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;
    public GameObject gameoverSet;
    public Text scoreText;
    public Image[] lifeImage;

    Animator anim;

    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if (curSpawnDelay > maxSpawnDelay)
        {
            SpawnEnemy();
            maxSpawnDelay = Random.Range(0.5f, 3f);
            curSpawnDelay = 0;
        }

        // UI Score Update
        Player playerLogic = player.GetComponent<Player>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score);
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
        // 생성되지 않은 오브젝트가 생성된 player에 접근할 수 없다. 그러므로 GameManager를 통해 생성과 동시에 player 정보를 제공하는 방식을 사용한다.
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

    public void UpdateLifeIcon(int life)
    {
        // UI Life Init Disable
        for(int i = 0; i < 3; i++)
        {
            lifeImage[i].color = new Color(1, 1, 1, 0);
        }

        // UI Life Active
        for (int i = 0; i < life; i++)
        {
            lifeImage[i].color = new Color(1, 1, 1, 1);
        }
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

    public void GameOver()
    {
        gameoverSet.SetActive(true);
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }
}
