using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Practice : MonoBehaviour
{
    //1. MainCamera : 2D월드의 상황을 보여주는 카메라
    //    1) Size를 통해 카메라 줌을 할 수 있다.
    //    2) 2D Background color는 MainCamera를 통해 변경한다.
    //    3) Projection을 통해 카메라에 원근법을 사용하게 변경할 수 있다.

    //2. Sprite : 2D월드에서 기본적으로 생성하는 Object
    //    1) Z축을 이용하여 Object의 배치 순서를 결정할 수 있다.
    //    2) Z축이 같더라도 Order in Layer를 통해 뭐가 먼저 보이게할지 정할 수 있다.
    //    3) 2D 프로젝트에선 이미지를 render하면 자동으로 Sprite로 적용해준다.
    //    4) 픽셀아트를 세팅할 경우 Filter Mode를 Point로 설정하면 번지지 않는다.
    //    5) Compression을 통해 이미지 압축 세팅을 변경할 수 있다.(픽셀아트의 경우 None으로 세팅)
    //    6) Pixels Per Unit을 통해 이미지 크기를 Pixel에 맞게 설정할 수 있다.

    //3. 물리 적용
    //    1) 2D는 3와 다르게 Box Collider 2D를 사용해야 한다.
    //    2) 3D와 마찬가지로 크기에 맞게 Collider 사이즈를 설정해줘야 한다.
    //    3) Rigidbody 역시 2D를 사용해야 한다.
    //    4) Project Setting -> Physics2D -> Default Contact Offset의 값을 최소값으로 변경하면
    //            Collider 충돌 여백을 없앨 수 있다.
}
