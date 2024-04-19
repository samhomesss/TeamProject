using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DescriptionPanel : UI_Scene
{
    private Canvas _uI_DescriptionPanel_Canvas;
    private Button _close_button;

    private void Start()
    {
        _close_button = GetComponentInChildren<Button>();
        _uI_DescriptionPanel_Canvas = GetComponent<Canvas>();

        _close_button.onClick.AddListener(() =>
        {
            _uI_DescriptionPanel_Canvas.enabled = false;
        });

    }
}
