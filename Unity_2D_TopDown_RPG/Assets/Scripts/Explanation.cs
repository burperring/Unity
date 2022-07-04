using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explan : MonoBehaviour
{
    // ** Player�� ���� ī�޶� ȥ�� �����ٴ� ��� Player�� Rigidbody���� z���� ���������ָ� ������ �ذ�ȴ�.
    // ** Public ���ڰ� �� �־��ֱ�

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

    // 5. �ִϸ��̼� ����
    //    1) �ִϸ��̼� ������ �� Setting -> Transition Duration�� 0 ���� �����ϸ� ������ ��ȭ�� �ٷ� ������ �� �ִ�.
    //    2) ������ �ִϸ��̼��� ������ �� Ű �Է��� �� ������ ������ ���� ��� ���� �Ǿ �ִϸ��̼��� ������� �ʴ´�.
    //    3) ���α׷����� ���� ���������� Ű �Է��� �ѹ��� �־��ָ� �ȴ�.
    //    4) �ִϸ��̼ǿ��� ������ȯ�� �ٷιٷ� ������� �ʴ� ��� animator ���ÿ��� can transition to self�� �����ؾ� �Ѵ�.
    //    5) �������� �̹����� ���� �ִϸ��̼��� �ƴ� Ư�� �̹����� �����̴� �ִϸ��̼��� ����� ���ؼ��� Animator ������Ʈ�� Animator Controller�� Animation�� �־���� �Ѵ�.
    //    6) Animator Controller�� Animation�� ���콺 ��Ŭ���� ���� ������ �� �ִ�.
    //    7) ������ �Ϸ�Ǹ� Window -> Animation â�� ���� �ִϸ��̼��� �����ϸ� �ȴ�.

    // 6. UI ����
    //    1) Ư�� �̹����� �þ�߸��� �ʰ� �ܰ��� �״�� ���� ä ���ʸ� ä���ֱ� ���ؼ��� Image Type�� Sliced�� �����Ѵ�.
    //    2) Sliced�� ����ϱ� ���ؼ��� Sprite Editor�� ���� Border���� ��������� �Ѵ�.
    //    3) �̹����� �糡���� ����ä��� ���ؼ��� Ctrl + Shift + Alt�� ��� ���� ���� stretch�� ���ϴ� ��ġ�� �����Ͽ� �����Ѵ�.
    //    4) GameObject �⺻ �Լ��� �ν����� â���� �ٷ� �Ҵ��� �����ϴ�. (ex Menu Set�� Button���� Ŭ�� �Լ� ������ Menu Set�� �״�� �������� �⺻���� GameObject �Լ��� ����� �� �ִ�.)

    // 7. ��ȭâ ����
    //    1) ��ȭ �����͸� ������ �� Dictionary ������ ����Ѵ�.
    //    2) Dictionary ������ Key���� Key�� ����� Value 2���� ����ϴ� ������ Type�� 2���� �� �ۼ������ �Ѵ�.
    //    3) ��ȯ ���� �ִ� ����Լ��� return���� �� ����� �Ѵ�.

    // 8. ���̺� ��ġ
    //    1) PlayerPrefs�� �̿��Ͽ� ���̺긦 �����ϸ� ������Ʈ���� ����ȴ�
    //    2) ������Ʈ�� ������ -> HKEY_CURRENT_USER -> Software -> ȸ���̸� -> �����̸��� ����ȴ�.
}