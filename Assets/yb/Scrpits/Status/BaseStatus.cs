using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStatus : MonoBehaviour
{
    protected Data _data;
    protected int _currentHp;
    protected int _maxHp;

    public int MaxHp => _maxHp;
    public int CurrentHp => _currentHp;
    private void Start() {
        _data = Managers.Data;
        Init();
    }

    protected virtual void Init() {
        _maxHp = _data.DefaultPlayerMaxHp;
        _currentHp = _maxHp;
    }

    public int SetHp(int amout) {
        _currentHp += amout;
        return _currentHp;
    }
   
   
}
