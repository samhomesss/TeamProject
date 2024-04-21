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

    Image _playerResultNumberImage;
    Image _playerCharacterImage;
    TMP_Text _playerNickName;
    TMP_Text _playerResultNumber;
    Slider _playerColorPercent;

    private void Start()
    {
        
    }

    public void Initisarize( string nickName, string resultNumber, float amount)
    {
        Init();
        _playerResultNumberImage = Get<GameObject>((int)GameObjects.PlayerResultNumberImage).GetComponent<Image>();
        _playerCharacterImage = Get<GameObject>((int)GameObjects.PlayerCharacterImage).GetComponent<Image>();
        _playerNickName = Get<GameObject>((int)GameObjects.PlayerNickName).GetComponent<TMP_Text>();
        _playerResultNumber = Get<GameObject>((int)GameObjects.PlayerResultNumber).GetComponent<TMP_Text>();
        _playerColorPercent = Get<GameObject>((int)GameObjects.PlayerColorPercent).GetComponent<Slider>();
        // _playerResultNumberImage.sprite = resultImage;
        // _playerCharacterImage.sprite = charImage;
        _playerNickName.text = nickName; // �г���
        _playerColorPercent.GetComponentInChildren<TMP_Text>().text = resultNumber; // ��� �ۼ�������
        _playerColorPercent.value = amount; // �����̴��� �����ִ� ��
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
    }
}
