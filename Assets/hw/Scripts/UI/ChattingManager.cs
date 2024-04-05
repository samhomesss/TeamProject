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
    [SerializeField] private TMP_Text _chatText;

    [SerializeField] private UnityEngine.UI.Button _extend_Button;
    [SerializeField] private RectTransform _scrollView_RectTransform;

    private bool _extend_ButtonEnabled = false;

    public void Onsubmit(string s)
    {
        if (string.IsNullOrWhiteSpace(s))
            return;

        photonView.RPC("RpcAddChat", RpcTarget.All, $"{PhotonNetwork.LocalPlayer.NickName} : {_inputchat.text}");
        _inputchat.text = "";

        _inputchat.ActivateInputField();
    }


    private void Start()
    {

        _inputchat = transform.Find($"ChatingVariable/ChattingInput").GetComponent<TMP_InputField>();
        //onSubmit은 inputField의 프로퍼티 엔터를 누르면 호출되도록설정
        _inputchat.onSubmit.AddListener(Onsubmit);

        _chatText = transform.Find($"ChatingVariable/ScrollView/Viewport/Content/Chat_list").GetComponent<TMP_Text>();
        _trContent = transform.Find($"ChatingVariable/ScrollView/Viewport/Content").GetComponent<Transform>();
        _extend_Button = transform.Find($"ChatingVariable/ExtendButton").GetComponent<UnityEngine.UI.Button>();
        _scrollView_RectTransform = transform.Find($"ChatingVariable/ScrollView").GetComponent<RectTransform>();


        _extend_Button.onClick.AddListener(() =>
        {//채팅창 창크기를 조절하는 버튼, 버튼을 눌러 최소화, 최대화를 할 수 잇다.
            _extend_ButtonEnabled = !_extend_ButtonEnabled;

            if (_extend_ButtonEnabled)
            {
                _scrollView_RectTransform.offsetMin = new Vector2(_scrollView_RectTransform.offsetMin.x, 80);
                // bottom 수정
                _scrollView_RectTransform.offsetMax = new Vector2(_scrollView_RectTransform.offsetMax.x, 450);
                // Top 수정
            }
            else
            {
                _scrollView_RectTransform.offsetMin = new Vector2(_scrollView_RectTransform.offsetMin.x, 80);
                // bottom 수정
                _scrollView_RectTransform.offsetMax = new Vector2(_scrollView_RectTransform.offsetMax.x, 0);
                // Top 수정
            }
        });

    }

    [PunRPC]
    void RpcAddChat(string msg) //들어온 채팅을 chat_text에 += 더해서 올린다.
    {
        _chatText.text += "\n"+msg;
    }
}
