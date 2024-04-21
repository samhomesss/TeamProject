using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yb;

public class PlayerAudioController : AudioController
{
    public void SetSfx(Define.PlayerAudioType type) {
        _sources[(int)type].Play();
    }

    protected override void Start() {
        base.Start();
    }
}
