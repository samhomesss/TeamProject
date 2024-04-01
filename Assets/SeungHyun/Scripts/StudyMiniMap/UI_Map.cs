using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sh
{
    public class UIMap : UI_Scene
    {
        // RawImage (�̴ϸ� , ������ �Ȱ��� �־ ����Ʈ�� �޴��ǵ� ���ص� �ɵ�?)
        [HideInInspector] public List<RawImage> RawImage = new List<RawImage>();

        // RawImage�� �ǵ��� �ʰ� �� ���� Texture�� �÷��� 
        // ������ �ٲٴ°�
        [HideInInspector] public Texture2D Texture;

        Node node;
        protected virtual void Start()
        {
            node = Managers.Node;
            // node.Nodes.GetLength(0) new Texture2D[0��° �� , 1��° ��]
            Texture = new Texture2D(node.Nodes.GetLength(0), node.Nodes.GetLength(1));
            // ũ�� 100 100 ¥�� ���ο� 2D Texture����� �ذŰ�
        }


        // ���� �ٲٴ°�
        public void UpdateMap(Color[] color)
        {
            Color[,] color2 = new Color[100, 100];
            // 100, 100 ����� ���� ������ ���°Ű�
            color2[10, 10] = Color.red;
            // ���÷� 10,10�� ���� ���������� �ٲ۰�
            // SetPixels�� ���߹迭�� ���� ���� �ʱ⿡ 
            // ���߹迭�� �׳� �迭�� �ٲ������
            Texture.SetPixels(color); // �� �÷��� �ؽ��� �ٲ��ذŰ� 
            Texture.Apply(false);
        }

        #region ���ð���
        public override void Init()
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }

}
