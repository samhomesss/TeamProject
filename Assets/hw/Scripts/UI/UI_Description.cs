using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Description : UI_Scene
{
    private Button _buttonClicked;
    private Canvas DescriptionCanvas;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Canvas>().enabled = true;
        DescriptionCanvas = Util.FindChild(transform.parent.gameObject, "UI_DescriptionPanel").GetComponent<Canvas>();
        _buttonClicked = GetComponentInChildren<Button>();
        _buttonClicked.onClick.AddListener(() =>
        {
            DescriptionCanvas.enabled = true;
        });
    }
}
