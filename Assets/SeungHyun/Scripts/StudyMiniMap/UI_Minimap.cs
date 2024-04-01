using Sh;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sh
{
    public class UI_Minimap : UIMap
    {
        #region 필요없는 부분
        // 어떤 카메라로 볼건가? 
        // 
        //  [SerializeField] private CameraController Camera;
        #endregion

        // UIMinimap에  RawImage 가 자식으로 들어가 있는것 
        private enum RawImages
        {
            MinimapRawImage,
        }
        #region 필요없는 부분
        //private enum Images
        //{
        //    MinimapIndicator,
        //}

        // private Image _indicator;
        // private RectTransform _indicatorRect;
        #endregion

        protected override void Start()
        {
            base.Start();
            
            RawImage.Add(Util.FindChild(gameObject, "MinimapRawImage").GetComponent<RawImage>());

            RawImage[(int)RawImages.MinimapRawImage].texture = Texture;
        }

        
    }
}

