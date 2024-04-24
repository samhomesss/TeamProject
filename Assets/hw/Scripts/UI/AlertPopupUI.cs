using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;
using yb;

public class AlertPopupUI : UI_Scene
{
    enum GameObjects
    {
        ConfirmButton,
        CancleButton,
        Title_Text
    }

    private Button _confirmButton;
    private Button _cancelButton;
    private TMP_Text _title_Text;

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject confirmButton = GetObject((int)(GameObjects.ConfirmButton));
        GameObject cancelButton = GetObject((int)GameObjects.CancleButton);
        GameObject title_Text = GetObject((int)GameObjects.Title_Text);

        _confirmButton = confirmButton.GetComponent<Button>();
        _cancelButton = cancelButton.GetComponent<Button>();
        _title_Text = title_Text.GetComponent<TMP_Text>();

        _confirmButton.onClick.AddListener(() =>
        {
            GetComponent<Canvas>().enabled = false;
        });

        _cancelButton.onClick.AddListener(() =>
        {
            GetComponent<Canvas>().enabled = false;
        });
    }
    public void SetText(string text)
    {
        _title_Text.text = text;
    }

    void Start()
    {
        Init();
    }
}
