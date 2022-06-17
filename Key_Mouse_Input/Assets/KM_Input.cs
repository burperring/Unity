using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KM_Input : MonoBehaviour
{
    void Start()
    {
        // Translate : ���� ���� ������ ��ġ�� ���ϴ� �Լ�
        //Vector3 vec = new Vector3(10, 0, 0);
        //transform.Translate(vec);
    }
    void Update()
    {
        // Input : ���� �� �Է��� �����ϴ� Ŭ����
        // anyKey : �ƹ� Ű�� �Է¹����� true ��ȯ
        if(Input.anyKeyDown)
            Debug.Log("anyKeyDown�� �ƹ� Ű�� ������ ��ȯ�Ѵ�.");

        if (Input.anyKey)
            Debug.Log("anyKey�� �ƹ� Ű�� ������ ���� ��ȯ�Ѵ�.");

        // GetKey : Ű���� ��ư�� �Է¹����� true ��ȯ
        if (Input.GetKeyDown(KeyCode.Return))
            Debug.Log("Enter Key�� �������ϴ�.");

        if (Input.GetKey(KeyCode.LeftArrow))
            Debug.Log("���� ����Ű�� ������ �ֽ��ϴ�.");

        if (Input.GetKeyUp(KeyCode.RightArrow))
            Debug.Log("������ ����Ű�� �����ϴ�.");

        // GetMouseButton : ���콺 ��ư�� �Է¹����� true ��ȯ
        if (Input.GetMouseButtonDown(0))
            Debug.Log("���콺 ���� ��ư�� �������ϴ�.");

        if (Input.GetMouseButton(1))
            Debug.Log("���콺 ������ ��ư�� ������ �ֽ��ϴ�.");

        if (Input.GetMouseButtonUp(0))
            Debug.Log("���콺 ���� ��ư�� �����ϴ�.");

        // GetButton : Edit -> Project Setting�� �ִ� Input ���� �Է¹����� true ��ȯ
        if (Input.GetButtonDown("Jump"))
            Debug.Log("Jump ��ư�� �������ϴ�.");

        if (Input.GetButton("Jump"))
            Debug.Log("Jump ��ư�� ������ ���Դϴ�.");

        if (Input.GetButtonUp("Jump"))
            Debug.Log("Jump ��ư�� �����ϴ�.");

        // Ⱦ, �� �̵�
        // GetAxis : ����, ���� ��ư �Է��� ������ float�� ��ȯ(����ġ)
        // GetAxisRaw : ����, ���� ��ư �Է��� ������ float �� ��ȯ(����ġ x, �߰��� ���� -1, 0, 1 ���)
        if (Input.GetButton("Horizontal"))
            Debug.Log("Ⱦ �̵� ��..." + Input.GetAxis("Horizontal"));

        if (Input.GetButton("Horizontal"))
            Debug.Log("Ⱦ �̵� ��..." + Input.GetAxisRaw("Horizontal"));

        if (Input.GetButton("Vertical"))
            Debug.Log("�� �̵� ��..." + Input.GetAxisRaw("Vertical"));

        // ������Ʈ �̵�
        if (Input.GetButton("Jump"))
        {
            Vector3 vec = new Vector3(0, 0.01f, 0);
            transform.Translate(vec);
        }

        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            Vector3 vec = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            transform.Translate(vec);
        }

    }
}
