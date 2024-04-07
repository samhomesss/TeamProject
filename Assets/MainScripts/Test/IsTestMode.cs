using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTestMode : MonoBehaviour
{
    public static IsTestMode Instance;

    public Define.User CurrentUser;
    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        if (CurrentUser == Define.User.None)
            Debug.Log("현재 유저를 정하세요");
    }
}
