using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Разновидность объекта, который уменьшает здоровье игрока при клике

public class Hurting : InteractiveObjectBase
{
    [SerializeField] private int healthToLose = 25;


    public override void Interact(InteractiveObjectBase objClicked)
    {
        base.Interact(this);

        if (playerNearby != null)
        {
            playerNearby.Stats.Damage(healthToLose);
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
