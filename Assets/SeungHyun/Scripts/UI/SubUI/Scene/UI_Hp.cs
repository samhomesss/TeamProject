using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using yb;


public class UI_Hp : UI_Scene
{
    enum GameObjects
    {
        HP_Slider,
        HP_Text,
    }

    Map map;
    Slider _hpslider;
    Text _hptext;

    public float HpSlider {
        get => _hpslider.value;
        set => _hpslider.value = value; 
    }


    private void Start()
    {
        map = Map.MapObject.GetComponent<Map>();
        Init();
        _hpslider.maxValue = 100;
        _hpslider.value = 100;
        _hptext.text = "30 / 30";
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        GameObject hp_slider = Get<GameObject>((int)GameObjects.HP_Slider);
        GameObject hp_text = GetObject((int)GameObjects.HP_Text);

        _hpslider = hp_slider.GetComponent<Slider>();
        _hptext = hp_text.GetComponent<Text>();
        if(IsTestMode.Instance.CurrentUser == Define.User.Hw)
        {
            SetPlayer(GameObject.Find($"Player{PhotonNetwork.LocalPlayer.ActorNumber}").GetComponentInChildren<PlayerController>());
        }
        else
        {
            SetPlayer(map.Player);
        }
        _hptext.text = ($"{_hpslider.value} / {_hpslider.maxValue}").ToString();
    }

    public override void PlayerEvent(PlayerController player)
    {
       player = map.Player;
    }

    void SetPlayer(PlayerController player)
    {
        player.HpEvent += HpDamaged;
    }

    public void HpDamaged(int Hp , int maxHP)
    {
        _hpslider.value = Hp;
        _hptext.text = ($"{Hp} / {maxHP}").ToString();
    }

        

}



