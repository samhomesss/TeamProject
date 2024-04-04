using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yb;

public class Data
{
    public const int MaxRelicCount = 2;

    private float _defaultPlayerMoveSpeed = 5f;

    private int _defaultPlayerMaxHp = 30;

    private int[] _defaultWeaponDamage = new int[] {2, 1, 3};

    private float[] _defaultWeaponDelay = new float[] { 1f, .5f, 2f };

    private float[] _defaultWeaponRealodTime = new float[] { 1f, 1f, 1.5f };

    private float[] _defaultProjectileVelocity = new float[] {10f, 10f, 10f };

    private int[] _defaultWeaponRemainBullet = new int[] {15, 30, 8 };

    private int[] _defaultWeaponMaxBullet = new int[] {60, 120, 30 };

    public float DefaultPlayerMoveSpeed => _defaultPlayerMoveSpeed;

    public int DefaultPlayerMaxHp => _defaultPlayerMaxHp;

    public int DefaultWeaponDamage(int type) => _defaultWeaponDamage[type]; 
    public float DefaultWeaponDelay(int type) => _defaultWeaponDelay[type]; 
    public float DefaultWeaponRealodTime(int type) => _defaultWeaponRealodTime[type]; 
    public float DefaultWeaponVelocity(int type) => _defaultProjectileVelocity[type]; 
    public int DefaultWeaponRemainBullet(int type) => _defaultWeaponRemainBullet[type]; 
    public int DefaultWeaponMaxBullet(int type) => _defaultWeaponMaxBullet[type]; 
}
