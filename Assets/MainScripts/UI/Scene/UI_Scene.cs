using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yb;

public class UI_Scene : UI_Base
{
    protected PlayerController _player;
    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject, false);
    }

   public virtual void PlayerEvent(PlayerController player)
   {
      _player = player;
   }
}
