using UnityEngine;

// разновидность объекта, который дает опыт игроку при клике

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
