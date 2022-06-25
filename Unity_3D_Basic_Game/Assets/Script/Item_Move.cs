using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Move : MonoBehaviour
{
    public float rotateSpeed;

    void Update()
    {
        // Rotate(Vector3) : 매개변수 기준으로 회전시키는 함수
        // Space.World : 월드 좌표계를 기준으로 회전시킨다.
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {

    }
}
