using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatusInGameReadyInRoomSlot : MonoBehaviour
{
    private TMP_Text _status;

    public void Refresh(bool isReady)
    {
        _status.enabled = isReady;
    }


    //Todo: �����Ҷ� ����ĳ���� �ְ�, �غ� �ִϸ��̼� ����

    private void Awake()
    {
        _status = transform.Find("Text (TMP) - Status").GetComponent<TMP_Text>();  
    }
}
