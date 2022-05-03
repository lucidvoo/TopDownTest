using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivingExperience : InteractiveObjectBase
{
    [SerializeField] private int expToGive = 20;

    public override void Interact(InteractiveObjectBase objClicked)
    {
        base.Interact(this);

        if (playerNearby != null)
        {
            playerNearby.Stats.GiveExp(expToGive);
        }
        else
        {
            Debug.LogError("Smth wrong with player detection");
        }
    }

    public override ObjSaveData GetSaveData()
    {
        return new ObjSaveData(transform.position, GetType());
    }
}
