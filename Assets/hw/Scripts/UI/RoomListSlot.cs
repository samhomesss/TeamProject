using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RoomListSlot : MonoBehaviour
{
    public int roomIndex; //�������� �ε���
    private TMP_Text _roomName;
    private TMP_Text _playerRatio;
    private Button _select;//��Ŭ�������� �̺�Ʈ ����
    public event UnityAction onSelect
    {
        add
        {
            _select.onClick.AddListener(value);
        }
        remove
        {
            _select.onClick.RemoveListener(value);
        }
    }

    public void Refresh(string roomName,int currentPlayersInRoom,int maxPlayerRoom)
    {
        _roomName.text = roomName;
        _playerRatio.text = $"{currentPlayersInRoom} / {maxPlayerRoom}";
    }

    private void Awake()
    {
        _select = GetComponent<Button>();
        _roomName = transform.Find("Text (TMP) - RoomName").GetComponent<TMP_Text>();
        _playerRatio = transform.Find("Text (TMP) - PlayerRatio").GetComponent <TMP_Text>();
    }
}
