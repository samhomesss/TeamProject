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


    //Todo: 레디할때 게임캐릭터 넣고, 준비 애니메이션 실행

    private void Awake()
    {
        _status = transform.Find("Text (TMP) - Status").GetComponent<TMP_Text>();  
    }
}
