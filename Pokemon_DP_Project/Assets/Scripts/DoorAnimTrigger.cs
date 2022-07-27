using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimTrigger : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void DoorOpen()
    {
        anim.SetTrigger("onDoorOpen");
    }

    public void DoorClose()
    {
        anim.SetTrigger("onDoorClose");
    }

    public void Door()
    {
        anim.SetTrigger("onDoor");
    }
}
