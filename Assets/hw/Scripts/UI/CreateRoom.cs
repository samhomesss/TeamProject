using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CreateRoom : UI_Scene
{
    enum GameObjects
    {
        Roomname_TextField,
        MaxPlayer_Scrollbar,
        MaxPlayerValue_Text,
        Confirm_Button,
        Cancel_Button,
    }

    private TMP_InputField _roomName_TextField;
    private Scrollbar _maxPlayer_Scrollbar;
    private TMP_Text _maxPlayerValue_Text;

    private Button _confirm_Button;
    private Button _cancel_Button;

    public override void Init()
    {

        base.Init();
        Bind<GameObject>(typeof(GameObjects));

        GameObject roomName_TextField = Get<GameObject>((int)GameObjects.Roomname_TextField);
        GameObject maxPlayer_Scrollbar = Get<GameObject>((int)GameObjects.MaxPlayer_Scrollbar);
        GameObject maxPlayerValue_Text = Get<GameObject>((int)GameObjects.MaxPlayerValue_Text);

        GameObject confirm_Button = Get<GameObject>((int)GameObjects.Confirm_Button);
        GameObject cancel_Button = Get<GameObject>((int)GameObjects.Cancel_Button);

        _roomName_TextField = roomName_TextField.GetComponent<TMP_InputField>();
        _maxPlayer_Scrollbar = maxPlayer_Scrollbar.GetComponent<Scrollbar>();
        _maxPlayerValue_Text = maxPlayerValue_Text.GetComponent<TMP_Text>();
        _confirm_Button = confirm_Button.GetComponent<Button>();
        _cancel_Button = cancel_Button.GetComponent<Button>();

    }
    protected void Start()//방 만들때의 초기 UI설정
    {
        Init();

        _confirm_Button.interactable = false;
        _roomName_TextField.onValueChanged.AddListener(value => _confirm_Button.interactable = value.Length > 1);//방제목이 2글자 이상일때 활성화

        _maxPlayerValue_Text.text = (Mathf.RoundToInt(_maxPlayer_Scrollbar.value * _maxPlayer_Scrollbar.numberOfSteps + 1)).ToString();//방의 최대인원수 설정
        _maxPlayer_Scrollbar.onValueChanged.AddListener(value =>
        {
            _maxPlayerValue_Text.text = (Mathf.RoundToInt(_maxPlayer_Scrollbar.value * _maxPlayer_Scrollbar.numberOfSteps + 1)).ToString(); //스크롤바의 핸들이 움직일때 갱신
        });

        _confirm_Button.onClick.AddListener(() =>//확인버튼을 눌렀을때 방생성
        {
            RoomOptions roomOptions = new RoomOptions //룸 프로퍼티 지정
            {
                CustomRoomProperties = new ExitGames.Client.Photon.Hashtable
                {
                        { "levelLimit", 8 },//방에 프로퍼티 설정
                },
                MaxPlayers = Mathf.RoundToInt(_maxPlayer_Scrollbar.value * _maxPlayer_Scrollbar.numberOfSteps + 1),
                PublishUserId = true,//모든 플레이어들에게 보여지도록 설정
            };
            PhotonNetwork.CreateRoom(_roomName_TextField.text, roomOptions);//서버로 부터 콜백을 받아야 방을 생성할 수 있음 
        });

        _cancel_Button.onClick.AddListener(() =>
        {
            this.GetComponent<Canvas>().enabled = false;
        });
    }
}
