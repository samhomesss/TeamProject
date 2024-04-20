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

    public void OnApplicationQuit() // OnApplicationQuit()�� ȣ��ɶ� ���� �÷��̾ �������� ���� �����Ե�
    {
        PhotonNetwork.LeaveRoom();
    }


    public override void OnConnectedToMaster()//���� �÷��̾ �����ͼ����� ����ɶ� ȣ���.
    {
        base.OnConnectedToMaster();

        onConnetedToMaster?.Invoke();

        // �ٸ� Ŭ���̾�Ʈ��� ���� ����ȭ �� �� ����ϴ� ������Ƽ,
        // true�� ��������� PhotonNetwork.LoadLevel()�� ����� �ٸ� Ŭ���̾�Ʈ���� ���� ����ȭ �� �� ����
        PhotonNetwork.AutomaticallySyncScene = true;

        Debug.Log("[PhotonManager] : Connected to master");
    }

    public override void OnCreatedRoom()//�� ����� ������ ȣ��Ǵ� �Լ�, OnJoinedRoom()�� ���� ȣ��, ���� ������ Ŭ���̾�Ʈ���Ը� ȣ��
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
        SceneManager.LoadScene("GameReady");//������ ���� �ĸ� ����Ӿ����� �����ϵ��� ����
        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable()
        {
            {"isReady",false } //�濡 ������ ��� Ŭ���̾�Ʈ ���� �⺻������ isReady�� �ʱ⼳���� false�� �־���
        });
        // Todo:�ΰ��� ����� �Ѿ�Բ� ����= GameState.InGameReady;
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        SceneManager.LoadScene("LobbyScene");
        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable()
        {

        });//���� �������� Ŀ���� ������Ƽ �ʱ�ȭ
    }


}
