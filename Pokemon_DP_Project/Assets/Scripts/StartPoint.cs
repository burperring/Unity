using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public string startPoint;

    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();

        if(startPoint == player.currentMapName)
        {
            player.transform.position = this.transform.position;
        }
    }
}
