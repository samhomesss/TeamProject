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
        //onSubmit�� inputField�� ������Ƽ ���͸� ������ ȣ��ǵ��ϼ���
        _inputchat.onSubmit.AddListener(Onsubmit);

    }

    [PunRPC]
    void RpcAddChat(string input)
    {
        //1.�۾��ٰ� ���͸� ġ��
        //2. chatitem�� �ϳ� �����.
        //�θ� ScroolView -Content)
        //3. text ������Ʈ�� ������ inputfield�� ������ ������
        GameObject _item = Instantiate(_chatItemFactory, _trContent);

        TMP_Text _text = _item.GetComponent<TMP_Text>();
        _text.text = $"{LoginInformation.profile.nickname} :{input}";


        Debug.Log(_text.text);
    }

}
