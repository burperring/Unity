using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public GameManager gameManager;
    public string transferMapName;
    public Transform target;

    private Player player;
    
    void Start()
    {
        player = FindObjectOfType<Player>();   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            player.currentMapName = transferMapName;

            if (gameObject.tag == "StairDown")
            {
                SceneManager.LoadScene("Stage " + gameManager.stage + "-" + (gameManager.floor - 1));
                player.transform.position = new Vector3(target.position.x, target.position.y, target.position.z);
                player.transform.position = target.position;
            }
            else if (gameObject.tag == "StairUp")
            {
                SceneManager.LoadScene("Stage " + gameManager.stage + "-" + (gameManager.floor + 1));
                player.transform.position = new Vector3(target.position.x, target.position.y, target.position.z);
                player.transform.position = target.position;
            }
        }
    }
}
