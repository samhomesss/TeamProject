using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


    public class RoomListSlot : UI_Scene
    {
        enum GameObjects
        {
            RoomName,
            PlayerRatio,
        }

        public int roomIndex; //방정보의 인덱스.

        [Header("아이템 높이")]
        public float itemHeight;        // 스크롤 뷰에 추가할 때 이 아이템이 가져야할 높이 값.

        private TMP_Text _roomName;
        private TMP_Text _playerRatio;
        private Button _selectButton;//방클릭했을때 이벤트 설정

        //public event UnityAction onSelect
        //{
        //    add
        //    {
        //        _select.onClick.AddListener(value);
        //    }
        //    remove
        //    {
        //        _select.onClick.RemoveListener(value);
        //    }
        //}

        public void Refresh(string roomName, int currentPlayersInRoom, int maxPlayerRoom)
        {
            _roomName.text = roomName;
            _playerRatio.text = $"{currentPlayersInRoom} / {maxPlayerRoom}";
        }

        public override void Init()
        {
            base.Init();
            Bind<GameObject>(typeof(GameObjects));

            GameObject roomname = Get<GameObject>((int)GameObjects.RoomName);
            GameObject playerRatio = Get<GameObject>((int)GameObjects.PlayerRatio);

            _roomName = roomname.GetComponent<TMP_Text>();
            _playerRatio = playerRatio.GetComponent<TMP_Text>();
        }

        private void Awake()
        {
        _selectButton = GetComponent<Button>();
        _selectButton.onClick.AddListener(() =>
        {
            //PhotonNetwork.JoinRoom();
        });
        }


        // 아이템이 스크롤 뷰에 추가되고 나면 높이 값이 0이되는 문제가 있어 이를 초기화하기 위한 함수.
        public void Reset()
        {
            RectTransform rect = GetComponent<RectTransform>();
            rect.localScale = Vector3.one;
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, itemHeight);
        }
    }
