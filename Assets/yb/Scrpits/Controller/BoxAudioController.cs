using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yb;

public class BoxAudioController : AudioController
{
    public void SetSfx(Define.BoxAudioType type) {
        Init();
        _sources[(int)type].Play();
        GameObject.Destroy(gameObject, 1f);
    }
}
