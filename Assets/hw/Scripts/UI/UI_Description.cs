using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 설명서 버튼 
/// </summary>
public class UI_Description : UI_Scene
{
    private Button _buttonClicked;
    private Canvas DescriptionCanvas;
    AudioSource _audioSource;
    AudioClip _audioClip;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = Util.FindChild(transform.gameObject, "Description Button").GetComponent<AudioSource>();
        _audioClip = Managers.Resources.Load<AudioClip>("Prefabs/sh/Sound/cartoon_boing_jump_03");

        gameObject.GetComponent<Canvas>().enabled = true;
        DescriptionCanvas = Util.FindChild(transform.parent.gameObject, "UI_DescriptionPanel").GetComponent<Canvas>();
        _buttonClicked = GetComponentInChildren<Button>();
        _buttonClicked.onClick.AddListener(() =>
        {
            _audioSource.PlayOneShot(_audioClip , 0.5f);
            DescriptionCanvas.enabled = true;
        });
    }
}
