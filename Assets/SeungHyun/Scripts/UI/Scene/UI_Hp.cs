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

        // �ǰ� ���� �׼��� ���� �߰� ���ְ� 
        SetPlayer(map.Player);
        //Managers.Input.HpReduce -= HpDamaged;
        //Managers.Input.HpReduce += HpDamaged;
       
        _hptext.text = ($"{_hpslider.value} / {_hpslider.maxValue}").ToString();
    }

    // �ѹ� ��� �غ��� ��
    public override void PlayerEvent(PlayerController player)
    {
       // Todo : �˷��� ��������!!!~~~
       // base.PlayerEvent(player);
       // player.PlayerEvent.Item1 += HpDamaged;
    }

    //Todo �������� Action ����
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



