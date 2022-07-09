using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;        // 파일을 읽기 위해 사용

public class GameManager : MonoBehaviour
{
    public string[] enemyObjs;
    public Transform[] spawnPoints;

    public float nextSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;
    public GameObject gameoverSet;
    public Text scoreText;
    public Image[] lifeImage;
    public Image[] boomImage;
    public ObjectManager objectManager;

    public List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;

    public int StageCount;

    Animator anim;

    void Awake()
    {
        spawnList = new List<Spawn>();
        enemyObjs = new string[] { "EnemyS", "EnemyM", "EnemyL" };
        //ReadSpawnFile();
    }

    void ReadSpawnFile()
    {
        // 1. 변수 초기화
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        // 2. Read Spawn File
        TextAsset textFile = Resources.Load("Stage " + StageCount) as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        while(stringReader != null)
        {
            string line = stringReader.ReadLine();

            if (line == null)
                break;

            // 3. Create Spawn Data
            Spawn spawnData = new Spawn();
            spawnData.delay = float.Parse(line.Split(',')[0]);
            spawnData.type = line.Split(',')[1];
            spawnData.point = int.Parse(line.Split(',')[2]);
            spawnList.Add(spawnData);
        }

        // 4. File Close
        stringReader.Close();
        StageCount++;

        // 5. Apply First Spawn Delay
        nextSpawnDelay = spawnList[0].delay;
    }

    void Update()
    {
        if (spawnEnd)
            ReadSpawnFile();

        curSpawnDelay += Time.deltaTime;

        if (curSpawnDelay > nextSpawnDelay && !spawnEnd)
        {
            SpawnEnemy();
            curSpawnDelay = 0;
        }

        // UI Score Update
        Player playerLogic = player.GetComponent<Player>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score);
    }

    void SpawnEnemy()
    {
        int enemyIndex = 0;
        switch(spawnList[spawnIndex].type)
        {
            case "S":
                enemyIndex = 0;
                break;
            case "M":
                enemyIndex = 1;
                break;
            case "L":
                enemyIndex = 2;
                break;
            case "LS":
                enemyIndex = 0;
                Player playerLogicS = player.GetComponent<Player>();
                if(playerLogicS.life < 3)
                    playerLogicS.life++;
                UpdateLifeIcon(playerLogicS.life);
                break;
            case "LM":
                enemyIndex = 1;
                Player playerLogicM = player.GetComponent<Player>();
                if (playerLogicM.life < 3)
                    playerLogicM.life++;
                UpdateLifeIcon(playerLogicM.life);
                break;
            case "LL":
                enemyIndex = 2;
                Player playerLogicL = player.GetComponent<Player>();
                if (playerLogicL.life < 3)
                    playerLogicL.life++;
                UpdateLifeIcon(playerLogicL.life);
                break;
        }

        int enemyPoint = spawnList[spawnIndex].point;
        GameObject enemy = objectManager.MakeObj(enemyObjs[enemyIndex]);
        enemy.transform.position = spawnPoints[enemyPoint].position;

        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        // 생성되지 않은 오브젝트가 생성된 player에 접근할 수 없다. 그러므로 GameManager를 통해 생성과 동시에 player 정보를 제공하는 방식을 사용한다.
        enemyLogic.player = player;
        enemyLogic.objectManager = objectManager;

        // Enemy Spawn
        if (enemyPoint == 6 || enemyPoint == 8)         // Right
        {
            enemy.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), -1);
        }
        else if (enemyPoint == 5 || enemyPoint == 7)    // Left
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enemyLogic.speed, -1);
        }
        else
        {                                       // Front
            rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));
        }

        // Respawn Index ++
        spawnIndex++;
        if(spawnIndex == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }

        // Next Spawn Delay Renewal(갱신)
        nextSpawnDelay = spawnList[spawnIndex].delay;
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

    public void UpdateBoomIcon(int Boom)
    {
        // UI Life Init Disable
        for (int i = 0; i < 4; i++)
        {
            boomImage[i].color = new Color(1, 1, 1, 0);
        }

        // UI Life Active
        for (int i = 0; i < Boom; i++)
        {
            boomImage[i].color = new Color(1, 1, 1, 1);
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
