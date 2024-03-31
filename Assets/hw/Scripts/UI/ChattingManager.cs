using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChattingManager : MonoBehaviourPun
{
    [SerializeField]private GameObject _chatItemFactory;
    [SerializeField] private TMP_InputField _inputchat;
    [SerializeField] private Transform _trContent;


    public void Onsubmit(string s)
    {
        photonView.RPC("RpcAddChat", RpcTarget.All, s);

        _inputchat.text = "";

        _inputchat.ActivateInputField();
    }


    private void Start()
    {
        //onSubmit은 inputField의 프로퍼티 엔터를 누르면 호출되도록설정
        _inputchat.onSubmit.AddListener(Onsubmit);

    }

    [PunRPC]
    void RpcAddChat(string input)
    {
        //1.글쓰다가 엔터를 치면
        //2. chatitem을 하나 만든다.
        //부모를 ScroolView -Content)
        //3. text 컴포넌트를 가져와 inputfield의 내용을 적어줌
        GameObject _item = Instantiate(_chatItemFactory, _trContent);

        TMP_Text _text = _item.GetComponent<TMP_Text>();
        _text.text = $"{LoginInformation.profile.nickname} :{input}";


        Debug.Log(_text.text);
    }

}
