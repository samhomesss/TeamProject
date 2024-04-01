using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sh
{
    public class UIMap : UI_Base
    {
        [HideInInspector] public List<RawImage> RawImage = new List<RawImage>();
        [HideInInspector] public Texture2D Texture;

        protected virtual void Start()
        {
            var node = Managers.Node;
            Texture = new Texture2D(node.Nodes.GetLength(0), node.Nodes.GetLength(1));
        }


        // 색을 바꾸는거
        public void UpdateMap(Color[] color)
        {
            Texture.SetPixels(color);
            Texture.Apply(false);
        }

        public override void Init()
        {
            throw new System.NotImplementedException();
        }
    }

}
