using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using Hashtable = ExitGames.Client.Photon.Hashtable;

// Hashtable can use value
// byte, boolean, short, int, long, float, double, string, byte-array, int-array
// <type>array, hashtable, dictionary, Vector2, Vector3, Quaternion, PhotonPlayer

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [Header("DisconnectPanel")]
    public GameObject disConnectPanel;
    public TMP_InputField nickNameInput;

    [Header("LobbyPanel")]
    public GameObject lobbyPanel;
    public TMP_InputField roomInput;
    public TMP_Text lobbyInfoText;
    public Button[] cellBtn;
    public Button previousBtn;
    public Button nextBtn;

    [Header("RoomPanel")]
    public GameObject roomPanel;
    public GameObject gameStartBtn;
    public GameObject clientReady;
    public GameObject nonClientReady;
    public TMP_Text listText;
    public TMP_Text roomInfoText;
    public TMP_Text clientNameText;
    public TMP_Text nonClientNameText;
    public TMP_Text[] chatText;
    public TMP_InputField chatInput;
    public Image clientCharImg;
    public Image nonClientCharImg;

    [Header("CharacterSelectPanel")]
    public GameObject chSelectPanel;

    [Header("SetValue")]
    public Sprite[] CharacterImg;
    public int iClientSelectChar;
    public int iNonClientSelectChar;
    public bool isClientReady;
    public bool isNonClientReady;

    [Header("ETC")]
    public TMP_Text statusText;
    public PhotonView PV;
    public PlayerValueSet playerValue;

    List<RoomInfo> myList = new List<RoomInfo>();
    int currentPage = 1, maxPage, multiple;

    Hashtable SC;

    #region Roomlist update
    // Pervious : -2, Next : -1
    public void MyListClick(int num)
    {
        if (num == -2)
            --currentPage;
        else if (num == -1)
            ++currentPage;
        else
            PhotonNetwork.JoinRoom(myList[multiple + num].Name);
    }

    void MyListRenewal()
    {
        // Max page
        maxPage = (myList.Count % cellBtn.Length == 0) ? myList.Count / cellBtn.Length : myList.Count / cellBtn.Length + 1;

        // Previous, Next button
        previousBtn.interactable = (currentPage <= 1) ? false : true;
        nextBtn.interactable = (currentPage >= maxPage) ? false : true;

        // Set room list
        multiple = (currentPage - 1) * cellBtn.Length;
        for (int i = 0; i < cellBtn.Length; i++)
        {
            cellBtn[i].interactable = (multiple + i < myList.Count) ? true : false;
            cellBtn[i].transform.GetChild(0).GetComponent<TMP_Text>().text =
                (multiple + i < myList.Count) ? myList[multiple + i].Name : "";
            cellBtn[i].transform.GetChild(1).GetComponent<TMP_Text>().text =
                (multiple + i < myList.Count) ? myList[multiple + i].PlayerCount + "/" + myList[multiple + i].MaxPlayers : "";
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        int roomCount = roomList.Count;

        for (int i = 0; i < roomCount; i++)
        {
            if (!roomList[i].RemovedFromList)
            {
                if (!myList.Contains(roomList[i]))
                    myList.Add(roomList[i]);
                else
                    myList[myList.IndexOf(roomList[i])] = roomList[i];
            }
            else if (myList.IndexOf(roomList[i]) != -1)
                myList.RemoveAt(myList.IndexOf(roomList[i]));
        }
        MyListRenewal();
    }
    #endregion

    #region Server connet
    void Update()
    {
        statusText.text = PhotonNetwork.NetworkClientState.ToString();
        lobbyInfoText.text = (PhotonNetwork.CountOfPlayers - PhotonNetwork.CountOfPlayersInRooms) + "�κ� /" + PhotonNetwork.CountOfPlayers + "����";
    }

    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        disConnectPanel.SetActive(false);
        lobbyPanel.SetActive(true);
        roomPanel.SetActive(false);
        PhotonNetwork.LocalPlayer.NickName = nickNameInput.text;
        myList.Clear();
    }

    public void DisConnect() => PhotonNetwork.Disconnect();

    public override void OnDisconnected(DisconnectCause cause)
    {
        disConnectPanel.SetActive(true);
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(false);
    }
    #endregion

    #region Room
    public void CreateRoom() => 
        PhotonNetwork.CreateRoom(roomInput.text == "" ? "Room" + Random.Range(0, 100).ToString("000") : roomInput.text, new RoomOptions { MaxPlayers = 2 });

    public void JoinRandomRoom() => PhotonNetwork.JoinRandomRoom();

    public void LeaveRoom() => PhotonNetwork.LeaveRoom();

    public override void OnJoinedRoom()
    {
        disConnectPanel.SetActive(false);
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);

        gameStartBtn.SetActive(PhotonNetwork.IsMasterClient);

        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { "SelectCharacter", 5 }});

        RoomRenewal();
        chatInput.text = "";

        for(int i = 0; i < chatText.Length; i++)
        {
            chatText[i].text = "";
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        gameStartBtn.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        roomInput.text = "";
        CreateRoom();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        roomInput.text = "";
        CreateRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        RoomRenewal();
        ChatRPC("<color=yellow>" + newPlayer.NickName + "���� �����߽��ϴ�</color>");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RoomRenewal();
        ChatRPC("<color=yellow>" + otherPlayer.NickName + "���� �����ϼ̽��ϴ�</color>");
    }

    public void RoomRenewal()
    {
        int setNum;

        SetDefault();

        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            listText.text += PhotonNetwork.PlayerList[i].NickName + ((i + 1 == PhotonNetwork.PlayerList.Length) ? "" : ", ");
        }

        PV.RPC("SetNicnameRPC", RpcTarget.All, PhotonNetwork.LocalPlayer.NickName, PhotonNetwork.IsMasterClient);

        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            PV.RPC("SetCharacterPRC", RpcTarget.All, setNum = PhotonNetwork.IsMasterClient ? iClientSelectChar : iNonClientSelectChar, PhotonNetwork.IsMasterClient);
        }

        roomInfoText.text = PhotonNetwork.CurrentRoom.Name + " / " + PhotonNetwork.CurrentRoom.PlayerCount + "�� / �ִ� " + PhotonNetwork.CurrentRoom.MaxPlayers + "��";
    }

    public void SetDefault()
    {
        listText.text = "";
        clientNameText.text = "";
        nonClientNameText.text = "";
        clientCharImg.sprite = CharacterImg[5];
        nonClientCharImg.sprite = CharacterImg[5];
        iClientSelectChar = 5;
        iNonClientSelectChar = 5;
        playerValue.selectCharNum = 5;
        isClientReady = false;
        isNonClientReady = false;
        clientReady.SetActive(false);
        nonClientReady.SetActive(false);
    }

    [PunRPC]
    public void SetNicnameRPC(string msg, bool isClient)
    {
        if (isClient)
            clientNameText.text = msg;
        else
            nonClientNameText.text = msg;
    }

    public void StartGame()
    {
        if (!isClientReady || !isNonClientReady)
        {
            ChatRPC("��� �غ���� �ʾҽ��ϴ�.");
            return;
        }

        PhotonNetwork.LoadLevel(1);
    }

    public void CharacterPanelOpen()
    {
        chSelectPanel.SetActive(true);
    }
    #endregion

    #region Character select & Ready
    public void CloseCharacterPanel()
    {
        chSelectPanel.SetActive(false);
    }

    public void SelectCharacter(int num)
    {
        // SC : SelectCharacter, IsReady
        SC = PhotonNetwork.LocalPlayer.CustomProperties;

        SC["SelectCharacter"] = num;
        playerValue.selectCharNum = num;

        PV.RPC("SetCharacterPRC", RpcTarget.All, SC["SelectCharacter"], PhotonNetwork.IsMasterClient);
    }

    public void SetReady()
    {
        if (playerValue.selectCharNum == 5)
        {
            ChatRPC(PhotonNetwork.LocalPlayer.NickName + "�� ĳ���Ͱ� ���õ��� �ʾҽ��ϴ�.");
            return;
        }

        PV.RPC("SetPlayerReadyRPC", RpcTarget.All, PhotonNetwork.IsMasterClient);
    }

    [PunRPC]
    public void SetCharacterPRC(int num, bool isClient)
    {
        if (isClient)
        {
            iClientSelectChar = num;
            clientCharImg.sprite = CharacterImg[num];
        }
        else
        {
            iNonClientSelectChar = num;
            nonClientCharImg.sprite = CharacterImg[num];
        }
    }

    [PunRPC]
    public void SetPlayerReadyRPC(bool isClient)
    {
        if (isClient)
        {
            isClientReady = true;
            clientReady.SetActive(true);
        }
        else
        {
            isNonClientReady = true;
            nonClientReady.SetActive(true);
        }
    }
    #endregion

    #region Chat setting
    public void Send()
    {
        PV.RPC("ChatRPC", RpcTarget.All, PhotonNetwork.NickName + ":" + chatInput.text);
        chatInput.text = "";
    }

    [PunRPC] // RPC is send all players for in room
    void ChatRPC(string msg)
    {
        bool isInput = false;

        for(int i = 0; i < chatText.Length; i++)
        {
            if(chatText[i].text == "")
            {
                isInput = true;
                chatText[i].text = msg;
                break;
            }
        }

        if(!isInput) // Is chatting full move up 1 block
        {
            for(int i = 1; i < chatText.Length; i++)
            {
                chatText[i - 1].text = chatText[i].text;
            }
            chatText[chatText.Length - 1].text = msg;
        }
    }
    #endregion
}