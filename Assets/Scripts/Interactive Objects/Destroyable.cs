using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
