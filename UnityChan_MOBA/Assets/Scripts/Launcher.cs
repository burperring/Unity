using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks
{
    // Photon Server Setting
    // Photon -> PhotonUnityNetworking -> Resources -> Photon Server Settings\

    public static Launcher Instance;

    public TMP_Dropdown dropdown;

    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_Text errorText;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListPrefab;
    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject playerListPrefab;
    [SerializeField] GameObject startGameButton;
    [SerializeField] Sprite[] selectCharacterImg;
    [SerializeField] Image clientCharacterImg;
    [SerializeField] Image userCharacterImg;

    List<RoomInfo> myList = new List<RoomInfo>();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Debug.Log("Connecting to Master");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu("title");
        Debug.Log("Joined Lobby");
        PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000");
    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameInputField.text))
            return;

        PhotonNetwork.CreateRoom(roomNameInputField.text);
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu("room");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        // If create room -> Create player prefabs
        Player[] players = PhotonNetwork.PlayerList;

        foreach(Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < players.Length; i++)
        {
            Instantiate(playerListPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }

        // Only client control game start
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Room Creation Fail: " + message;
        MenuManager.Instance.OpenMenu("error");
    }

    public void PlayerCharacterImg()
    {
        switch(dropdown.value)
        {
            case 0:
                if (PhotonNetwork.IsMasterClient)
                    clientCharacterImg.sprite = selectCharacterImg[0];
                else
                    userCharacterImg.sprite = selectCharacterImg[0];
                break;
            case 1:
                if (PhotonNetwork.IsMasterClient)
                    clientCharacterImg.sprite = selectCharacterImg[1];
                else
                    userCharacterImg.sprite = selectCharacterImg[1];
                break;
            case 2:
                if (PhotonNetwork.IsMasterClient)
                    clientCharacterImg.sprite = selectCharacterImg[2];
                else
                    userCharacterImg.sprite = selectCharacterImg[2];
                break;
            case 3:
                if (PhotonNetwork.IsMasterClient)
                    clientCharacterImg.sprite = selectCharacterImg[3];
                else
                    userCharacterImg.sprite = selectCharacterImg[3];
                break;
            case 4:
                if (PhotonNetwork.IsMasterClient)
                    clientCharacterImg.sprite = selectCharacterImg[4];
                else
                    userCharacterImg.sprite = selectCharacterImg[4];
                break;
            case 5:
                if (PhotonNetwork.IsMasterClient)
                    clientCharacterImg.sprite = selectCharacterImg[5];
                else
                    userCharacterImg.sprite = selectCharacterImg[5];
                break;
            case 6:
                if (PhotonNetwork.IsMasterClient)
                    clientCharacterImg.sprite = selectCharacterImg[6];
                else
                    userCharacterImg.sprite = selectCharacterImg[6];
                break;
        }
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("loading");
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("title");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // Do update destroy all room list
        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }

        // Recreate room list
        for (int i = 0; i < roomList.Count; i++)
        {
            if (!roomList[i].RemovedFromList)
            {
                if (!myList.Contains(roomList[i]))
                    myList.Add(roomList[i]);
                else
                    myList[myList.IndexOf(roomList[i])] = roomList[i];
            }

            Instantiate(roomListPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerListPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }
}
