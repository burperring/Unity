using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseAdress : MonoBehaviour
{
    public GameManager gameManager;
    public Animator anim;

    public int stage;
    public int count;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        gameManager.stage = stage;
        gameManager.count = count;

        if(gameManager.beforeStage == stage && gameManager.beforeCount == count)
        {
            anim.SetTrigger("onTrigger");

            gameManager.stage = 0;
            gameManager.count = 0;
        }
    }
}
