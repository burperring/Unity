using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KM_Input : MonoBehaviour
{
    void Start()
    {
        // Translate : 벡터 값을 현재의 위치에 더하는 함수
        //Vector3 vec = new Vector3(10, 0, 0);
        //transform.Translate(vec);
    }
    void Update()
    {
        // Input : 게임 내 입력을 관리하는 클래스
        // anyKey : 아무 키를 입력받으면 true 반환
        if(Input.anyKeyDown)
            Debug.Log("anyKeyDown은 아무 키를 누를때 반환한다.");

        if (Input.anyKey)
            Debug.Log("anyKey는 아무 키를 누르는 동안 반환한다.");

        // GetKey : 키보드 버튼을 입력받으면 true 반환
        if (Input.GetKeyDown(KeyCode.Return))
            Debug.Log("Enter Key를 눌렀습니다.");

        if (Input.GetKey(KeyCode.LeftArrow))
            Debug.Log("왼쪽 방향키를 누르고 있습니다.");

        if (Input.GetKeyUp(KeyCode.RightArrow))
            Debug.Log("오른쪽 방향키를 땠습니다.");

        // GetMouseButton : 마우스 버튼을 입력받으면 true 반환
        if (Input.GetMouseButtonDown(0))
            Debug.Log("마우스 왼쪽 버튼을 눌렀습니다.");

        if (Input.GetMouseButton(1))
            Debug.Log("마우스 오른쪽 버튼을 누르고 있습니다.");

        if (Input.GetMouseButtonUp(0))
            Debug.Log("마우스 왼쪽 버튼을 땠습니다.");

        // GetButton : Edit -> Project Setting에 있는 Input 값을 입력받으면 true 반환
        if (Input.GetButtonDown("Jump"))
            Debug.Log("Jump 버튼을 눌렀습니다.");

        if (Input.GetButton("Jump"))
            Debug.Log("Jump 버튼을 누르는 중입니다.");

        if (Input.GetButtonUp("Jump"))
            Debug.Log("Jump 버튼을 땠습니다.");

        // 횡, 종 이동
        // GetAxis : 수평, 수직 버튼 입력을 받으면 float값 반환(가중치)
        // GetAxisRaw : 수평, 수직 버튼 입력을 받으면 float 값 반환(가중치 x, 중간값 없이 -1, 0, 1 출력)
        if (Input.GetButton("Horizontal"))
            Debug.Log("횡 이동 중..." + Input.GetAxis("Horizontal"));

        if (Input.GetButton("Horizontal"))
            Debug.Log("횡 이동 중..." + Input.GetAxisRaw("Horizontal"));

        if (Input.GetButton("Vertical"))
            Debug.Log("종 이동 중..." + Input.GetAxisRaw("Vertical"));

        // 오프젝트 이동
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
