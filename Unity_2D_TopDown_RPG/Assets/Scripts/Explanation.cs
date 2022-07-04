using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explan : MonoBehaviour
{
    // ** Player에 넣은 카메라가 혼자 굴러다닐 경우 Player의 Rigidbody에서 z축을 고정시켜주면 문제가 해결된다.
    // ** Public 인자값 꼭 넣어주기

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

    // 5. 애니메이션 세팅
    //    1) 애니메이션 세팅할 때 Setting -> Transition Duration을 0 으로 세팅하면 움직임 변화를 바로 보여줄 수 있다.
    //    2) 움직임 애니메이션을 진행할 때 키 입력을 꾹 누르고 있으면 값이 계속 들어가게 되어서 애니메이션이 실행되지 않는다.
    //    3) 프로그래밍을 통해 강제적으로 키 입력을 한번만 넣어주면 된다.
    //    4) 애니메이션에서 방향전환이 바로바로 실행되지 않는 경우 animator 세팅에서 can transition to self를 해제해야 한다.
    //    5) 여러개의 이미지를 통한 애니메이션이 아닌 특정 이미지가 움직이는 애니메이션을 만들기 위해서는 Animator 컴포넌트에 Animator Controller와 Animation을 넣어줘야 한다.
    //    6) Animator Controller와 Animation은 마우스 우클릭을 통해 생성할 수 있다.
    //    7) 세팅이 완료되면 Window -> Animation 창을 열어 애니메이션을 제작하면 된다.

    // 6. UI 세팅
    //    1) 특정 이미지를 늘어뜨리지 않고 외곽은 그대로 냅둔 채 안쪽만 채워넣기 위해서는 Image Type을 Sliced로 변경한다.
    //    2) Sliced를 사용하기 위해서는 Sprite Editor에 들어가서 Border값을 지정해줘야 한다.
    //    3) 이미지를 양끝으로 가득채우기 위해서는 Ctrl + Shift + Alt를 모두 누른 다음 stretch에 원하는 위치를 선택하여 설정한다.
    //    4) GameObject 기본 함수는 인스펙터 창에서 바로 할당이 가능하다. (ex Menu Set의 Button들은 클릭 함수 생성에 Menu Set을 그대로 가져오면 기본적인 GameObject 함수를 사용할 수 있다.)

    // 7. 대화창 세팅
    //    1) 대화 데이터를 저장할 땐 Dictionary 변수를 사용한다.
    //    2) Dictionary 변수는 Key값과 Key와 연결된 Value 2개를 사용하는 변수로 Type을 2개를 꼭 작성해줘야 한다.
    //    3) 반환 값이 있는 재귀함수는 return까지 꼭 써줘야 한다.

    // 8. 세이브 위치
    //    1) PlayerPrefs를 이용하여 세이브를 진행하면 레지스트리에 저장된다
    //    2) 레지스트리 편집기 -> HKEY_CURRENT_USER -> Software -> 회사이름 -> 게임이름에 저장된다.
}