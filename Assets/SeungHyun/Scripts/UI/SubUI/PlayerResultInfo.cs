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
        _playerNickName.text = nickName; // 닉네임
        _playerColorPercent.GetComponentInChildren<TMP_Text>().text = resultNumber; // 결과 퍼센테이지
        _playerColorPercent.value = amount; // 슬라이더로 보여주는 거
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
    }
}
