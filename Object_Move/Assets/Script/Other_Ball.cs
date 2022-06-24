using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Other_Ball : MonoBehaviour
{
    MeshRenderer mesh;
    Material mat;

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mat = mesh.material;
    }

    // 위에서 부터 세가지 함수는 한 묶음이다.
    // OnCollisionEnter : 물리적 충돌이 시작할 때 호출되는 함수(충돌 시작)
    private void OnCollisionEnter(Collision collision)
    {
        // Collision : 충돌 정보 클래스
        // if문을 통해 걸어주지 않았을 경우 프로그램 실행과 동시에 바닥과 충돌하여 충돌함수가 실행된다.
        if(collision.gameObject.name == "My_Ball")
            mat.color = new Color(0, 0, 0);
    }

    // OnCollisionStay : 물리적 충돌이 진행중일 때 호출되는 함수(충돌하는 중)
    private void OnCollisionStay(Collision collision)
    {

    }

    // OnCollisionExit : 물리적 충돌이 끝났을 때 호출되는 함수(충돌 끝)
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "My_Ball")
            mat.color = new Color(1, 1, 1);
    }
    // -----------------------------------------

    /*
    // 실제 물리적인 충돌로 발생하는 이벤트
    void OnCollisionEnter(Collision collision){  }
    void OnCollisionStay(Collision collision) { }
    void OnCollisionExit(Collision collision) { }

    // 콜라이더 충돌로 발생하는 이벤트
    void OnTriggerEnter(Collider other) { }
    void OnTriggerStay(Collider other) { }
    void OnTriggerExit(Collider other) { }
    */
}
