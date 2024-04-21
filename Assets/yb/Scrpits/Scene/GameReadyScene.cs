using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReadyScene : BaseScene
{
    public override void Clear() {
    }

    private void Start() {
        _fade.SetFade(false);
    }
}
