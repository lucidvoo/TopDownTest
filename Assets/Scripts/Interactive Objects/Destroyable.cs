using UnityEngine;

// разновидность объекта, который разрушается за несколько кликов

public class Destroyable : InteractiveObjectBase
{
    [SerializeField] private int hitsToDestroy = 3;

    private int timesHit = 0;


    public override void Interact(InteractiveObjectBase objClicked)
    {
        base.Interact(this);

        timesHit++;

        if (timesHit >= hitsToDestroy)
        {
            Events.onInteractiveObjectDestroyed.Invoke(this);
            Destroy(gameObject);
        }
    }


    public override ObjSaveData GetSaveData()
    {
        return new ObjSaveData(transform.position, GetType());
    }
}
