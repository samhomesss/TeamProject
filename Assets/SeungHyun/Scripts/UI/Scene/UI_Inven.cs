using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    public class UI_Inven : UI_Scene
    {

        List<UI_Inven_Item> ui_Inven_Items = new List<UI_Inven_Item>();
        UI_Inven_Item invenItem;
        enum GameObjects
        {
            GridPanel
        }

        void Start()
        {
            Init();
            // 구독해주고
            ItemCreate_Button.OnImageChanged += ChangeImage;
        }


        public override void Init()
        {
            base.Init();

            Bind<GameObject>(typeof(GameObjects));

            GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
            foreach (Transform child in gridPanel.transform)
                Managers.Resources.Destroy(child.gameObject);

            // 실제 인벤토리 정보를 참고해서
            for (int i = 0; i < 4; i++)
            {
                GameObject item = Managers.UI.MakeSubItem<UI_Inven_Item>(gridPanel.transform).gameObject;
                invenItem = item.GetOrAddComponent<UI_Inven_Item>();
                // 리스트에 추가
                ui_Inven_Items.Add(invenItem);
                invenItem.SetInfo($"아이템{i}번");
            }
        }

        public void ChangeImage(Sprite itemimage)
        {
            for (int i = 0; i < ui_Inven_Items.Count; i++)
            {
                if (!ui_Inven_Items[i].IsEmpty)
                {
                    continue;
                }

                ui_Inven_Items[i].SetIsEmpty(false);
                ui_Inven_Items[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite = itemimage;
                break;
                 
            }
        }
    }


