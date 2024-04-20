using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public static PhotonManager instance
    {
        get
        {
            if(s_instance == null)
            {
                s_instance = new GameObject().AddComponent<PhotonManager>();
            }
            return s_instance;
        }
    }


    private static PhotonManager s_instance;
    public event Action onConnetedToMaster;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if(PhotonNetwork.IsConnected == false)
        {
            if (PhotonNetwork.ConnectUsingSettings())
                Debug.Log("[PhotonManager] : Connected to Photon server");
            else
                throw new Exception("[PhotonManager] : Failed to Connect to Photon server.");
        }
    }

    public void OnApplicationQuit() // OnApplicationQuit()이 호출될때 현재 플레이어가 참여중인 방을 나가게됨
    {
        PhotonNetwork.LeaveRoom();
    }


    public override void OnConnectedToMaster()//로컬 플레이어가 마스터서버에 연결될때 호출됨.
    {
        base.OnConnectedToMaster();

        onConnetedToMaster?.Invoke();

        // 다른 클라이언트들과 씬을 동기화 할 때 사용하는 프로퍼티,
        // true로 설정해줘야 PhotonNetwork.LoadLevel()이 방장외 다른 클라이언트까지 씬을 동기화 할 수 있음
        PhotonNetwork.AutomaticallySyncScene = true;

        Debug.Log("[PhotonManager] : Connected to master");
    }

    public override void OnCreatedRoom()//방 만들고 들어갔을때 호출되는 함수, OnJoinedRoom()도 같이 호출, 룸을 생성한 클라이언트에게만 호출
    {
        base.OnCreatedRoom();
        Debug.Log($"[PhotonManager] : Create room");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);

        Debug.Log($"[PhotonManager]: Failed CreateRoom Reason : {returnCode}:{message}");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        SceneManager.LoadScene("GameReady");//방장이 방을 파면 방게임씬으로 입장하도록 설정
        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable()
        {
            {"isReady",false } //방에 입장한 모든 클라이언트 들은 기본값으로 isReady의 초기설정값 false를 넣어줌
        });
        // Todo:인게임 레디로 넘어가게끔 설정= GameState.InGameReady;
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        SceneManager.LoadScene("LobbyScene");
        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable()
        {

        });//방을 나왔으니 커스텀 프로퍼티 초기화
    }


}
