using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sh
{
    // 필요 없는 스크립트 
    public class HP_Text : UI_Base
    {
        enum GameObjects
        {
           HP_Text
        }

        string _name;

        void Start()
        {
            Init();
        }

        public override void Init()
        {
            Bind<GameObject>(typeof(GameObjects));
            Get<GameObject>((int)GameObjects.HP_Text).GetComponent<Text>().text = _name;
        }

        public void SetHp(float maxHP, float HP)
        {
            _name = $"{(int)HP} / {(int)maxHP}";
            Debug.Log(_name);
        }
    }
}


