using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnim : MonoBehaviour
{
    public DoorAnimTrigger doorTrigger;
    public HouseAdress houseAdress;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (houseAdress.stage == gameManager.beforeStage && houseAdress.count == gameManager.beforeCount)
        {
            doorTrigger.DoorClose();
            gameManager.beforeStage = 0;
            gameManager.beforeCount = 0;
        }
    }
}
