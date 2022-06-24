using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_Practice : MonoBehaviour
{
    // * UI는 Hieracy의 순서에 따라 위 아래가 결정된다.
    //     -> 밑에 있을수록 가낭 나중에 그린다.(가장 위에 그려진다.)

    // GUI : 게임 플레이시 유저들에게 보여주는 창
    // 1. Canvas : Hierarchy 창에서 Object 생성하듯이 Canvas를 생성해줘야 한다.
    //     1) 마우스 우클릭 -> UI -> Canvas
    
    // 2. 스크린 : Canvas를 생성하면 나오는 영역
    //     1) 게임이 표시되는 화면, 해당도로 크기를 결정하는 구역이다.
    
    // 3. 텍스트 UI : UI부분에서 텍스트를 입력하는 공간
    //     1) Canvas 우클릭 -> UI -> Lagacy -> Text
    //     2) 폰트를 변경하기 위해서는 다운받은 폰트를 사용할 Unity 프로젝트에 옮겨주면 사용할 수 있다.
    //             -> 사용하는 폰트 라이센스를 확인하고 사용해야 한다.
    //     3) Horizontal, Vertical Overflow 값을 기존 Wrap, Truncate가 아닌 Overflow로 변경하면 짤린 글씨를 보여준다.
    //             -> 대신 실제 실행했을 때 글자가 깨질 수 있다.
    //     4) Line Spacing은 줄 바꿈이 일어날 때 공간크기를 의미한다.
    
    // 4. 이미지 UI : UI부분에서 이미지를 출력하는 공간
    //     1) UI에 이미지를 출력하기 위해서는 출력할 이미지를 Unity 프로젝트에 옮겨줘야 한다.
    //     2) 옮긴 이미지의 type을 Default에서 Sprite로 설정해야 UI에 적용이 가능하다.
    //     3) Preserve Aspect를 통해 이미지 비율을 고정시킬 수 있다.
    //     4) Set Native Size를 통해 이미지의 원래 크기로 변환시킬 수 있다.
    //     5) Image Type은 총 4가지로 이미지의 크기를 변환시키는 방법이다.
    //         I. Simple : 일반적인 이미지 확장 방법
    //         II. Sliced : 이미지의 양 끝은 남겨둔 채 중간을 늘리는 방법
    //         III. Tiled : 보여주는 크기에 맞춰서 원래 이미지를 타일처럼 붙여넣는 방법
    //         IV. Filled : 가로, 세로, 각도의 크기에 맞춰서 이미지를 짤라서 보여주는 방법
    //                 -> Filled 기능을 이용하여 쿨타임 효과 구현이 가능하다.

    // 5. 버튼 UI : 클릭 이벤트를 가지고 있는 반응형 UI
    //     1) Canvas 우클릭 -> UI -> Lagacy -> Button
    //     2) Interactable로 버튼 반응하게 할지 말지를 결정(T/F)
    //     3) Transition : 버튼을 눌렀을 때 어떻게 반응할지 결정하는 곳
    //         I. Color Tint : 버튼의 색을 변경한다. ex) 마우스를 버튼에 올린다, 누른다, 비활성된다.
    //     4) On Click() : 버튼 클릭 시 호출되는 이벤트 함수
    //         I. Hierarchy에서 사용할 함수를 가지고 있는 오브젝트를 버튼에 연결시켜준다.
    //         II. 사용할 함수를 선택해준다.

    // 6. 앵커 : 만든 UI에 위치한 것들을 해상도에 따라서 위치를 고정시키는 방법
    //     1) 각 UI들의 Rect Transform에서 Alt + Shift를 눌러 원하는 방향에 고정시켜 준다.
    //     2) 고정된 UI를 위치에 맞게 값을 변경하여 이쁘게 정리한다.
}
