using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yb;

public class Data
{
    public const int MaxRelicCount = 2;

    private float _defaultPlayerMoveSpeed = 5f;

    private int _defaultPlayerMaxHp = 100;

    private float _moveSpeedDecrease = 0.5f;

    private float _defaultResurrectionTime = 8f;

    private float _bonusResurrectionTime = 3f;

    private int _defaultShotgunProjectile = 6;

    private int[] _bonusProjectile = new int[] {1, 1, 3 };

    private float[] _bonusProjectileChance = new float[] {0.5f, 0.2f,0.3f  };

    private float[] _bonusAttackDelay = new float[] { 0.5f, 0.2f, 0.7f };

    private int[] _defaultWeaponDamage = new int[] {15, 10, 20};

    private float[] _defaultWeaponDelay = new float[] { 1f, .5f, 2f };

    private float[] _defaultWeaponRealodTime = new float[] { 1f, 1f, 1.5f };

    private float[] _defaultProjectileVelocity = new float[] {10f, 10f, 10f };

    private int[] _defaultWeaponRemainBullet = new int[] {15, 30, 8 };

    private int[] _defaultWeaponMaxBullet = new int[] {60, 120, 30 };
    private float[] _defaultProjectileRnage = new float[] { 20f, 16f, 12f };

    private Vector3[] _defaultWinScenePosition = new Vector3[] { new Vector3(2.74f, 1.14f, -4.04f), new Vector3(1.52f, 0.68f, -3.09f), new Vector3(3.34f, 0.41f, -5.33f) };
    private Vector3 _defaultWinSceneRotation = new Vector3(0f, -142.3f, 0f);

    private int _defaultShieldMaxHp = 10;

    private float _defaultShieldRecuveryTime = 3f;

    private float _defaultGuardSpeed = 3f;

    private float _defaultGuardRadius = 3f;

    public float DefaultPlayerMoveSpeed => _defaultPlayerMoveSpeed;
    public float DefaultShieldRecuveryTime => _defaultShieldRecuveryTime;
    public float DefaultGuardRadius => _defaultGuardRadius;
    public float DefaultGuardSpeed => _defaultGuardSpeed;



    public int DefaultPlayerMaxHp => _defaultPlayerMaxHp;
    public int DefaultShieldMaxHp => _defaultShieldMaxHp;

    public float DefaultResurrectionTime => _defaultResurrectionTime;
    public float BonusResurrectionTime => _bonusResurrectionTime;
    public int DefaultShotgunProjectile => _defaultShotgunProjectile;

    public int DefaultWeaponDamage(int type) => _defaultWeaponDamage[type];

    public int BonusProjectile(int type) => _bonusProjectile[type]; 
    public float BonusAttackDelay(int type) => _bonusAttackDelay[type]; 
    public float BonusProjectileChance(int type) => _bonusProjectileChance[type]; 
    public float DefaultWeaponDelay(int type) => _defaultWeaponDelay[type]; 
    public float DefaultWeaponRealodTime(int type) => _defaultWeaponRealodTime[type]; 
    public float DefaultWeaponVelocity(int type) => _defaultProjectileVelocity[type]; 
    public int DefaultWeaponRemainBullet(int type) => _defaultWeaponRemainBullet[type]; 
    public int DefaultWeaponMaxBullet(int type) => _defaultWeaponMaxBullet[type];

    public float MoveSpeedDecrease() => _moveSpeedDecrease;
    public float DefaultProjectileRnage(int type) => _defaultProjectileRnage[type];

    public Vector3 DefaultWinScenePosition(int rank) => _defaultWinScenePosition[rank];
    public Vector3 DefaultWinSceneRotation() => _defaultWinSceneRotation;
}
