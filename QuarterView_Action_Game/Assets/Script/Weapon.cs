using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Range };      // Melee : 근접, Range : 원거리
    public Type type;
    public int damage;
    public float rate;                      // 공격속도
    public int maxAmmo;
    public int curAmmo;

    public BoxCollider meleeArea;           // 근접공격 범위
    public TrailRenderer trailEffect;       // 공격 효과
    public GameObject bullet;               // 생성할 프리펩 변수
    public Transform bulletPos;             // 프리펩 생성할 위치
    public GameObject bulletCase;
    public Transform bulletCasePos;

    public void Use()
    {
        if (type == Type.Melee)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }
        else if(type == Type.Range && curAmmo > 0)
        {
            curAmmo--;
            StartCoroutine("Shot");
        }
    }

    IEnumerator Swing() // 열거형 함수 클래스
    {
        // yield : 결과를 전달하는 키워드
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.3f);
        trailEffect.enabled = false;

        // 코루틴 탈출
        yield break;
    }

    IEnumerator Shot()
    {
        // 1. Shoot Bullet
        GameObject instantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50;

        yield return null;

        // 2. Cartridge Ejection (탄피배출)
        GameObject instantCase = Instantiate(bulletCase, bulletCasePos.position, bulletCasePos.rotation);
        Rigidbody caseRigid = instantCase.GetComponent<Rigidbody>();
        Vector3 caseVec = bulletCasePos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);
        caseRigid.AddForce(caseVec, ForceMode.Impulse);
        caseRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);
    }
}
