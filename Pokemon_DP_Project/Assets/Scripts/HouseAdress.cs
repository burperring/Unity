using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseAdress : MonoBehaviour
{
    public GameManager gameManager;

    public int stage;
    public int count;

    private void OnTriggerEnter(Collider other)
    {
        gameManager.stage = stage;
        gameManager.count = count;
    }
}
