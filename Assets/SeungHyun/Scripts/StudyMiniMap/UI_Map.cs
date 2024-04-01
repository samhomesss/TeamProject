using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sh
{
    public class UIMap : UI_Scene
    {
        // RawImage (미니맵 , 전장의 안개가 있어서 리스트로 햇던건데 안해도 될듯?)
        [HideInInspector] public List<RawImage> RawImage = new List<RawImage>();

        // RawImage는 건들지 않고 그 위에 Texture를 올려서 
        // 색상을 바꾸는거
        [HideInInspector] public Texture2D Texture;

        Node node;
        protected virtual void Start()
        {
            node = Managers.Node;
            // node.Nodes.GetLength(0) new Texture2D[0번째 값 , 1번째 값]
            Texture = new Texture2D(node.Nodes.GetLength(0), node.Nodes.GetLength(1));
            // 크기 100 100 짜리 새로운 2D Texture만들어 준거고
        }


        // 색을 바꾸는거
        public void UpdateMap(Color[] color)
        {
            Color[,] color2 = new Color[100, 100];
            // 100, 100 노드의 색을 가지고 오는거고
            color2[10, 10] = Color.red;
            // 예시로 10,10의 색을 빨간색으로 바꾼거
            // SetPixels은 이중배열을 지원 하지 않기에 
            // 이중배열을 그냥 배열로 바꿔줘야함
            Texture.SetPixels(color); // 그 컬러로 텍스쳐 바꿔준거고 
            Texture.Apply(false);
        }

        #region 무시가능
        public override void Init()
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }

}
