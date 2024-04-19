using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using yb;

public class AlertPopupUI : UI_Scene
{
    enum GameObjects
    {
        ConfirmButton,
        CancleButton,
    }

    private Button _confirmButton;
    private Button _cancelButton;

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject ConfirmButton = GetObject((int)(GameObjects.ConfirmButton));
        GameObject CancelButton = GetObject((int)GameObjects.CancleButton);

        _confirmButton = ConfirmButton.GetComponent<Button>();
        _cancelButton = CancelButton.GetComponent<Button>();


        _confirmButton.onClick.AddListener(() =>
        {
            GetComponent<Canvas>().enabled = false;
        });

        _cancelButton.onClick.AddListener(() =>
        {
            GetComponent<Canvas>().enabled = false;
        });
    }
    void Start()
    {
        Init();
    }
}
