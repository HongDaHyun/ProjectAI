using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;


public class NetworkManager : MonoBehaviourPunCallbacks
{
    public NetWork network;
    GameManager gameManager;

    List<RoomInfo> myList = new List<RoomInfo>();
    int currentPage = 1, maxPage, multiple;

    #region 방리스트 갱신
    public void MyListClick(int num)
    {
        if (num == -2) --currentPage;
        else if (num == -1) ++currentPage;
        else PhotonNetwork.JoinRoom(myList[multiple + num].Name);
        MyListRenewal();
    }

    void MyListRenewal()
    {
        // 최대페이지
        maxPage = (myList.Count % network.roomBtn.Length == 0) ? myList.Count / network.roomBtn.Length : myList.Count / network.roomBtn.Length + 1;

        // 이전, 다음버튼
        network.previousBtn.interactable = (currentPage <= 1) ? false : true;
        network.nextBtn.interactable = (currentPage >= maxPage) ? false : true;

        // 페이지에 맞는 리스트 대입
        multiple = (currentPage - 1) * network.roomBtn.Length;
        for (int i = 0; i < network.roomBtn.Length; i++)
        {
            network.roomBtn[i].interactable = (multiple + i < myList.Count) ? true : false;
            network.roomBtn[i].transform.GetChild(0).GetComponent<Text>().text = (multiple + i < myList.Count) ? myList[multiple + i].Name : "";
            network.roomBtn[i].transform.GetChild(1).GetComponent<Text>().text = (multiple + i < myList.Count) ? myList[multiple + i].PlayerCount + "/" + myList[multiple + i].MaxPlayers : "";
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        int roomCount = roomList.Count;
        for (int i = 0; i < roomCount; i++)
        {
            if (!roomList[i].RemovedFromList)
            {
                if (!myList.Contains(roomList[i])) myList.Add(roomList[i]);
                else myList[myList.IndexOf(roomList[i])] = roomList[i];
            }
            else if (myList.IndexOf(roomList[i]) != -1) myList.RemoveAt(myList.IndexOf(roomList[i]));
        }
        MyListRenewal();
    }
    #endregion


    #region 서버연결
    private void Awake()
    {
        gameManager = GameManager.Instance;
        Screen.SetResolution(960, 540, false);
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
    }

    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster() => PhotonNetwork.JoinLobby();

    public override void OnJoinedLobby()
    {
        network.lobbyPannel.SetActive(true);
        network.roomPannel.SetActive(false);
        PhotonNetwork.LocalPlayer.NickName = network.nickNameInput.text;
        myList.Clear();
    }

    public void Disconnect() => PhotonNetwork.Disconnect();

    public override void OnDisconnected(DisconnectCause cause)
    {
        network.lobbyPannel.SetActive(false);
        network.roomPannel.SetActive(false);
    }
    #endregion


    #region 방
    public void CreateRoom() => PhotonNetwork.CreateRoom(network.roomNameInput.text == "" ? "Room" + Random.Range(0, 100) : network.roomNameInput.text, new RoomOptions { MaxPlayers = 2 });

    public void JoinRandomRoom() => PhotonNetwork.JoinRandomRoom();

    public void LeaveRoom() => PhotonNetwork.LeaveRoom();

    public override void OnJoinedRoom()
    {
        network.lobbyPannel.SetActive(false);
        network.roomPannel.SetActive(true);
        RoomRenewal();
    }

    public override void OnCreateRoomFailed(short returnCode, string message) { network.roomNameInput.text = ""; CreateRoom(); } 

    public override void OnJoinRandomFailed(short returnCode, string message) { network.roomNameInput.text = ""; CreateRoom(); }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        RoomRenewal();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RoomRenewal();
    }

    void RoomRenewal()
    {
        for(int i = 0; i < 2; i++)
        {
            network.player[i].text = "";
        }
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            network.player[i].text = PhotonNetwork.PlayerList[i].NickName;
        network.roomName.text = PhotonNetwork.CurrentRoom.Name;
    }
    #endregion


    #region 채팅
    //public void Send()
    //{
    //    PV.RPC("ChatRPC", RpcTarget.All, PhotonNetwork.NickName + " : " + ChatInput.text);
    //    ChatInput.text = "";
    //}

    //[PunRPC] // RPC는 플레이어가 속해있는 방 모든 인원에게 전달한다
    //void ChatRPC(string msg)
    //{
    //    bool isInput = false;
    //    for (int i = 0; i < ChatText.Length; i++)
    //        if (ChatText[i].text == "")
    //        {
    //            isInput = true;
    //            ChatText[i].text = msg;
    //            break;
    //        }
    //    if (!isInput) // 꽉차면 한칸씩 위로 올림
    //    {
    //        for (int i = 1; i < ChatText.Length; i++) ChatText[i - 1].text = ChatText[i].text;
    //        ChatText[ChatText.Length - 1].text = msg;
    //    }
    //}
    #endregion

    [PunRPC]
    void StartRPC()
    {
        network.networkPannel.SetActive(false);
        gameManager.isGameOver = false;
        gameManager.soundManager.bgm.Play();
        StartCoroutine(gameManager.SecPerMoney(gameManager.secMoneySize, gameManager.secMoney));
    }

    public void StartBtn()
    {
        if (PhotonNetwork.PlayerList.Length == 2 && PhotonNetwork.IsMasterClient)
        {
            network.PV.RPC("StartRPC", RpcTarget.All);
        }
    }
}

[System.Serializable]
public struct NetWork
{
    public GameObject networkPannel;

    [Header("ServerPannel")]
    public GameObject serverPannel;
    public InputField nickNameInput;

    [Header("LobbyPannel")]
    public GameObject lobbyPannel;
    public InputField roomNameInput;
    public Button[] roomBtn;
    public Button previousBtn;
    public Button nextBtn;

    [Header("RoomPanel")]
    public GameObject roomPannel;
    public Text roomName;
    public Text[] player;

    [Header("ETC")]
    public PhotonView PV;
}
