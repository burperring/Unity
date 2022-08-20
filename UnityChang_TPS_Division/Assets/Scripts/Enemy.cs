using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemyMaxHP;
    public float enemyCurHP;

    // Enemy Sight Value
    [SerializeField]
    float enemySightAngle = 0f;
    [SerializeField]
    float enemySightDistance = 0f;
    [SerializeField]
    LayerMask targetLayerMask = 0;

    private bool isDead = false;
    private Rigidbody rigid;
    private Animator anim;
    private Vector3 moveVec;

    protected Player player;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        Sight();
    }

    void Sight()
    {
        Collider[] findTargetCols = Physics.OverlapSphere(transform.position, enemySightDistance, targetLayerMask);

        if(findTargetCols.Length > 0)
        {
            // Find Target and Check Pos
            Transform target = findTargetCols[0].transform;
            Vector3 targetDirection = (target.position - transform.position).normalized;

            // Check Target Angle
            float targetAngle = Vector3.Angle(targetDirection, transform.forward);
            if(targetAngle < enemySightAngle * 0.5f)
            {
                if (Physics.Raycast(transform.position, targetDirection, out RaycastHit targetHit, enemySightDistance))
                {
                    if (targetHit.collider.tag == "Player")
                    {
                        //Debug.Log(findTargetCols[0].transform.position);
                        //Debug.Log(targetDirection);
                    }
                }
            }
        }
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
            rigid.AddForce(reactVec * 2, ForceMode.Impulse);

            if (!isDead)
                anim.SetTrigger("doDead");

            isDead = true;

            Destroy(gameObject, 4);
        }
    }
}
