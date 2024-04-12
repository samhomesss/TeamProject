using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using yb;
//using static UnityEditor.Progress;


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

        // 피가 깎인 액션을 여기 추가 해주고 
        SetPlayer(map.Player);
        //Managers.Input.HpReduce -= HpDamaged;
        //Managers.Input.HpReduce += HpDamaged;
       
        _hptext.text = ($"{_hpslider.value} / {_hpslider.maxValue}").ToString();
    }

    // 한번 사용 해보려 함
    public override void PlayerEvent(PlayerController player)
    {
       // Todo : 알려줘 윤범이형!!!~~~
       // base.PlayerEvent(player);
       // player.PlayerEvent.Item1 += HpDamaged;
    }

    //Todo 윤범이형 Action 연결
    void SetPlayer(PlayerController player)
    {
        //player.HpEvent = null;
        player.HpEvent += HpDamaged;
    }

    public void HpDamaged(int Hp , int maxHP)
    {
        //_hpslider.value -= damage;
        _hptext.text = ($"{Hp} / {maxHP}").ToString();
    }

        

}



