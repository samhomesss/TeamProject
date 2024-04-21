using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtionSound : MonoBehaviour
{
    Button _buttonClicked;
    AudioSource _audioSource;
    AudioClip _audioClip;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioClip = Managers.Resources.Load<AudioClip>("Prefabs/sh/Sound/cartoon_boing_jump_03");

        _buttonClicked = GetComponent<Button>();
        _buttonClicked.onClick.AddListener(() =>
        {
            _audioSource.PlayOneShot(_audioClip, 0.07f);
        });
    }

}
