using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmPopupUI : UI_Scene
{
    enum GameObjects
    {
        ConfirmButton,
        CancleButton,
        TitleText
    }

    private Button _confirmButton;
    private Button _cancelButton;
    private TMP_Text _title_Text;
    private Canvas _registerId;

    void Start()
    {
        Bind<GameObject>(typeof(GameObjects));



        GameObject TitleText = GetObject((int)GameObjects.TitleText);
        GameObject ConfirmButton = GetObject((int)(GameObjects.ConfirmButton));
        GameObject CancelButton = GetObject((int)GameObjects.CancleButton);

        _title_Text = TitleText.GetComponent<TMP_Text>();
        _confirmButton = ConfirmButton.GetComponent<Button>();
        _cancelButton = CancelButton.GetComponent<Button>();

        _registerId = Util.FindChild(transform.parent.gameObject, "ConfirmPopupUI").GetComponent<Canvas>();

        _confirmButton.onClick.AddListener(() =>
        {
            this.enabled = false;
            _registerId.enabled = false;
        });

        _cancelButton.onClick.AddListener(() =>
        {
            this.enabled = false;
            _registerId.enabled = false;
        });


    }

    public string SetTitletext(string title)
    {
        string title_text = title;
        return title_text;
    }
}
