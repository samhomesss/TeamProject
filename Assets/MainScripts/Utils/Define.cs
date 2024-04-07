using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define 
{
    public enum GameState
    {
        None,
        Login,
        WaitUntilLoggedIn,
        InLobby,
        InGameReady,
        InGamePlay,
    }

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
    public enum UIEvent
    {
        Click,
        Drag,
    }

    public enum User {
        None,
        Hw,
        Yb,
        Sh,
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
        Press,
        Click,
    }

    public enum SceneType
    {
        None,
        Login,
        Lobby,
        InGame,
    }
}
