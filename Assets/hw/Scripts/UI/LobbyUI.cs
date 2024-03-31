using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour, ILobbyCallbacks
{

    public int selectedRoomListSlotIndex
    {
        get => _selectedRoomListSlotIndex;
        set
        {
            _selectedRoomListSlotIndex = value;
            _join.interactable = value >= 0;
        }
    }

    private Button _join;
    private Button _create;
    [SerializeField] RectTransform _roomListContent;
    [SerializeField] RoomListSlot _roomListslotPrefab;
    private List<RoomListSlot> _roomListslots = new List<RoomListSlot>();
    private int _selectedRoomListSlotIndex;
    private List<RoomInfo> _localRoomInfos;

    private Canvas _canvas;



    private void Awake()
    {
        _join = transform.Find("Button-Join").GetComponent<Button>();
        _create = transform.Find("Button - Create").GetComponent <Button>();
        _canvas = GameObject.Find("Canvas - CreatingRoom").GetComponent<Canvas>();
            
        _join.onClick.AddListener(() =>
        {
            if (PhotonNetwork.JoinRoom(_localRoomInfos[_selectedRoomListSlotIndex].Name)) 
            {

            }
            else
            {
                //Todo:���� ���ٴ� �˾�â �����
                Debug.Log("The room is invalid");
            }
        });
        _create.onClick.AddListener(() =>
        {
            _canvas.enabled = true;
        });
    }
    private void Start()
    {
      StartCoroutine(C_JoinLobbyAttheVeryFirstTime());   
    }

    IEnumerator C_JoinLobbyAttheVeryFirstTime()
    {
        yield return new WaitUntil(() => PhotonNetwork.NetworkClientState == ClientState.ConnectedToMasterServer);
        PhotonNetwork.JoinLobby();
    }
    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);//�ش� �ν��Ͻ��� OnDisable�� �ɶ� �ݹ��� ��Ͽ��� ����
    }

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this); //PhotonNetwork interface�� ��ӹ޾�����, �ݹ� ȣ�� ������� ��Ͻ�Ŵ
    }
    public void OnJoinedLobby()//�κ� �������� �� �޼��� ȣ��
    {

        PhotonNetwork.LocalPlayer.NickName = LoginInformation.profile.nickname; //�����Ʈ��ũ�� �̸��� �α��� �����ʿ� �ִ� �г������� ����
        Debug.Log("joined Lobby");
        Debug.Log($"{PhotonNetwork.LocalPlayer.NickName}�� ���Ű� ȯ���մϴ�");

        //ä��â �����ϰ� �ϱ�.


    }

    public void OnLeftLobby()//�κ� ���� �� �޼��� ȣ��
    {

        Debug.Log($"{PhotonNetwork.LocalPlayer.NickName}���� �����̽��ϴ�.");
        throw new System.NotImplementedException();
    }

    public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    {
        throw new System.NotImplementedException();
    }

    public void OnRoomListUpdate(List<RoomInfo> roomList)//�κ� �ִ� �÷��̾�鿡�� ���� ���°� �ٲ� ���� ���� ���� ���¸� �˷���
    {
        _localRoomInfos = roomList;
        Debug.Log("Room List Updated....");

        for(int i = _roomListslots.Count - 1; i >= 0; i--)
            Destroy(_roomListslots[i].gameObject);

        _roomListslots.Clear();
        

        for(int i = 0; i<roomList.Count; i++)
        {

            RoomListSlot tempSlot = Instantiate(_roomListslotPrefab, _roomListContent);//���Ը���Ʈ �����
            tempSlot.roomIndex = i;
            tempSlot.Refresh(roomList[i].Name, roomList[i].PlayerCount, roomList[i].MaxPlayers);//������ ����
            tempSlot.onSelect += () => selectedRoomListSlotIndex = tempSlot.roomIndex;//�̺�Ʈ�� ����ڰ� ������ ���� �ε����� �Ѱ���

            _roomListslots.Add(tempSlot);//������ tempSlot�� �븮��Ʈ�� ����
        }
    }

}
