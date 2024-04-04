using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define 
{
    public enum WeaponType {
        Pistol,
        Rifle,
        Shotgun,
    }

    public enum PlayerState {
        Shot,
        Reload,
        Pickup,
        Die,
        Idle
    }

    public enum RelicType {
        BonusAttackSpeedRelic,
        BonusProjectileRelic,
        BonusResurrectionTimeRelic,
        GuardRelic,
        ShieldRelic,
        Count,
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
