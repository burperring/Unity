using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explan : MonoBehaviour
{
    // 1. Rule Tile : ��Ģ�� ���� �� �ִ� Ÿ��
    //    1) Unity���� �⺻������ �����ϴ� ����� �ƴϴ�.
    //    2) ���ۿ��� 2D Extra �˻� -> GitHub -> 2d-extras �ٿ�ε�
    //    3) �ٿ�ε��� ������ ����� ������Ʈ�� �ֱ�
    //    4) ����� -> ��Ŭ�� -> 2D -> Tiles -> Rule Tile �����ϱ�
    //    5) ������ ���� �ش� Ÿ�ϵ��� ���� �� ��Ģ �����ϱ�

    // 2. �ִϸ��̼� Tile �����
    //    1) Rule Tile�� �����.
    //    2) �ش� Tile�� Output�� Single�� Animation���� �ٲ��ش�.

    // 3. �ܺ� ��� �����ϱ�
    //    1) Tile Map�� ���� �ܺ� ��踦 �����Ѵ�.
    //    2) ���������� ����� ���ؼ� Rigidbody, Tilemap Collider, Composite Collider�� �������ش�.
    //    3) �ƹ� Tile�� �ܺθ� �������ָ� �̻��ϱ� ������ ���� �� Tilemap Renderer�� Mask Interaction�� Visible Inside Mask�� �ٲ۴�.

    // 4. �ȼ� ����Ʈ
    //    1) �ȼ��� ������ ���̴� ��� �ȼ� ����Ʈ�� �̿��Ͽ� �Ϻ��ϰ� ����� �� �� �ִ�.(Unity���� �⺻������ �������� �ʴ´�.)
    //    2) Window -> Package Manager�� �� +��ư�� ���� Add package form git URL�� Ŭ���Ѵ�.
    //    3) �ű⿡ com.unity.entities�� �߰��ϸ� 2D pixel perfect�� �����ϸ� �ش� ������ ����Ʈ�ϸ� �ȴ�.
    //    4) ����Ʈ�� �Ϸ�Ǹ� mainCamera�� Pixel Perfect Camera Component�� �߰����ش�.
    //    5) Assets Pixels Per Unit�� ���� ������ �ִ� Pixel �ػ󵵿� ���� ������ָ� �ȴ�.
    //    6) X, Y, Stretch Fill�� true�� �ٲ���� �Ѵ�.

}