using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    [SerializeField]
    Transform[] wayPoints = null;
    [SerializeField]
    BoxCollider meleeArea;

    // Enemy Move Sound
    [SerializeField]
    private AudioSource walkSound;
    [SerializeField]
    private AudioSource runSound;
    [SerializeField]
    private AudioSource screamSound;

    private int count = 0;
    private float enemySpeed;
    private float missTarget = 0;
    private bool isDead = false;
    private bool isMove;
    private bool isChase;
    private bool isAttack;
    private bool isMoveSound;
    private bool doScream = false;

    private Rigidbody rigid;
    private Animator anim;

    protected Player player;

    NavMeshAgent nav = null;
    Transform target;

    private void Start()
    {
        InvokeRepeating("MovetoNextWayPoint", 0f, 2f);
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        Sight();
        enemyMove();
        MissTarget();
    }

    private void FixedUpdate()
    {
        Targetting();
    }

    void enemyMove()
    {
        if(!isAttack)
            isMove = nav.velocity == Vector3.zero ? false : true;

        anim.SetBool("isMove", isMove);
        anim.SetFloat("Speed", enemySpeed);

        if (enemySpeed == 0.5f && !isMoveSound)
            WalkSound();
        else if (enemySpeed == 4.5f && !isMoveSound)
            RunSound();
    }

    void Sight()
    {
        Collider[] findTargetCols = Physics.OverlapSphere(transform.position, enemySightDistance, targetLayerMask);

        if (findTargetCols.Length > 0)
        {
            // Find Target and Check Pos
            target = findTargetCols[0].transform;
            Vector3 targetDirection = (target.position - transform.position).normalized;

            // Check Target Angle
            float targetAngle = Vector3.Angle(targetDirection, transform.forward);
            if (targetAngle < enemySightAngle * 0.5f)
            {
                if (Physics.Raycast(transform.position, targetDirection, out RaycastHit targetHit, enemySightDistance))
                {
                    if (targetHit.collider.tag == "Player" && !isDead)
                    {
                        CancelInvoke();

                        if (!isChase && !doScream)
                            StartCoroutine(Scream());

                        if (doScream && !isAttack)
                            Chase(target);
                    }
                }
            }
            else
            {
                target = null;
            }
        }
    }

    void MissTarget()
    {
        if (target == null && isChase)
        {
            missTarget += Time.deltaTime;
            nav.speed = 0f;
            enemySpeed = 0f;
        }

        if (missTarget > 13)
        {
            isChase = false;
            doScream = false;
            missTarget = 0;
            InvokeRepeating("MovetoNextWayPoint", 0f, 2f);
        }
    }

    void WalkSound()
    {
        isMoveSound = true;
        walkSound.Play();
        StartCoroutine(MoveSoundOut());
    }

    void RunSound()
    {
        isMoveSound = true;
        runSound.Play();
        StartCoroutine(MoveSoundOut());
    }

    IEnumerator MoveSoundOut()
    {
        if (enemySpeed == 0.5f)
        {
            yield return new WaitForSeconds(2f);
            isMoveSound = false;
        }
        else if(enemySpeed == 4.5f)
        {
            yield return new WaitForSeconds(0.33f);
            isMoveSound = false;
        }
    }

    IEnumerator Scream()
    {
        isChase = true;
        isMoveSound = false;
        missTarget = 0;
        nav.speed = 0;
        enemySpeed = 0;
        screamSound.Play();
        anim.SetTrigger("doScream");
        yield return new WaitForSeconds(2.5f);

        doScream = true;
    }

    void Chase(Transform target)
    {
        nav.speed = 4.5f;
        enemySpeed = 4.5f;
        nav.SetDestination(target.position);
    }

    void MovetoNextWayPoint()
    {
        if (wayPoints.Length == 0)
            return;

        if (nav.velocity == Vector3.zero)
        {
            nav.speed = 0.5f;
            enemySpeed = 0.5f;
            nav.SetDestination(wayPoints[count++].position);

            if (count >= wayPoints.Length)
                count = 0;
        }
        
    }

    private void Targetting()
    {
        float targetRadius = 1f;
        float targetRange = 0.6f;

        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, targetRadius,
            transform.forward, targetRange, LayerMask.GetMask("Player"));

        if (rayHits.Length > 0 && !isAttack && !isDead)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        if (!isAttack)
        {
            isAttack = true;
            isChase = false;
            isMove = false;
            anim.SetTrigger("doAttack");

            nav.speed = 0f;
            enemySpeed = 0f;

            yield return new WaitForSeconds(0.5f);
            meleeArea.enabled = true;

            yield return new WaitForSeconds(1.4f);
            meleeArea.enabled = false;

            yield return new WaitForSeconds(0.1f);

            isChase = true;
            isMove = true;
            isAttack = false;
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

            // Enemy Dead Stop All
            nav.speed = 0;
            StopAllCoroutines();
            CancelInvoke();

            if (!isDead)
                anim.SetTrigger("doDead");

            isDead = true;

            Destroy(gameObject, 4);
        }
    }
}
