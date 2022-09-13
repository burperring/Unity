using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    public enum Type { Misaki, UC, UCWGS, UCWTD, Yuko };
    public Type selectCharType;

    // Player set
    public float speed = 8.0f;
    private bool isMove;

    public Camera mainCamera;
    private Animator animator;
    private Vector3 destination;

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
    }

    private void PlayerMove()
    {
        if(isMove)
        {
            var dir = destination - transform.position;
            PV.RPC("PlayerLookRPC", RpcTarget.AllBuffered, dir);
            rigid.MovePosition(transform.position + dir.normalized * speed * Time.deltaTime);
        }

        if(Vector3.Distance(transform.position, destination) <= 0.1f)
        {
            isMove = false;
        }
    }

    [PunRPC]
    void PlayerLookRPC(Vector3 dir)
    {
        animator.transform.forward = dir;
    }
    #endregion
}
