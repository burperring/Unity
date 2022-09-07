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
    public TMP_Text listText;
    public TMP_Text roomInfoText;
    public TMP_Text[] chatText;
    public TMP_InputField chatInput;

    [Header("ETC")]
    public TMP_Text statusText;
    public PhotonView PV;

    List<RoomInfo> myList = new List<RoomInfo>();
    int currentPage = 1, maxPage, multiple;

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
        lobbyInfoText.text = (PhotonNetwork.CountOfPlayers - PhotonNetwork.CountOfPlayersInRooms) + "로비 /" + PhotonNetwork.CountOfPlayers + "접속";
    }

    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster() => PhotonNetwork.JoinLobby();

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
        RoomRenewal();
        chatInput.text = "";

        for(int i = 0; i < chatText.Length; i++)
        {
            chatText[i].text = "";
        }
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
        ChatRPC("<color=yellow>" + newPlayer.NickName + "님이 참가했습니다</color>");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RoomRenewal();
        ChatRPC("<color=yellow>" + otherPlayer.NickName + "님이 퇴장하셨습니다</color>");
    }

    public void RoomRenewal()
    {
        listText.text = "";

        for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            listText.text += PhotonNetwork.PlayerList[i].NickName + ((i + 1 == PhotonNetwork.PlayerList.Length) ? "" : ", ");
        }

        roomInfoText.text = PhotonNetwork.CurrentRoom.Name + " / " + PhotonNetwork.CurrentRoom.PlayerCount + "명 / 최대 " + PhotonNetwork.CurrentRoom.MaxPlayers + "명";
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
