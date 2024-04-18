using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResultInfo : UI_Base
{
    enum GameObjects
    {
        PlayerResultNumberImage, // ���� ��� ���� �̹���
        PlayerCharacterImage, // �÷��̾��� �̹��� �ȹٲ����?
        PlayerNickName, // �ش� �÷��̾��� �г���
        PlayerResultNumber, // 1�� 2�� 3���� ������ ������� �ؽ�Ʈ
        PlayerColorPercent, // �ش� �÷��̾ �󸶳� ���� ���� ĥ�ߴ���.
    }

    public Image PlayerResultImage { get => _playerResultNumberImage; set => _playerResultNumberImage = value; }
    public Image PlayerCharacterImage { get => _playerCharacterImage; set => _playerCharacterImage = value; }
    public TMP_Text PlayerNickName { get => _playerNickName; set =>  _playerNickName = value; }
    public TMP_Text PlayerResultNumber { get => _playerResultNumber; set => _playerResultNumber = value; }
    public Slider PlayerColorPercent { get => _playerColorPercent; set => _playerColorPercent = value; }

    Image _playerResultNumberImage;
    Image _playerCharacterImage;
    TMP_Text _playerNickName;
    TMP_Text _playerResultNumber;
    Slider _playerColorPercent;

    private void Start()
    {
        Init();
        _playerResultNumberImage = Get<GameObject>((int)GameObjects.PlayerResultNumberImage).GetComponent<Image>();
        _playerCharacterImage = Get<GameObject>((int)GameObjects.PlayerCharacterImage).GetComponent<Image>();
        _playerNickName = Get<GameObject>((int)GameObjects.PlayerNickName).GetComponent<TMP_Text>();
        _playerResultNumber = Get<GameObject>((int)GameObjects.PlayerResultNumber).GetComponent<TMP_Text>();
        _playerColorPercent = Get<GameObject>((int)GameObjects.PlayerColorPercent).GetComponent<Slider>();

    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));

        //_playerResultNumberImage = Get<GameObject>((int)GameObjects.PlayerResultNumberImage).GetComponent<Image>();
        //_playerCharacterImage = Get<GameObject>((int)GameObjects.PlayerCharacterImage).GetComponent<Image>();
        //_playerNickName = Get<GameObject>((int)GameObjects.PlayerNickName).GetComponent<TMP_Text>();
        //_playerResultNumber = Get<GameObject>((int)GameObjects.PlayerResultNumber).GetComponent<TMP_Text>();
        //_playerColorPercent = Get<GameObject>((int)GameObjects.PlayerColorPercent).GetComponent<Slider>();

        //Debug.Log(_playerNickName.gameObject.name);
    }
}
