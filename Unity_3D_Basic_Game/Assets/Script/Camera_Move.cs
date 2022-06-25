using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Move : MonoBehaviour
{
    Transform playerTransform;
    Vector3 Offset;

    void Awake()
    {
        // Player tag를 가진 Object를 가져온다.
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // 현재 월드의 카메라와 Player와의 거리 구하기
        Offset = transform.position - playerTransform.position;
    }

    void LateUpdate()
    {
        // 현재 월드의 카메라와 Player와의 거리만큼 더하기
        transform.position = playerTransform.position + Offset;
    }
}
