using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int ItemID => itemID; 
    int itemID;
   
    private void Start()
    {
        GameObject go = this.gameObject;
        switch (go.name)
        {
            case "Pistol":
                itemID = 50;
                break;

            case "Rifle":
                itemID = 51;
                break;

            case "ShotGun":
                itemID = 52;
                break;
            default:
                break;
        }

        //Debug.Log(itemID + ":" + go.name);
    }
}
