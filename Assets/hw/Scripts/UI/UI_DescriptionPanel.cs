using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DescriptionPanel : UI_Scene
{
    private Canvas _uI_DescriptionPanel_Canvas;
    private Button _close_button;

    AudioSource _audioSource;
    AudioClip _audioClip;

    private void Start()
    {
        _audioSource = Util.FindChild(transform.gameObject, "Exit Button").GetComponent<AudioSource>();
        _audioClip = Managers.Resources.Load<AudioClip>("Prefabs/sh/Sound/cartoon_boing_jump_03");

        _close_button = GetComponentInChildren<Button>();
        _uI_DescriptionPanel_Canvas = GetComponent<Canvas>();

        _close_button.onClick.AddListener(() =>
        {
            _audioSource.PlayOneShot(_audioClip, 0.5f);
            _uI_DescriptionPanel_Canvas.enabled = false;
        });

    }
}
