// 1. 3D 애니메이션
//    1) 3D 애니메이션을 만들 때는 .FBX 파일이 필요하다.
//          .FBX : 각종 정보들이 구분되어 저장되는 3D 포맷

// 2. 파티클 설정
//    1) 파티클의 Material을 설정하지 않으면 기본값으로 보라색(마젠타)색으로 나오게 된다.
//    2) Renderer에서 파티클의 Material을 설정할 수 있다.
//    3) Emission에서 파티클 입자 출력양을 설정할 수 있다.
//    4) Shape를 통해 파티클 입자 출력 모양을 설정할 수 있다.
//    5) Color Over Life Time을 통해 시간에 따른 파티클의 색상변화를 설정할 수 있다.
//    6) Size Over Life Time을 통해 시간에 따른 파티클의 크기변화를 설정할 수 있다.
//    7) Limit Velocity over Life Time을 통해 시간에 따른 파티클의 속도를 제한할 수 있다.
//          ** 해당 옵션에서 Drag를 통해 저항값을 설정하면 파티클이 이동하는 양을 설정할 수 있다.
//    8) 파티클 Start Speed와 Start Lifetime을 통해 파티클이 남아있는 시간과 기본 속도를 설정할 수 있다.
//    9) 캐릭터 주변 물체가 움직일 때 파티클이 나오게 하는 방법
//          -> Simulation Space를 World로 세팅한 다음 Emission의 값을 Rate over Distance로 세팅하면 된다.

// 3. 애니메이션 레이어
//    1) 어러개의 애니메이션을 한번에 동작시키기 위해서는 애니메이션 레이어를 사용해야 한다.
//    2) 애니메이션 레이어를 사용하기 위해서는 특정 부위만 실행시키기 위한 Avatar Mask가 필요하다.
//    3) 사람형태의 경우 쉽게 설정할 수 있지만 그렇지 않을 경우 .FBX파일의 Rig에서 Avatar Definition을 Create From this Model로 변경하면 해당 아바타가 생성된다.
//    4) 생성된 아바타를 Avatar Mask에서 Transform을 통해 원하는 위치만 선정하여 설정할 수 있다.
//    5) 새롭게 덧그릴 애니메이션의 경우 기존 Base Layer 대신 새로운 Layer를 생성하여 해당 애니메이션이 실행될 수 있는 값을 설정해준다.
//    6) 값을 모두 설정하면 Layer 설정을 눌러 생성한 Mask를 넣어준 다음 Weight값을 설정하여 애니메이션을 어느정도로 실행할지 결정한다.
//              ** 참고로 Weight 값이 1에 가까울수록 해당 애니메이션의 동작이 선명해진다.
//                 Weight 값을 알맞게 설정하여 최대한 자연스럽게 애니메이션이 동작하게 설정하면 된다.

// 4. 트레일 랜더러(Trail Renderer)
//    1) 트레일 랜더러는 Add Component를 통해 추가할 수 있다.
//    2) 트레일 랜더러를 사용하게 되면 해당 오브젝트가 움직일 때 모양, 잔상을 남길 수 있다.
//    3) Time을 통해 잔상이 남아있는 시간을 설정할 수 있다.
//    4) Min Vertex Distance를 통해 잔상의 꺾임을 설정할 수 있다. (값이 크면 클수록 모양이 각지게 된다.)

// 5. Nav AI
//    1) 캐릭터의 AI를 생성하는 방법은 NavMeshAgent 컴포넌트를 추가해주면 된다. (Navigation을 사용하는 컴포넌트)
//    2) Nav 관련 클래스는 UnityEngine.AI 네임스페이스를 사용한다.
//    3) SetDestination() : 도착할 목표 위치 지정 함수
//    4) 세팅이 완료되면 AI를 움직이기 위한 NavMesh를 만들어줘야 한다. (NavMesh : NavAgent가 경로를 그리기 위한 바탕)
//    5) NavMesh 세팅하는 법 : Window -> AI -> Navigation -> Bake -> Bake 버튼 클릭
//              ** NavMesh는 Static 오브젝트만 Bake 할 수 있다.
//    6) 만약 지형이 바뀌었다면 다시 Bake를 진행해서 NavMesh를 다시 생성해줘야 한다.
//    7) 특정한 설정을 하지 않고 해당 AI가 물리충돌을 일으키게 된다면 충돌에 대한 물리속도에 변화가 발생하여 움직임의 변화가 발생한다. (제대로 움직이지 않는다.)
//    8) rigidbody의 velocity와 angularVelocity의 값을 0으로 세팅하면 충돌에도 물리속도 변화가 없다.