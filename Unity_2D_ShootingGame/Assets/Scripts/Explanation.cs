using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explanation : MonoBehaviour
{
    // 1. ���� ȭ�� ����
    //    1) ���� â���� Free Aspect�� ��� â ũ�⿡ ���� ȭ�� ������ �ڵ����� �����ִ� ���̴�.
    //    2) ������ ������ �������� �ش� ��ư�� ���� ���ϴ� ������ �����ϸ� �ȴ�.
    //    3) ���ϴ� ������ ���� ��� ���Ӱ� ����� �ȴ�. �̶� Aspect Ratio�� ����� �Ǹ� ������ �����ǰ� ũ��� �����Ӱ� ���Ѵ�.

    // 2. Rigidbody 2D
    //    1) Body Types�� Kinematics�� �����ϰ� �ȴٸ� ���������� �ȹް� �ȴ�.
    //    2) Kinematics�� �����ϸ� Rigidbody ������ �浹 �������굵 �ȹް� �ȴ�.

    // 3. ������ : ��Ȱ���� ���� �������� ����� ���� ������Ʈ
    //    1) ������ ������Ʈ�� Hierarchyâ���� Assets ������ �ű�� �Ǹ� �ش� ������Ʈ�� ������(Prefabs)�� �ȴ�.
    //    2) �������� �� �ش� ������Ʈ�� �Ķ������� ���ϰ� �Ǹ� ȭ��ǥ�� �����.
    //    3) �������� �̿��� ������Ʈ�� �����ϰ� �Ǹ� ��� ���̰� �ǹǷ� Destroy()�� �̿��Ͽ� �ı��ؾ� �Ѵ�.
    //    4) �ڵ忡�� ������ �������� ����ϱ� ���ؼ��� Destroy()�� �ݴ��� Instantiate()�� ����ؾ� �Ѵ�. 
    //              Instantiate(������, ������ġ, ������Ʈ ����)(type 4) : �Ű����� ������Ʈ�� �����ϴ� �Լ�

    // 4. Vector�� ����Vector�� �����ϴ� ���
    //    1) Vector2, Vector3 ��� ����� �����ϴ�.
    //    2) Vector�� ������ ���� .normalized�� ���� �ȴ�.
    //    3) .normalized�� ���� ���Ͱ� ���� ��(1)�� ���ѵȴ�.

    // 5. UI ����
    //    1) UI�� ������ �� ���� �ػ󵵿� ���� UI ũ�⸦ �����ϱ� ���ؼ��� Canvas Scaler -> UI Scale Mode -> Scale With Screen Size�� �����Ѵ�.
    //    2) Scale With Screen Size : ���� �ػ��� UI ũ�� ����
    //    3) Reference Resolution���� Canvas Scaler(���� �ػ� ��)�� ��������� �Ѵ�.
    //    4) ���ڸ����� ��ǥ�� �ִ� ���ڸ� �����ϱ� ���ؼ��� .ToString()�� �ƴ� string.Format()���� �����ؾ� �Ѵ�.
    //    5) string.Format("{0:n0}", ���ڰ�) : �ش� ���ڰ��� ���ڸ����� ��ǥ�� �����ִ� ���� ������� ��ȯ�϶�
}