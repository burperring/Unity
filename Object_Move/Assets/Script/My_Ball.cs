using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class My_Ball : MonoBehaviour
{
    Rigidbody rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        // 1. �ӷ� �ٲٱ�
        // rigid.velocity = Vector3.right;     // velocity = ��ü�� �ӵ��� ������ �ش�.
        // rigid.velocity = new Vector3(2, 4, 3);
        // rigid.AddForce(Vector3.up * 10, ForceMode.Impulse);    // Vec�� ����� ũ��� ���� �ش�, ForceMode = ���� �ִ� ���(����, ���� �ݿ�)
    }

    private void FixedUpdate()
    {
        // RigidBody ���� �ڵ�� FixedUpdate�� �ۼ��ؾ� �Ѵ�.
        // rigid.velocity = new Vector3(2, 4, -1);

        // 2. ���� ���ϱ�
        if (Input.GetButtonDown("Jump"))
        {
            rigid.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }

        Vector3 vec = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        rigid.AddForce(vec, ForceMode.Impulse);

        // 3. ȸ����
        // rigid.AddTorque(Vector3.up);     // Vec ������ ������ ȸ������ ����, Vec�� ������ ��� ������ �̵� ���⿡ ����!
    }

    // OnTriggerStay : �ݶ��̴��� ��� �浹�ϰ� ���� �� ȣ��, ���������� Exit, Enter�� �����Ѵ�
    // Trigger�� ������ �浹�� �ƴ� ���ƴ��� Ȯ���ϴ� �Լ��̴�.
    private void OnTriggerStay(Collider other)
    {
        if(other.name == "Cube")
            rigid.AddForce(Vector3.up * 1, ForceMode.Impulse);
    }

    void Update()
    {
        
    }
}
