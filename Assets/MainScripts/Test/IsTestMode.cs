using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTestMode : MonoBehaviour
{
    public static IsTestMode Instance;

    public Define.User CurrentUser;
    private void Awake() {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        else
        Instance = this;

        DontDestroyOnLoad(this);
    }

    private void Start() {
        if (CurrentUser == Define.User.None)
            CurrentUser = Define.User.Hw;
    }
}
