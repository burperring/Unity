using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector_DeltaTime : MonoBehaviour
{
    Vector3 target = new Vector3(8, 1.5f, 0);

    void Update()
    {
        // 1. MoveTowards(현재위치, 목표위치, 속도(비례)) : 등속이동
        //transform.position = Vector3.MoveTowards(transform.position, target, 1f);


        // 2. SmoothDamp(현재위치, 목표위치, 참조속도, 속도(반비례)) : 부드러운 감속이동
        //Vector3 velo = Vector3.zero;
        //                                                                 // ref : 참조접근 -> 실시간으로 바뀌는 값 적용 가능
        //transform.position = Vector3.SmoothDamp(transform.position, target, ref velo, 0.1f);


        // 3. Lerp(현재위치, 목표위치, 속도(비례, 최대값 = 1)) : 선형보간, SmoothDamp보다 감속시간이 길다.
        //transform.position = Vector3.Lerp(transform.position, target, 0.02f);


        // 4. Slerp(현재위치, 목표위치, 속도(비례, 최대값 = 1)) : 구면 선형 보간, 호를 그리며 이동
        transform.position = Vector3.Slerp(transform.position, target, 0.005f);


        // 5. deltaTime : 이전 프레임의 완료까지 걸린 시간
        // deltaTime 사용법
        // 1) Translate : 벡터에 곱하기
        //    transform.Translate(vec * Time.deltaTime);
        // 2) Vector 함수 : 시간 매개변수에 곱하기
        //    Vector3.Lerp(Vec1, Vec2, T * Time.deltaTime);
        Vector3 vec = new Vector3(
            Input.GetAxisRaw("Horizontal") * Time.deltaTime,
            Input.GetAxisRaw("Vertical") * Time.deltaTime);
        transform.Translate(vec);
    }
}
