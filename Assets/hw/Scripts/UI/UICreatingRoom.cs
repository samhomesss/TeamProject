using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Hw
{
    public class UICreatingRoom : MonoBehaviour
    {
        private TMP_InputField _roomName;
         private Scrollbar _maxPlayer;
         private TMP_Text _maxPlayerValue;

         private Button _confirm;
         private Button _cancel;


        protected void Awake()//방 만들때의 초기 UI설정
        {
            _roomName = transform.Find("Panel/InputField (TMP) - RoomName").GetComponent<TMP_InputField>();//방이름 설정
            _maxPlayer = transform.Find("Panel/Scrollbar - MaxPlayer").GetComponent<Scrollbar>();//방의 최대인원을 설정할 스크롤바 설정
            _maxPlayerValue = transform.Find("Panel/Text (TMP) - MaxPlayerValue").GetComponent<TMP_Text>();//방인원을 표시해줄 텍스트 설정
            _confirm = transform.Find("Panel/Button - Confirm").GetComponent<Button>();//확인버튼 설정
            _cancel = transform.Find("Panel/Button - Cancel").GetComponent<Button>();//취소버튼 설정



            _confirm.interactable = false;
            _roomName.onValueChanged.AddListener(value => _confirm.interactable = value.Length > 1);//방제목이 2글자 이상일때 활성화

            _maxPlayerValue.text = (Mathf.RoundToInt(_maxPlayer.value * _maxPlayer.numberOfSteps+1)).ToString();//방의 최대인원수 설정
            _maxPlayer.onValueChanged.AddListener(value =>
            {
                _maxPlayerValue.text = (Mathf.RoundToInt(_maxPlayer.value * _maxPlayer.numberOfSteps + 1)).ToString(); //스크롤바의 핸들이 움직일때 갱신
            });

            _confirm.onClick.AddListener(() =>//확인버튼을 눌렀을때 방생성
            {
                RoomOptions roomOptions = new RoomOptions
                {
                    CustomRoomProperties = new ExitGames.Client.Photon.Hashtable
                    {
                        { "levelLimit", 10 },//방에 프로퍼티 설정
                    },
                    MaxPlayers = Mathf.RoundToInt(_maxPlayer.value * _maxPlayer.numberOfSteps + 1),
                    PublishUserId = true,//모든 플레이어들에게 보여지도록 설정
            };
            PhotonNetwork.CreateRoom(_roomName.text, roomOptions);//서버로 부터 콜백을 받아야 방을 생성할 수 있음 
                //로비종료를 여러개사용할 때 TypeLobby로 설정
            });
            _cancel.onClick.AddListener(() =>
            {
                this.GetComponent<Canvas>().enabled = false;
            });
        }
    }
} 