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
        Count,
    }
    
    public enum ItemType {
        HpPotion,
        DamageUpPotion,
        MoveSpeedUpPotion,
        AttackSpeedUpPotion,
        Count,
    }

    public enum PlayerState {
        Shot,
        Reload,
        Pickup,
        Die,
        Idle,
        Respawn,
        Win,
    }
    public enum UIEvent
    {
        Click,
        Drag,
        Enter,
        Exit,
    }

    public enum User {
        None,
        Hw,
        Yb,
        Sh,
    }

    // »¡ º¸ ÃÊ ÆÄ ³ë 
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
        Main,
        LoginScene,
        LobbyScene,
        GameReady,
        GamePlay,
        GameResultScene,
        Quit,
        None,
    }
}
