using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    void Update()
    {
        // 미사일 회전
        transform.Rotate(Vector3.right * 50 * Time.deltaTime);
    }
}
