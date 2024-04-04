using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon.StructWrapping;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using System;

namespace Hw
{
    public class GameReadyinRoomUI : MonoBehaviour, IInRoomCallbacks
    {
        [SerializeField] Transform _playerStatusInGameReadyInRoomContent;//스크롤뷰 content 위치로 붙일 위치
        [SerializeField] PlayerStatusInGameReadyInRoomSlot[] _playerStatusInGameReadyInRoomSlotPrefab;
        private Button _ready;
        private Button _start;
        private Button _back;
        [SerializeField] private Dictionary<string, PlayerStatusInGameReadyInRoomSlot> _playerStatusInGameReadyInRoomSlots = new Dictionary<string, PlayerStatusInGameReadyInRoomSlot>();
        //UserID, 입장한 플레이어들의 레디상태를 표시해줄 슬롯을 딕셔너리로 만들어 UserID키값으로 슬롯의 정보 확인

        private void Awake()
        {
            _ready = transform.Find("Button-Ready").GetComponent<Button>();
            _start = transform.Find("Button-Start").GetComponent<Button>();
            _back = transform.Find("Button-Back").GetComponent<Button>();
            _ready.onClick.AddListener(() =>
            {
                PhotonNetwork.LocalPlayer.SetCustomProperties(
                    new Hashtable {
                    { "isReady", !(bool)PhotonNetwork.LocalPlayer.CustomProperties["isReady"] }
                    });//레디 버튼을 누르면 커스텀 프로퍼티가 수정 반대 되도록 설정
            });

            _start.onClick.AddListener(() =>
            {
                if (PhotonNetwork.IsMasterClient == false)
                    throw new System.Exception($"[gameReadyinRoomUI]: Tried to start game even i'm not a master Client");

                //모든 플레리어 준비됐는지 확인
                foreach (Player player in PhotonNetwork.PlayerList)
                {
                    if (player.IsMasterClient)//마스터 클라이언트라면 판별할 필요없음
                        continue;

                    if (player.CustomProperties.TryGetValue("isReady", out bool isReady))
                    {
                        if (isReady == false)
                            return;
                    }
                    else
                    {
                        return;
                    }
                }

                //커스텀프로퍼티를 만들어 가지고 있던 PhotonView를 저장한다.
                
                PhotonNetwork.LoadLevel("GamePlay");

            });

            _back.onClick.AddListener(() =>
            {



            });

            for (int i = 0; i < _playerStatusInGameReadyInRoomSlotPrefab.Length; i++)
            {
                _playerStatusInGameReadyInRoomSlotPrefab[i] = transform.Find($"Panel - RoomList/Scroll View/Viewport/Content/PlayerStatusInGameReadyInRoomSlot{i}").GetComponent<PlayerStatusInGameReadyInRoomSlot>();
            }
        }

        private void Start()
        {
            StartCoroutine(C_Init());
        }


        IEnumerator C_Init()
        {
            yield return new WaitUntil(() => PhotonNetwork.NetworkClientState == ClientState.Joined);//클라이언트 상태가 JOined 되면 아래 줄 실행
            yield return new WaitUntil(() => PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("isReady"));//프로퍼티 가져올때까지  대기
            _start.gameObject.SetActive(PhotonNetwork.LocalPlayer.IsMasterClient == true);//마스터 클라이언트는 _start버튼이 보이도록
            _ready.gameObject.SetActive(PhotonNetwork.LocalPlayer.IsMasterClient == false);//마스터 클라이언트는 레디버튼이 없어짐.

            Player[] players = PhotonNetwork.PlayerList;//들어갈때 플레이어 리스트를 셈



            for (int i = 0; i < players.Length; i++) //최대 프리펩 갯수 - 남은 플레이어 수 빼기
            {
                _playerStatusInGameReadyInRoomSlots.Add(players[i].UserId,
                _playerStatusInGameReadyInRoomSlotPrefab[_playerStatusInGameReadyInRoomSlotPrefab.Length - PhotonNetwork.PlayerList.Length]);

                //var slot = Instantiate(_playerStatusInGameReadyInRoomSlotPrefab, _playerStatusInGameReadyInRoomContent);//스크롤뷰에 프리펩을 배치시켜 플레이어가 들어오면 자리가 생기도록 설정
                //slot.Refresh((bool)players[i].CustomProperties["isReady"]);//들어온 플레이어들의 초기 프로퍼티 셋팅
                //_playerStatusInGameReadyInRoomSlots.Add(players[i].UserId,slot);//딕셔너리에 USserID키값과 위에 설정한 slot을 넘겨줌

            }


        }

        private void OnEnable()
        {
            PhotonNetwork.AddCallbackTarget(this); // 해당 인스턴스가 활성화 될때 콜백대상 목록추가
        }
        private void OnDisable()
        {
            PhotonNetwork.RemoveCallbackTarget(this); //해당 인스턴가 비활성화 될때 콜백대상 에서 제거
        }

        public void OnMasterClientSwitched(Player newMasterClient) //
        {
            _start.gameObject.SetActive(newMasterClient.IsLocal == true);
            _ready.gameObject.SetActive(newMasterClient.IsLocal == false);
            //방장이 나가면 방은 그대로 두고, 모든 플레이어가 레디를 풀고, 오래있었던 다음 클라이언트가 방장이 됨.
        }

        public void OnPlayerEnteredRoom(Player newPlayer)//신규 플레이어가 들어왔을때 이벤트 호출, 슬롯을 만듦
        {


            _playerStatusInGameReadyInRoomSlots.Add(newPlayer.UserId,
                _playerStatusInGameReadyInRoomSlotPrefab[_playerStatusInGameReadyInRoomSlotPrefab.Length - PhotonNetwork.PlayerList.Length]);
            //신규 플레이어가 들어오면 슬롯자리에 신규플레이어의 딕셔너리가 추가 되도록 설정


            foreach (var player in _playerStatusInGameReadyInRoomSlots)
            {
                Debug.Log(player.Key + ":" + player.Value);
            }


            //var slot = Instantiate(_playerStatusInGameReadyInRoomSlotPrefab, _playerStatusInGameReadyInRoomContent);//슬롯위치에 프리펩을 생성함
            //_playerStatusInGameReadyInRoomSlots.Add(newPlayer.UserId, slot);//딕셔너리 목록에 유저 추가

            //들어온 플레이어 PlayerStatusInGameReadyInRoomSlot의 배열의 순서만큼 배정.
            //

            StartCoroutine(C_RefreshSlot(newPlayer));//
        }

        IEnumerator C_RefreshSlot(Player player)
        {
            yield return new WaitUntil(() => player.CustomProperties.ContainsKey("isReady"));//신규플레이어가 커스텀프로퍼티를 가져올때까지 대시
            _playerStatusInGameReadyInRoomSlots[player.UserId].Refresh((bool)player.CustomProperties["isReady"]);//가져오면 초기값 false로 생성
        }


        public void OnPlayerLeftRoom(Player otherPlayer)
        {
            if (_playerStatusInGameReadyInRoomSlots.TryGetValue(otherPlayer.UserId, out PlayerStatusInGameReadyInRoomSlot slot))
            {
                //Destroy(slot.gameObject);//나가면 유저의 슬롯 없앰
                _playerStatusInGameReadyInRoomSlots.Remove(otherPlayer.UserId);//나간유저의 딕셔너리 삭제
            }
        }

        public void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps) // targerPlayer는 속성이 바뀐 유저, 변경된 속성을 담고 있는 해쉬테이블
        {
            if (_playerStatusInGameReadyInRoomSlots.TryGetValue(targetPlayer.UserId, out PlayerStatusInGameReadyInRoomSlot slot))
            {
                if (changedProps.TryGetValue("isReady", out bool value))
                {
                    slot.Refresh(value);
                }
            }
        }

        public void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
        }
    }
}


