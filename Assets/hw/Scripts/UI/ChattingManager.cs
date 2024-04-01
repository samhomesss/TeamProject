using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChattingManager : MonoBehaviourPun
{
    [SerializeField] private TMP_InputField _inputchat;
    [SerializeField] private Transform _trContent;
    [SerializeField] private TMP_Text[] _chatText;

    [SerializeField] private ScrollRect _scrollRect;
    public void Onsubmit(string s)
    {

        photonView.RPC("RpcAddChat", RpcTarget.All,$"{PhotonNetwork.LocalPlayer.NickName} : {_inputchat.text}");

        _inputchat.text = "";


        _scrollRect.verticalNormalizedPosition = 0f;

        _inputchat.ActivateInputField();

    }


    private void Start()
    {
        _scrollRect = GetComponent<ScrollRect>();
        //onSubmit은 inputField의 프로퍼티 엔터를 누르면 호출되도록설정
        _inputchat.onSubmit.AddListener(Onsubmit);

        for(int i = 0; i < _chatText.Length; i++)
        {
            _chatText[i] = transform.Find($"Panel - Chatingtest/Scroll View/Viewport/Content/Chatitem_{i}").GetComponent<TMP_Text>();
        }

        _scrollRect = transform.Find($"Panel - Chatingtest").GetComponent<ScrollRect>(); 

    }

    [PunRPC]
    void RpcAddChat(string msg)
    {
        bool isInput = false;
        for(int i =0; i< _chatText.Length; i++)
        {
            if (_chatText[i].text == "")
            {
                isInput = true;
                _chatText[i].text = msg;
                break;
            }
        }
        if (!isInput)
        {
            for(int i = 1; i < _chatText.Length; i++)
            {
                _chatText[i - 1].text = _chatText[i].text;
            }
            _chatText[_chatText.Length - 1].text = msg;
        }
    }

}
