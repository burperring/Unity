using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemyMaxHP;
    public float enemyCurHP;

    private Rigidbody rigid;

    protected Player player;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        player = FindObjectOfType<Player>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            enemyCurHP -= player.equipDamage;

            // Enemy Death Motioni
            Vector3 reactVec = transform.position - collision.transform.position;

            OnDamage(reactVec);
        }
    }

    private void OnDamage(Vector3 reactVec)
    {
        if(enemyCurHP <= 0)
        {
            reactVec = reactVec.normalized;
            reactVec += Vector3.up;
            rigid.AddForce(reactVec * 5, ForceMode.Impulse);

            Destroy(gameObject, 4);
        }
    }
}
