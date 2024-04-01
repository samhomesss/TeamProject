using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ChattingManager : MonoBehaviourPun
{
    [SerializeField] private TMP_InputField _inputchat;
    [SerializeField] private Transform _trContent;
    [SerializeField] private TMP_Text[] _chatText;

    [SerializeField] private UnityEngine.UI.Button _extend_Button;
    [SerializeField] private RectTransform _scrollView_RectTransform;

    private bool _extend_ButtonEnabled = false;

    public void Onsubmit(string s)
    {
        if (string.IsNullOrWhiteSpace(s))
            return;

        photonView.RPC("RpcAddChat", RpcTarget.All,$"{PhotonNetwork.LocalPlayer.NickName} : {_inputchat.text}");
        _inputchat.text = "";

        _inputchat.ActivateInputField();

    }


    private void Start()
    {

        _inputchat = transform.Find($"Panel - ChatingVariable/ChattingInput").GetComponent<TMP_InputField>();
        //onSubmit�� inputField�� ������Ƽ ���͸� ������ ȣ��ǵ��ϼ���
        _inputchat.onSubmit.AddListener(Onsubmit);

        for (int i = 0; i < _chatText.Length; i++)
        {
            _chatText[i] = transform.Find($"Panel - ChatingVariable/ScrollView/Viewport/Content/Chatitem_{i}").GetComponent<TMP_Text>();
        }
        _trContent = transform.Find($"Panel - ChatingVariable/ScrollView/Viewport/Content").GetComponent<Transform>();
        _extend_Button = transform.Find($"Panel - ChatingVariable/ExtendButton").GetComponent<UnityEngine.UI.Button>();
        _scrollView_RectTransform = transform.Find($"Panel - ChatingVariable/ScrollView").GetComponent<RectTransform>();



        _extend_Button.onClick.AddListener(() => {
            _extend_ButtonEnabled = !_extend_ButtonEnabled;

            if (_extend_ButtonEnabled)
            {
                _scrollView_RectTransform.offsetMin = new Vector2(_scrollView_RectTransform.offsetMin.x, 80);
                // bottom ����
                _scrollView_RectTransform.offsetMax = new Vector2(_scrollView_RectTransform.offsetMax.x, 450);
                // Top ����
            }
            else
            {
                _scrollView_RectTransform.offsetMin = new Vector2(_scrollView_RectTransform.offsetMin.x, 80);
                // bottom ����
                _scrollView_RectTransform.offsetMax = new Vector2(_scrollView_RectTransform.offsetMax.x, 0);
                // Top ����
            }
        });

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
