using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explanation : MonoBehaviour
{
    // 1. 게임 화면 비율
    //    1) 게임 창에서 Free Aspect의 경우 창 크기에 따른 화면 비율을 자동으로 맞춰주는 것이다.
    //    2) 고정된 비율을 가지려면 해당 버튼을 눌러 원하는 비율을 세팅하면 된다.
    //    3) 원하는 비율이 없을 경우 새롭게 만들면 된다. 이때 Aspect Ratio로 만들게 되면 비율만 고정되고 크기는 자유롭게 변한다.

    // 2. Rigidbody 2D
    //    1) Body Types를 Kinematics로 설정하게 된다면 물리연산을 안받게 된다.
    //    2) Kinematics로 설정하면 Rigidbody 끼리의 충돌 물리연산도 안받게 된다.

    // 3. 프리펩 : 재활용을 위해 에셋으로 저장된 게임 오브젝트
    //    1) 생성한 오브젝트를 Hierarchy창에서 Assets 폴더로 옮기게 되면 해당 오브젝트는 프리펩(Prefabs)이 된다.
    //    2) 프리펩이 된 해당 오브젝트는 파란색으로 변하게 되며 화살표가 생긴다.
    //    3) 프리펩을 이용해 오브젝트를 생성하게 되면 계속 쌓이게 되므로 Destroy()를 이용하여 파괴해야 한다.
    //    4) 코드에서 생성한 프리펩을 사용하기 위해서는 Destroy()의 반대인 Instantiate()를 사용해야 한다. 
    //              Instantiate(프리펩, 생성위치, 오브젝트 방향)(type 4) : 매개변수 오브젝트를 생성하는 함수

    // 4. Vector를 단위Vector로 변경하는 방법
    //    1) Vector2, Vector3 모두 사용이 가능하다.
    //    2) Vector로 저장한 값에 .normalized를 쓰면 된다.
    //    3) .normalized를 쓰면 벡터가 단위 값(1)로 변한된다.

    // 5. UI 세팅
    //    1) UI를 세팅할 때 기준 해상도에 따라 UI 크기를 유지하기 위해서는 Canvas Scaler -> UI Scale Mode -> Scale With Screen Size로 세팅한다.
    //    2) Scale With Screen Size : 기준 해상도의 UI 크기 유지
    //    3) Reference Resolution에서 Canvas Scaler(기준 해상도 값)을 설정해줘야 한다.
    //    4) 세자리마다 쉼표를 넣는 숫자를 세팅하기 위해서는 .ToString()이 아닌 string.Format()으로 세팅해야 한다.
    //    5) string.Format("{0:n0}", 인자값) : 해당 인자값을 세자리마디 쉼표로 나눠주는 숫자 양식으로 변환하라

    // 6. 스크롤링
    //    1) 게임하는 배경 밖에서 사용을 다한 배경을 다시 앞으로 보내는 기법

    // 7. 오브젝트 풀링
    //    1) 현재까지 사용한 방법인 instantiate와 Destroy를 사용하여 오브젝트를 생성, 삭제하게 되면 조각난 쓰레기 메모리가 쌓이게 된다.
    //    2) 이러한 쓰레기 메모리가 쌓이게 되면 가비지컬렉트(GC)가 발동하게 되는데 이때 게임이 잠깐 끊기게 된다.(게임 렉)
    //                                      ** 가비지컬렉트(GC) : 쌓인 조각난 메모리를 비우는 기술
    //    3) 이러한 현상을 없애기 위해서 오브젝트 폴링 기술을 사용해야 한다.
    //    4) 오브젝트 폴링은 미리 생성해둔 풀에서 활성화/비활성화로 사용하는 방식이다.
    //    5) 게임을 진행할 때 로딩시간이 존재하는 이유는 장면배치 + 오브젝트 풀 생성을 위해 시간을 벌기 위해서이다.
    //    6) 풀링을 사용하면 Find를 진행할 때 많은 양을 통해 검색해야 하므로 딜레이가 발생할 수 있다.

    // 8. 텍스트 데이터
    //    1) 구조체를 생성한 다음 구조체 변수에 맞도록 값을 입력한다.
    //    2) 해당텍스트 파일을 저장한 다음 Resources 폴더를 생성한 다음 해당 폴더에 저장한다.
    //    3) 저장된 파일을 코드를 통해 읽어온 후 적용시킨다.
}
