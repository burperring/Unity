using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcQuestMove : MonoBehaviour
{
    public bool isNpcTransY;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter!!");
    }
}
