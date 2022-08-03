using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform target;
    public float orbitSpeed;
    Vector3 offSet;

    void Start()
    {
        offSet = transform.position - target.position;
    }

    void Update()
    {
        transform.position = target.position + offSet;

        // RotateAround() : 타겟 주위를 회전하는 함수
        transform.RotateAround(target.position, Vector3.up, orbitSpeed * Time.deltaTime);

        offSet = transform.position - target.position;
    }
}
