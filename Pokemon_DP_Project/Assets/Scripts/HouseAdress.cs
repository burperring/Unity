using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseAdress : MonoBehaviour
{
    public int stage;
    public int count;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        gameManager.stage = stage;
        gameManager.count = count;
    }
}
