using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yb;

public class PlayerAudioController : AudioController
{
    public void SetSfx(Define.PlayerAudioType type) {
        if (_sources[(int)type].isPlaying) {
            _sources[(int)type].Stop();
        }
        _sources[(int)type].Play();
    }

    protected override void Start() {
        base.Start();
    }
}
