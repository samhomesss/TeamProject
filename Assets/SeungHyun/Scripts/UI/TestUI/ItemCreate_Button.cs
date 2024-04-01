using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sh
{
    public class ItemCreate_Button : UI_Scene
    {
        public static event Action<Sprite> OnImageChanged;
        enum Buttons
        {
            ItemButton,
        }

        [SerializeField] int _itemID;
        Sprite _itemImage;

        private void Start()
        {
            _itemImage = Managers.ItemDataBase.GetItemData(_itemID).itemImage;
            Bind<Button>(typeof(Buttons));
            GetButton((int)Buttons.ItemButton).gameObject.BindEvent(OnButtonClicked);
        }

        public void OnButtonClicked(PointerEventData data)
        {
            // �������� �̺�Ʈ �߻� ��Ű�� �Ѱ� �ָ� �ɵ�?
            Debug.Log(Managers.ItemDataBase.GetItemData(_itemID).name);
            if (OnImageChanged != null)
                OnImageChanged.Invoke(_itemImage);
        }    
    }
}


