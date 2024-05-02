using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yb;

public class AchievementManager : MonoBehaviour
{
    PlayerController controller;
    Action _dieEvent;

    private int _deathCount = 0;
    
    // Update is called once per frame

    private void Start()
    {
        controller = GameObject.Find($"Player{PhotonNetwork.LocalPlayer.ActorNumber}").GetComponentInChildren<PlayerController>();
    }

    public void countDead()
    {
        _deathCount++;
        Debug.Log($"{_deathCount}만큼 죽었습니다");
    }

}
