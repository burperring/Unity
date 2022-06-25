using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Move : MonoBehaviour
{
    Transform playerTransform;
    Vector3 Offset;

    void Awake()
    {
        // Player tag�� ���� Object�� �����´�.
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // ���� ������ ī�޶�� Player���� �Ÿ� ���ϱ�
        Offset = transform.position - playerTransform.position;
    }

    void LateUpdate()
    {
        // ���� ������ ī�޶�� Player���� �Ÿ���ŭ ���ϱ�
        transform.position = playerTransform.position + Offset;
    }
}
