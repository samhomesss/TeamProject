using Sh;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sh
{
    public class UIMinimap : UIMap
    {
        #region 필요없는 부분
        // 어떤 카메라로 볼건가? 
        // 
        //  [SerializeField] private CameraController Camera;
        #endregion
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

