using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState{
    None,
    Login,
    WaitUntilLoggedIn,
    InLobby,
    InGameReady,
    InGamePlay,
}
public class GameManager
{
    [SerializeField]private GameState _state;
    public GameState state//���� ������ �θ��ų� ������ ������Ƽ
    {
        get => _state;
        set
        {
            if (_state == value) return; //���ӻ��°� �״�θ� ����, �ٲ�� ����
            _state = value;
        }
    }

}
