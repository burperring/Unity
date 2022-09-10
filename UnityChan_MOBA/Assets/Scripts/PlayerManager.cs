using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;
    PlayerValueSet playerValue;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
        playerValue = FindObjectOfType<PlayerValueSet>();
    }

    void Start()
    {
        if(PV.IsMine)
        {
            CreateController();
        }
    }

    void CreateController()
    {
        // Instantiate our player controller
        PhotonNetwork.Instantiate("PlayerController_" + playerValue.selectCharNum.ToString(), Vector3.zero, Quaternion.identity);
    }
}
