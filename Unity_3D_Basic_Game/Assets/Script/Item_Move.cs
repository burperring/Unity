using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Move : MonoBehaviour
{
    public float rotateSpeed;

    void Update()
    {
        // Rotate(Vector3) : �Ű����� �������� ȸ����Ű�� �Լ�
        // Space.World : ���� ��ǥ�踦 �������� ȸ����Ų��.
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {

    }
}
