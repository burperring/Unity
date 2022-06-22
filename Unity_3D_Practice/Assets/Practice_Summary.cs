using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Practice_Summary : MonoBehaviour
{
    // Add Component에서 주로 사용되는 Component
    //
    // 1. RigidBody : 중력 물리 효과를 받는 컴포넌트
    //    1) Mass : 수치가 높을수록 충돌이 무거워진다.
    //    2) User Gravity : 중력을 사용하는지에 대한 설정(T/F)
    //    3) Is Kinematic : 외부 물리 효과를 무시에 대한 설정(T/F)
    //
    // 2. Collider : 충돌 물리 효과를 받는 컴포넌트
    //      충돌 효과는 보이는 물체가 아닌 Collider의 영향을 받는다.
    //
    // 3. Material : 오브젝트의 표면 재질을 결정하는 컴포넌트
    //      Material을 바꾸기 위해서는 새로운 재질을 만들어줘야 한다.
    //      재질 편집은 새로 생성해서 적용해야 편집이 가능하다.
    //    1) Metalic : 금속 재질 수치
    //    2) Smoothness : 빛 반사 수치
    //    3) Texture : 재질에 들어가는 이미지
    //          Albedo 옆 작은 칸에 드래그해서 넣으면 이미지를 넣을 수 있다.
    //    4) Tiling : Texture 반복 타일 개수
    //    5) Emission : Texture 발광(밝기) 조절, 빛이 물리적으로 나오는건 아님
    // 
    // 4. Physics Material : 탄성과 마찰을 다루는 물리적인 재질 컴포넌트
    //      일반 Material과 마찬가지로 생성해서 컴포넌트를 추가해줘야 한다.
    //      컴포넌트를 추가하면 Collider에 Material로 추가된다.
    //    1) Bounciness : 탄성력, 높을수록 많이 튀어오름(0~1)
    //    2) Bounciness Combine : 다음 탄성을 계산하는 방식
    //    3) Friction : 마찰력, 낮을수록 많이 미끄러짐
    //          Static = 정지상태 마찰력, Dynamic = 움직일 때 마찰력
    //    4) Friction Combine : 다음 마찰력을 계산하는 방식
}
