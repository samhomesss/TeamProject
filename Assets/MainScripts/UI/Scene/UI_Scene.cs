using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scene : UI_Base
{
    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject, false);
    }

   // public void SetPlayer(PlayerController player)
   // {
   //     //    Tuple<Action<int,int> hpevent, Action<) qw = player.이벤트싹다리턴
   //     //        qw.item1 += 
   // }
}
