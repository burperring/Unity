using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerController : MonoBehaviour, IPunObservable
{
    public enum Type { Misaki, UC, UCWGS, UCWTD, Yuko };
    public Type selectCharType;

    [Header("Player set")]
    public float speed = 5.0f;
    public float health = 550f;
    private bool isMove;

    [Header("Object set")]
    public Camera mainCamera;
    public Image healthImg;

    private Animator animator;
    private Vector3 destination;

    Vector3 curPos;
    Rigidbody rigid;
    PhotonView PV;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();

        if (PV.IsMine)
            mainCamera = GetComponentInChildren<Camera>();
    }

    void Start()
    {
        if(!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rigid);
        }
    }

    void Update()
    {
        if (!PV.IsMine)
            return;

        SetTransformPos();
        PlayerSkill();
    }

    void FixedUpdate()
    {
        if (!PV.IsMine)
            return;

        PlayerMove();
    }

    #region Player move controller
    void SetTransformPos()
    {
        if(Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            
            if(Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                SetDestination(hit.point);
            }
        }
    }

    private void SetDestination(Vector3 dest)
    {
        destination = dest;
        isMove = true;
        animator.SetBool("isRun", true);
    }

    private void PlayerMove()
    {
        if (PV.IsMine)
        {
            if (isMove)
            {
                var dir = destination - transform.position;
                PV.RPC("PlayerLookRPC", RpcTarget.AllBuffered, dir);
                rigid.MovePosition(transform.position + dir.normalized * speed * Time.deltaTime);
            }

            if (Vector3.Distance(transform.position, destination) <= 0.1f)
            {
                isMove = false;
                animator.SetBool("isRun", false);
            }
        }
        else if ((transform.position - curPos).sqrMagnitude >= 100)
            transform.position = curPos;
        else
            transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 20);
    }

    [PunRPC]
    void PlayerLookRPC(Vector3 dir)
    {
        animator.transform.forward = dir;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();
        }
    }
    #endregion

    #region Player skill controller
    void PlayerSkill()
    {
        if (Input.GetButtonDown("SkillQ"))
        {
            UseSkillQ();
        }
        else if (Input.GetButtonDown("SkillW"))
        {
            UseSkillW();
        }
        else if (Input.GetButtonDown("SkillE"))
        {
            UseSkillE();
        }
        else if (Input.GetButtonDown("SkillR"))
        {
            UseSkillR();
        }
    }

    void UseSkillQ()
    {
        switch(selectCharType)
        {
            case Type.Misaki:
                animator.SetTrigger("doSkillQ");
                break;
            case Type.UC:
                animator.SetTrigger("doSkillQ");
                break;
            case Type.UCWGS:
                animator.SetTrigger("doSkillQ");
                break;
            case Type.UCWTD:
                animator.SetTrigger("doSkillQ");
                break;
            case Type.Yuko:
                animator.SetTrigger("doSkillQ");
                break;
        }
    }

    void UseSkillW()
    {
        switch (selectCharType)
        {
            case Type.Misaki:
                animator.SetTrigger("doSkillW");
                break;
            case Type.UC:
                animator.SetTrigger("doSkillW");
                break;
            case Type.UCWGS:
                animator.SetTrigger("doSkillW");
                break;
            case Type.UCWTD:
                animator.SetTrigger("doSkillW");
                break;
            case Type.Yuko:
                animator.SetTrigger("doSkillW");
                break;
        }
    }

    void UseSkillE()
    {
        switch (selectCharType)
        {
            case Type.Misaki:
                animator.SetTrigger("doSkillE");
                break;
            case Type.UC:
                animator.SetTrigger("doSkillE");
                break;
            case Type.UCWGS:
                animator.SetTrigger("doSkillE");
                break;
            case Type.UCWTD:
                animator.SetTrigger("doSkillE");
                break;
            case Type.Yuko:
                animator.SetTrigger("doSkillE");
                break;
        }
    }

    void UseSkillR()
    {
        switch (selectCharType)
        {
            case Type.Misaki:
                animator.SetTrigger("doSkillR");
                break;
            case Type.UC:
                animator.SetTrigger("doSkillR");
                break;
            case Type.UCWGS:
                animator.SetTrigger("doSkillR");
                break;
            case Type.UCWTD:
                animator.SetTrigger("doSkillR");
                break;
            case Type.Yuko:
                animator.SetTrigger("doSkillR");
                break;
        }
    }
    #endregion
}
