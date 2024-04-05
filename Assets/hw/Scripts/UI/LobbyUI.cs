﻿using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hw
{
    public class LobbyUI : UI_Scene, ILobbyCallbacks
    {

        enum GameObjects
        {
            Join_Button,
            Create_Button,
            RoomList_Content
        }
        public int selectedRoomListSlotIndex
        {
            get => _selectedRoomListSlotIndex;
            set
            {
                _selectedRoomListSlotIndex = value;
                _join_Button.interactable = value >= 0;
            }
        }

        private Button _join_Button;
        private Button _create_Button;
        private Canvas _createRoom_Canvas;

        RectTransform _roomListContent;
        RoomListSlot _roomListslotPrefab;

        private List<RoomListSlot> _roomListslots = new List<RoomListSlot>();
        private int _selectedRoomListSlotIndex;
        private List<RoomInfo> _localRoomInfos;

        [SerializeField] private Toggle testLoginToggle;

        public override void Init()
        {
            base.Init();
            Bind<GameObject>(typeof(GameObjects));

            GameObject join_Button = Get<GameObject>((int)GameObjects.Join_Button);
            GameObject create_Button = Get<GameObject>((int)GameObjects.Create_Button);
            GameObject roomList_Content = Get<GameObject>((int)GameObjects.RoomList_Content);


            _join_Button = join_Button.GetComponent<Button>();
            _create_Button = create_Button.GetComponent<Button>();
            _roomListContent = roomList_Content.GetComponent<RectTransform>();
            _createRoom_Canvas = Util.FindChild(transform.parent.gameObject, "CreateRoom").GetComponent<Canvas>();//게임오브젝트를 리턴해줌
            _roomListslotPrefab = Util.FindChild(transform.parent.gameObject, "RoomListSlot").GetComponent<RoomListSlot>();
        }


        private void Awake() //테스트 옵션 설정
        {
            var lines = File.ReadAllLines("Assets/TestOption.txt");
            if (lines.Length > 0)
            {
                testLoginToggle.gameObject.SetActive(lines[0].Equals("1"));
            }
        }

        private void Start()
        {
            Init();

            _join_Button.interactable = false;
            _create_Button.interactable = false;


            _join_Button.onClick.AddListener(() =>
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
            _create_Button.onClick.AddListener(() =>
            {

                if (testLoginToggle.IsActive() && testLoginToggle.isOn)
                {
                    _createRoom_Canvas.enabled = true;

                    _createRoom_Canvas.transform.Find("Panel/Roomname_TextField").GetComponent<TMP_InputField>().text = "TestRoom";
                    _createRoom_Canvas.transform.Find("Panel/MaxPlayer_Scrollbar").GetComponent<Scrollbar>().value = 0.2f;

                    _createRoom_Canvas.transform.Find("Panel/Confirm_Button").GetComponent<Button>().onClick.Invoke();
                }
                else
                {
                    _createRoom_Canvas.enabled = true;
                }

            });
            StartCoroutine(C_JoinLobbyAttheVeryFirstTime());


        }

        IEnumerator C_JoinLobbyAttheVeryFirstTime()
        {
            yield return new WaitUntil(() => PhotonNetwork.NetworkClientState == ClientState.ConnectedToMasterServer);
            PhotonNetwork.JoinLobby();
            _join_Button.interactable = true;
            _create_Button.interactable = true;
        }
        private void OnDisable()
        {
            PhotonNetwork.RemoveCallbackTarget(this);//해당 인스턴스가 Ondisable이 될때 콜백대상 목록에서 제거
        }

        private void OnEnable()
        {
            PhotonNetwork.AddCallbackTarget(this); //PhotonNetwork interface를 상속받았으면 콜백 호출 대상으로 등록시킴
        }
        public void OnJoinedLobby()//로비에 진입 했을 때 메서드 호출
        {

            PhotonNetwork.LocalPlayer.NickName = LoginInformation.profile.nickname; //포톤네트워크의 이름을 로그인 프로필에 있는 닉네임으로 설정
            Debug.Log("joined Lobby");
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName}님 오신걸 환영합니다.");

            //채팅창 깨끗하기 하기
        }

        public void OnLeftLobby()//로비를 떠날 때 메서드 호출
        {

            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName}님이 떠나셨습니다");
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

            for (int i = _roomListslots.Count - 1; i >= 0; i--)
                Destroy(_roomListslots[i].gameObject);

            _roomListslots.Clear();


            for (int i = 0; i < roomList.Count; i++)
            {
                RoomListSlot tempSlot = Instantiate(_roomListslotPrefab, _roomListContent);//슬롯 리스트 만들고
                tempSlot.GetComponent<Canvas>().sortingOrder = 1;
                tempSlot.roomIndex = i;
                tempSlot.Refresh(roomList[i].Name, roomList[i].PlayerCount, roomList[i].MaxPlayers);//방정보 갱신
                tempSlot.onSelect += () => selectedRoomListSlotIndex = tempSlot.roomIndex;//이벤트에 사용자가 선택한 방의 인덱스를 넘겨줌

                tempSlot.Reset();
                //RectTransform slotRectTransform = tempSlot.GetComponent<RectTransform>();
                //slotRectTransform.sizeDelta = new Vector2(slotRectTransform.sizeDelta.x, tempSlot.itemHeight);
                //slotRectTransform.localScale = Vector3.one;

                _roomListslots.Add(tempSlot);//생성된 tempSlot을 룸리스트에 저장
           }
        }
    }
}