using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define 
{
    public enum weaponType {
        Pistol,
        Rifle,
        Shotgun,
    }

    public enum RelicType {
        Relic1, 
        Relic2,
        Relic3,
        Relic4,
        Relic5,
    }

    public enum MouseEventType
    {
        None,
        LeftMouseDown,
        RightMouseDown,
        LeftMouseUp,
        RightMouseUp,
        LeftMouse,
        RightMouse,
        Enter,
        Drag,
    }

    public enum SceneType
    {
        None,
        InGame,
    }
}
