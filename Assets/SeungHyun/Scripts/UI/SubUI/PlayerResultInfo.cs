using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResultInfo : UI_Base
{
    enum GameObjects
    {
        PlayerResultNumberImage, // 게임 결과 순위 이미지
        PlayerCharacterImage, // 플레이어의 이미지 안바뀔수도?
        PlayerNickName, // 해당 플레이어의 닉네임
        PlayerResultNumber, // 1등 2등 3등을 제외한 등수들의 텍스트
        PlayerColorPercent, // 해당 플레이어가 얼마나 많은 색을 칠했는지.
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
