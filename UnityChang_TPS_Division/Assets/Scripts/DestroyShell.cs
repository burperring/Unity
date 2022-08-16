using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyShell : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (this.gameObject.tag == "BulletShell")
            Destroy(gameObject, 7f);
    }
}
