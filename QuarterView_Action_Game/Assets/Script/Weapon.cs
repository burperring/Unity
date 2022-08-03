using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Range };      // Melee : ����, Range : ���Ÿ�
    public Type type;
    public int damage;
    public float rate;                      // ���ݼӵ�
    public int maxAmmo;
    public int curAmmo;

    public BoxCollider meleeArea;           // �������� ����
    public TrailRenderer trailEffect;       // ���� ȿ��
    public GameObject bullet;               // ������ ������ ����
    public Transform bulletPos;             // ������ ������ ��ġ
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

    IEnumerator Swing() // ������ �Լ� Ŭ����
    {
        // yield : ����� �����ϴ� Ű����
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.3f);
        trailEffect.enabled = false;

        // �ڷ�ƾ Ż��
        yield break;
    }

    IEnumerator Shot()
    {
        // 1. Shoot Bullet
        GameObject instantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50;

        yield return null;

        // 2. Cartridge Ejection (ź�ǹ���)
        GameObject instantCase = Instantiate(bulletCase, bulletCasePos.position, bulletCasePos.rotation);
        Rigidbody caseRigid = instantCase.GetComponent<Rigidbody>();
        Vector3 caseVec = bulletCasePos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);
        caseRigid.AddForce(caseVec, ForceMode.Impulse);
        caseRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);
    }
}
