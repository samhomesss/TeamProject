using Photon.Pun;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace yb {
    /// <summary>
    /// ������ ��� Ŭ����
    /// </summary>
    public class ItemDroplable : IItemDroplable {
        private List<string> _itemsList = new List<string>();  //����� ������ ����� string���� ����
        public void Set(string item) {
             _itemsList.Add(item);
        }

        /// <summary>
        /// �������� ��ġ�� �ʰ� ���������� ���
        /// </summary>
        /// <param name="pos"></param>
        public void Drop(Vector3 pos) {
            if (_itemsList.Count <= 0)
                return;

            int x = 0;
            int z = 0;
            int dx = 0;
            int dy = -1;
            int t = 0;
            int count = _itemsList.Count;
            for (int i = 0; i < count; i++) {
                if ((-count / 2 < x) && (x <= count / 2) && (-count / 2 < z) && (z <= count / 2)) {
                    string path = $"yb/Weapon/{_itemsList[i]}"; //0411 00:13�� �����  yb/item/{_itemsList[i]} -> yb/Weapon/{_itemsList[i]} ���� ����
                    GameObject go;
                    if (IsTestMode.Instance.CurrentUser == Define.User.Hw) //0411 12:42 ����� ���� �׽�Ʈ�� ���ǹ�����
                    {
                        if (!PhotonNetwork.IsMasterClient)
                            return;

                        go = PhotonNetwork.Instantiate($"Prefabs/{path}", new Vector3(pos.x + x, 1f, pos.z + z), Quaternion.identity);
                    }
                    else
                    {
                        go = Managers.Resources.Instantiate(path, null);
                        go.transform.position = new Vector3(pos.x + x, 1f, pos.z + z);
                    }
                }

                if ((x == z) || ((x < 0) && (x == -z)) || ((x > 0) && (x == 1 - z))) {
                    t = dx;
                    dx = -dy;
                    dy = t;
                }
                x += dx;
                z += dy;
            }
        }
    }
}