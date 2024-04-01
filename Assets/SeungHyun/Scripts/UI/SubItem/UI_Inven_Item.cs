using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sh
{
    public class UI_Inven_Item : UI_Base
    {
        enum GameObjects
        {
            ItemIcon,
            ItemNameText,
        }

        public bool IsEmpty => _isEmpty;
        public GameObject Icon => _icon;
       

        string _name;
        bool _isEmpty = true;
        GameObject _icon;

        void Start()
        {
            Init();
        }

        public override void Init()
        {
            Bind<GameObject>(typeof(GameObjects));
            Get<GameObject>((int)GameObjects.ItemNameText).GetComponent<Text>().text = _name;
            _icon = Get<GameObject>((int)GameObjects.ItemIcon);
            _icon.BindEvent(UseItem);
        }

        public void UseItem(PointerEventData PointerEventData)
        {
            Debug.Log($"아이템 클릭! {_name}");
            Debug.Log(_icon.name);
            _isEmpty = true;
            _icon.GetComponent<Image>().sprite = Managers.ItemDataBase.GetItemData(0).itemImage;
        }

        public void SetInfo(string name)
        {
            _name = name;
        }

        public void SetIsEmpty(bool isEmpty)
        {
           _isEmpty = isEmpty;
        }
        
    }
}

