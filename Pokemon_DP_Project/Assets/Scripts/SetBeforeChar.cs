using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBeforeChar : MonoBehaviour
{
    public GameObject getNpc;

    private Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.beforeNpc = getNpc;
    }
}
