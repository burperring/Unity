using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explanation : MonoBehaviour
{
    // 1. 아틀라스
    //    1) 동시에 존재하는 이미지는 Sprite Mode를 Multiple로 변경해 사용한다.
    //    2) Sprite Editor를 통해 이미지를 잘라준다.
    //    3) Slice를 진행할 때 Automatic을 사용하면 이미지 사이즈에 맞게 짤라준다.
    //    4) Slice를 진행할 때 Grid By Cell Size를 사용하면 사용자가 원하는대로 이미지 사이즈를 지정해서 자를 수 있다.
    //    5) 이런식으로 동시에 존재하는 이미지를 아틀라스를 통해 사용하는 이유
    //        I.각 Stats에 존재하는 Batches 수가 적어진다.(Batch : 그래픽을 그리기 위해 메모리와 CPU를 사용한 횟수)
    //        II.즉 최적화에 좋아진다.(모바일의 경우 무조건 필수)

    // 2. 애니메이션
    //    1) 애니메이션으로 생성할 이미지를 한꺼번에 드래그하여 Object에 넣어준다.
    //    2) 그러먼 해당 Object에 Animator Component가 생성된다.
    //    3) Window -> Animation(혹은 Ctrl + 6)을 통해 애니메이션 창을 볼 수 있다.
    //    4) Window -> Animation -> Animator를 통해 애니메이션 상태를 변경할 수 있다.
    //    5) 해당 애니메이션의 Default State를 변경하기 위해서는 마우스 우클릭을 통해 변경할 수 있다.
    //    6) Animator에서 애니메이션의 Speed를 변경할 수 있다.

    // 3. 캐릭터 공기저항 넣는 법
    //    1) 해당 Object의 Rigidbody에서 Linear Drag를 통해 넣어준다.

    // 4. 키 입력에 따른 애니메이션 넣기
    //    1) 캐릭터가 움직이는 방향에 따른 애니메이션을 넣기 위해서는 Sprite Renderer에 Flip을 이용한다.
    //    2) Animator에서 마우스 우클릭을 통해 Make Transition을 만들어 다음 애니메이션을 선택한다.
    //    3) Animator의 Parameters에서 애니메이터 매개변수를 생성해준다.(애니메이터 매개변수 : 상태를 바꿀 때 필요한 변수)
    //    4) 생성했다면 화살표를 눌러 Conditions에 해당 매개변수를 추가해준다.
    //    5) Has Exit Time을 True로 설정하면 이전 애니메이션이 끝나기 전까지 다음 애니메이션을 진행하지 않는다.

}
