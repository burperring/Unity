using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector_DeltaTime : MonoBehaviour
{
    Vector3 target = new Vector3(8, 1.5f, 0);

    void Update()
    {
        // 1. MoveTowards(������ġ, ��ǥ��ġ, �ӵ�(���)) : ����̵�
        //transform.position = Vector3.MoveTowards(transform.position, target, 1f);


        // 2. SmoothDamp(������ġ, ��ǥ��ġ, �����ӵ�, �ӵ�(�ݺ��)) : �ε巯�� �����̵�
        //Vector3 velo = Vector3.zero;
        //                                                                 // ref : �������� -> �ǽð����� �ٲ�� �� ���� ����
        //transform.position = Vector3.SmoothDamp(transform.position, target, ref velo, 0.1f);


        // 3. Lerp(������ġ, ��ǥ��ġ, �ӵ�(���, �ִ밪 = 1)) : ��������, SmoothDamp���� ���ӽð��� ���.
        //transform.position = Vector3.Lerp(transform.position, target, 0.02f);


        // 4. Slerp(������ġ, ��ǥ��ġ, �ӵ�(���, �ִ밪 = 1)) : ���� ���� ����, ȣ�� �׸��� �̵�
        transform.position = Vector3.Slerp(transform.position, target, 0.005f);


        // 5. deltaTime : ���� �������� �Ϸ���� �ɸ� �ð�
        // deltaTime ����
        // 1) Translate : ���Ϳ� ���ϱ�
        //    transform.Translate(vec * Time.deltaTime);
        // 2) Vector �Լ� : �ð� �Ű������� ���ϱ�
        //    Vector3.Lerp(Vec1, Vec2, T * Time.deltaTime);
        Vector3 vec = new Vector3(
            Input.GetAxisRaw("Horizontal") * Time.deltaTime,
            Input.GetAxisRaw("Vertical") * Time.deltaTime);
        transform.Translate(vec);
    }
}
