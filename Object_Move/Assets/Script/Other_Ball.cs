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

    // ������ ���� ������ �Լ��� �� �����̴�.
    // OnCollisionEnter : ������ �浹�� ������ �� ȣ��Ǵ� �Լ�(�浹 ����)
    private void OnCollisionEnter(Collision collision)
    {
        // Collision : �浹 ���� Ŭ����
        // if���� ���� �ɾ����� �ʾ��� ��� ���α׷� ����� ���ÿ� �ٴڰ� �浹�Ͽ� �浹�Լ��� ����ȴ�.
        if(collision.gameObject.name == "My_Ball")
            mat.color = new Color(0, 0, 0);
    }

    // OnCollisionStay : ������ �浹�� �������� �� ȣ��Ǵ� �Լ�(�浹�ϴ� ��)
    private void OnCollisionStay(Collision collision)
    {

    }

    // OnCollisionExit : ������ �浹�� ������ �� ȣ��Ǵ� �Լ�(�浹 ��)
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "My_Ball")
            mat.color = new Color(1, 1, 1);
    }
    // -----------------------------------------

    /*
    // ���� �������� �浹�� �߻��ϴ� �̺�Ʈ
    void OnCollisionEnter(Collision collision){  }
    void OnCollisionStay(Collision collision) { }
    void OnCollisionExit(Collision collision) { }

    // �ݶ��̴� �浹�� �߻��ϴ� �̺�Ʈ
    void OnTriggerEnter(Collider other) { }
    void OnTriggerStay(Collider other) { }
    void OnTriggerExit(Collider other) { }
    */
}
