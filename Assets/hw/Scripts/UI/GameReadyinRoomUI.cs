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
        [SerializeField] Transform _playerStatusInGameReadyInRoomContent;//��ũ�Ѻ� content ��ġ�� ���� ��ġ
        [SerializeField] PlayerStatusInGameReadyInRoomSlot[] _playerStatusInGameReadyInRoomSlotPrefab;
        private Button _ready;
        private Button _start;
        private Button _back;
        [SerializeField] private Dictionary<string, PlayerStatusInGameReadyInRoomSlot> _playerStatusInGameReadyInRoomSlots = new Dictionary<string, PlayerStatusInGameReadyInRoomSlot>();
        //UserID, ������ �÷��̾���� ������¸� ǥ������ ������ ��ųʸ��� ����� UserIDŰ������ ������ ���� Ȯ��

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
                    });//���� ��ư�� ������ Ŀ���� ������Ƽ�� ���� �ݴ� �ǵ��� ����
            });

            _start.onClick.AddListener(() =>
            {
                if (PhotonNetwork.IsMasterClient == false)
                    throw new System.Exception($"[gameReadyinRoomUI]: Tried to start game even i'm not a master Client");

                //��� �÷����� �غ�ƴ��� Ȯ��
                foreach (Player player in PhotonNetwork.PlayerList)
                {
                    if (player.IsMasterClient)//������ Ŭ���̾�Ʈ��� �Ǻ��� �ʿ����
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

                //Ŀ����������Ƽ�� ����� ������ �ִ� PhotonView�� �����Ѵ�.
                
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
            yield return new WaitUntil(() => PhotonNetwork.NetworkClientState == ClientState.Joined);//Ŭ���̾�Ʈ ���°� JOined �Ǹ� �Ʒ� �� ����
            yield return new WaitUntil(() => PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("isReady"));//������Ƽ �����ö�����  ���
            _start.gameObject.SetActive(PhotonNetwork.LocalPlayer.IsMasterClient == true);//������ Ŭ���̾�Ʈ�� _start��ư�� ���̵���
            _ready.gameObject.SetActive(PhotonNetwork.LocalPlayer.IsMasterClient == false);//������ Ŭ���̾�Ʈ�� �����ư�� ������.

            Player[] players = PhotonNetwork.PlayerList;//���� �÷��̾� ����Ʈ�� ��



            for (int i = 0; i < players.Length; i++) //�ִ� ������ ���� - ���� �÷��̾� �� ����
            {
                _playerStatusInGameReadyInRoomSlots.Add(players[i].UserId,
                _playerStatusInGameReadyInRoomSlotPrefab[_playerStatusInGameReadyInRoomSlotPrefab.Length - PhotonNetwork.PlayerList.Length]);

                //var slot = Instantiate(_playerStatusInGameReadyInRoomSlotPrefab, _playerStatusInGameReadyInRoomContent);//��ũ�Ѻ信 �������� ��ġ���� �÷��̾ ������ �ڸ��� ���⵵�� ����
                //slot.Refresh((bool)players[i].CustomProperties["isReady"]);//���� �÷��̾���� �ʱ� ������Ƽ ����
                //_playerStatusInGameReadyInRoomSlots.Add(players[i].UserId,slot);//��ųʸ��� USserIDŰ���� ���� ������ slot�� �Ѱ���

            }


        }

        private void OnEnable()
        {
            PhotonNetwork.AddCallbackTarget(this); // �ش� �ν��Ͻ��� Ȱ��ȭ �ɶ� �ݹ��� ����߰�
        }
        private void OnDisable()
        {
            PhotonNetwork.RemoveCallbackTarget(this); //�ش� �ν��ϰ� ��Ȱ��ȭ �ɶ� �ݹ��� ���� ����
        }

        public void OnMasterClientSwitched(Player newMasterClient) //
        {
            _start.gameObject.SetActive(newMasterClient.IsLocal == true);
            _ready.gameObject.SetActive(newMasterClient.IsLocal == false);
            //������ ������ ���� �״�� �ΰ�, ��� �÷��̾ ���� Ǯ��, �����־��� ���� Ŭ���̾�Ʈ�� ������ ��.
        }

        public void OnPlayerEnteredRoom(Player newPlayer)//�ű� �÷��̾ �������� �̺�Ʈ ȣ��, ������ ����
        {


            _playerStatusInGameReadyInRoomSlots.Add(newPlayer.UserId,
                _playerStatusInGameReadyInRoomSlotPrefab[_playerStatusInGameReadyInRoomSlotPrefab.Length - PhotonNetwork.PlayerList.Length]);
            //�ű� �÷��̾ ������ �����ڸ��� �ű��÷��̾��� ��ųʸ��� �߰� �ǵ��� ����


            foreach (var player in _playerStatusInGameReadyInRoomSlots)
            {
                Debug.Log(player.Key + ":" + player.Value);
            }


            //var slot = Instantiate(_playerStatusInGameReadyInRoomSlotPrefab, _playerStatusInGameReadyInRoomContent);//������ġ�� �������� ������
            //_playerStatusInGameReadyInRoomSlots.Add(newPlayer.UserId, slot);//��ųʸ� ��Ͽ� ���� �߰�

            //���� �÷��̾� PlayerStatusInGameReadyInRoomSlot�� �迭�� ������ŭ ����.
            //

            StartCoroutine(C_RefreshSlot(newPlayer));//
        }

        IEnumerator C_RefreshSlot(Player player)
        {
            yield return new WaitUntil(() => player.CustomProperties.ContainsKey("isReady"));//�ű��÷��̾ Ŀ����������Ƽ�� �����ö����� ���
            _playerStatusInGameReadyInRoomSlots[player.UserId].Refresh((bool)player.CustomProperties["isReady"]);//�������� �ʱⰪ false�� ����
        }


        public void OnPlayerLeftRoom(Player otherPlayer)
        {
            if (_playerStatusInGameReadyInRoomSlots.TryGetValue(otherPlayer.UserId, out PlayerStatusInGameReadyInRoomSlot slot))
            {
                //Destroy(slot.gameObject);//������ ������ ���� ����
                _playerStatusInGameReadyInRoomSlots.Remove(otherPlayer.UserId);//���������� ��ųʸ� ����
            }
        }

        public void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps) // targerPlayer�� �Ӽ��� �ٲ� ����, ����� �Ӽ��� ��� �ִ� �ؽ����̺�
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


