using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    void Update()
    {
        // �̻��� ȸ��
        transform.Rotate(Vector3.right * 50 * Time.deltaTime);
    }
}
