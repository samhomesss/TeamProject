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

    private void Start()
    {
        map = Map.MapObject.GetComponent<Map>();
        Init();
        _hpslider.maxValue = 30;
        _hpslider.value = 30;
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

<<<<<<< Updated upstream
        SetPlayer(map.Player[0]);
=======
        SetPlayer(map.Player[PhotonNetwork.LocalPlayer.ActorNumber-1]);
>>>>>>> Stashed changes
        _hptext.text = ($"{_hpslider.value} / {_hpslider.maxValue}").ToString();
    }

    public override void PlayerEvent(PlayerController player)
    {
<<<<<<< Updated upstream
       player = map.Player[0];
=======
       player = map.Player[PhotonNetwork.LocalPlayer.ActorNumber - 1];
>>>>>>> Stashed changes
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



