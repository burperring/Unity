using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName;
    public Transform target;

    private GameManager gameManager;
    private Player player;

    float v;
    
    void Start()
    {
        player = FindObjectOfType<Player>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        v = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            player.currentMapName = transferMapName;

            if (gameObject.tag == "StairDown")
            {
                SceneManager.LoadScene("Stage " + gameManager.stage + "-" + (gameManager.count - 1));
                gameManager.count--;
                player.transform.position = new Vector3(target.position.x, target.position.y, target.position.z);
                player.transform.position = target.position;
            }
            else if (gameObject.tag == "StairUp")
            {
                SceneManager.LoadScene("Stage " + gameManager.stage + "-" + (gameManager.count + 1));
                gameManager.count++;
                player.transform.position = new Vector3(target.position.x, target.position.y, target.position.z);
                player.transform.position = target.position;
            }
            else if (gameObject.tag == "DoorOut" && v == -1)
            {
                SceneManager.LoadScene("Stage " + (gameManager.stage - 1) + "-0");
                gameManager.beforeStage = gameManager.stage;
                gameManager.beforeCount = gameManager.count;
                gameManager.stage--;
                gameManager.count = 0;
                player.transform.position = new Vector3(target.position.x, target.position.y, target.position.z);
                player.transform.position = target.position;
                player.isInHouse = false;
            }
            else if (gameObject.tag == "DoorIn" && v == 1)
            {
                if (player.isBike)
                    return;

                SceneManager.LoadScene("Stage " + gameManager.stage + "-" + gameManager.count);
                player.transform.position = new Vector3(target.position.x, target.position.y, target.position.z);
                player.transform.position = target.position;
                player.isInHouse = true;
            }
        }
    }
}
