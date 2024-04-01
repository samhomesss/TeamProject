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
                //Todo:방이 없다는 팝업창 만들기
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
        PhotonNetwork.RemoveCallbackTarget(this);//해당 인스턴스가 OnDisable이 될때 콜백대상 목록에서 제거
    }

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this); //PhotonNetwork interface를 상속받았으면, 콜백 호출 대상으로 등록시킴
    }
    public void OnJoinedLobby()//로비에 진입했을 때 메서드 호출
    {

        PhotonNetwork.LocalPlayer.NickName = LoginInformation.profile.nickname; //포톤네트워크의 이름을 로그인 프로필에 있는 닉네임으로 설정
        Debug.Log("joined Lobby");
        Debug.Log($"{PhotonNetwork.LocalPlayer.NickName}님 오신걸 환영합니다");

        //채팅창 깨끗하게 하기.


    }

    public void OnLeftLobby()//로비 떠날 때 메서드 호출
    {

        Debug.Log($"{PhotonNetwork.LocalPlayer.NickName}님이 떠나셨습니다.");
        throw new System.NotImplementedException();
    }

    public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    {
        throw new System.NotImplementedException();
    }

    public void OnRoomListUpdate(List<RoomInfo> roomList)//로비에 있는 플레이어들에게 방의 상태가 바뀔때 마다 현재 방의 상태를 알려줌
    {
        _localRoomInfos = roomList;
        Debug.Log("Room List Updated....");

        for(int i = _roomListslots.Count - 1; i >= 0; i--)
            Destroy(_roomListslots[i].gameObject);

        _roomListslots.Clear();
        

        for(int i = 0; i<roomList.Count; i++)
        {

            RoomListSlot tempSlot = Instantiate(_roomListslotPrefab, _roomListContent);//슬롯리스트 만들고
            tempSlot.roomIndex = i;
            tempSlot.Refresh(roomList[i].Name, roomList[i].PlayerCount, roomList[i].MaxPlayers);//방정보 갱신
            tempSlot.onSelect += () => selectedRoomListSlotIndex = tempSlot.roomIndex;//이벤트에 사용자가 선택한 방의 인덱스를 넘겨줌

            _roomListslots.Add(tempSlot);//생성된 tempSlot을 룸리스트에 저장
        }
    }

}
