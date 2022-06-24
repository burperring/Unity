using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class My_Ball : MonoBehaviour
{
    Rigidbody rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        // 1. 속력 바꾸기
        // rigid.velocity = Vector3.right;     // velocity = 물체의 속도를 생성해 준다.
        // rigid.velocity = new Vector3(2, 4, 3);
        // rigid.AddForce(Vector3.up * 10, ForceMode.Impulse);    // Vec의 방향과 크기로 힘을 준다, ForceMode = 힘을 주는 방식(가속, 무게 반영)
    }

    private void FixedUpdate()
    {
        // RigidBody 관련 코드는 FixedUpdate에 작성해야 한다.
        // rigid.velocity = new Vector3(2, 4, -1);

        // 2. 힘을 가하기
        if (Input.GetButtonDown("Jump"))
        {
            rigid.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }

        Vector3 vec = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        rigid.AddForce(vec, ForceMode.Impulse);

        // 3. 회전력
        // rigid.AddTorque(Vector3.up);     // Vec 방향을 축으로 회전력이 생김, Vec을 축으로 삼기 때문에 이동 방향에 주의!
    }

    // OnTriggerStay : 콜라이더가 계속 충돌하고 있을 때 호출, 마찬가지로 Exit, Enter가 존재한다
    // Trigger는 물리적 충돌이 아닌 곂쳤는지 확인하는 함수이다.
    private void OnTriggerStay(Collider other)
    {
        if(other.name == "Cube")
            rigid.AddForce(Vector3.up * 1, ForceMode.Impulse);
    }

    void Update()
    {
        
    }
}
