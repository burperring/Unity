using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explan : MonoBehaviour
{
    // 1. Rule Tile : 규칙을 정할 수 있는 타일
    //    1) Unity에서 기본적으로 제공하는 방식이 아니다.
    //    2) 구글에서 2D Extra 검색 -> GitHub -> 2d-extras 다운로드
    //    3) 다운로드한 파일을 사용할 프로젝트에 넣기
    //    4) 빈공간 -> 우클릭 -> 2D -> Tiles -> Rule Tile 생성하기
    //    5) 생성한 이후 해당 타일들을 넣은 후 규칙 생성하기

    // 2. 애니메이션 Tile 만들기
    //    1) Rule Tile을 만든다.
    //    2) 해당 Tile의 Output의 Single을 Animation으로 바꿔준다.

    // 3. 외부 경계 생성하기
    //    1) Tile Map을 통해 외부 경계를 생성한다.
    //    2) 물리현상을 만들기 위해서 Rigidbody, Tilemap Collider, Composite Collider를 생성해준다.
    //    3) 아무 Tile로 외부를 생성해주면 이상하기 때문에 만든 후 Tilemap Renderer의 Mask Interaction을 Visible Inside Mask로 바꾼다.

    // 4. 픽셀 퍼펙트
    //    1) 픽셀이 깨져서 보이는 경우 픽셀 퍼펙트를 이용하여 완벽하게 만들어 줄 수 있다.(Unity에서 기본적으로 제공하지 않는다.)
    //    2) Window -> Package Manager에 들어가 +버튼을 눌러 Add package form git URL을 클릭한다.
    //    3) 거기에 com.unity.entities를 추가하면 2D pixel perfect가 존재하며 해당 파일을 임포트하면 된다.
    //    4) 임포트가 완료되면 mainCamera에 Pixel Perfect Camera Component를 추가해준다.
    //    5) Assets Pixels Per Unit의 값을 가지고 있는 Pixel 해상도와 같게 만들어주면 된다.
    //    6) X, Y, Stretch Fill을 true로 바꿔줘야 한다.

}