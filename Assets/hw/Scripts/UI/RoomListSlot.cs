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

        public int roomIndex; //�������� �ε���.

        [Header("������ ����")]
        public float itemHeight;        // ��ũ�� �信 �߰��� �� �� �������� �������� ���� ��.

        private TMP_Text _roomName;
        private TMP_Text _playerRatio;
        private Button _selectButton;//��Ŭ�������� �̺�Ʈ ����

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


        // �������� ��ũ�� �信 �߰��ǰ� ���� ���� ���� 0�̵Ǵ� ������ �־� �̸� �ʱ�ȭ�ϱ� ���� �Լ�.
        public void Reset()
        {
            RectTransform rect = GetComponent<RectTransform>();
            rect.localScale = Vector3.one;
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, itemHeight);
        }
    }
