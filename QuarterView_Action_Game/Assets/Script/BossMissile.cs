using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMissile : Bullet
{
    public Transform target;
    public int missileHp;
    NavMeshAgent nav;
    MeshRenderer[] meshs;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        meshs = GetComponentsInChildren<MeshRenderer>();
    }

    private void Update()
    {
        nav.SetDestination(target.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Melee")
        {
            Weapon weapon = other.GetComponent<Weapon>();
            missileHp -= weapon.damage;

            StartCoroutine(OnDamage());
        }
        else if (other.tag == "Bullet")
        {
            Bullet bullet = other.GetComponent<Bullet>();
            missileHp -= bullet.damage;

            Destroy(other.gameObject);
            StartCoroutine(OnDamage());
        }
    }

    IEnumerator OnDamage()
    {
        foreach (MeshRenderer mesh in meshs)
            mesh.material.color = Color.gray;

        yield return new WaitForSeconds(0.1f);

        if (missileHp > 0)
        {
            foreach (MeshRenderer mesh in meshs)
                mesh.material.color = Color.white;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
