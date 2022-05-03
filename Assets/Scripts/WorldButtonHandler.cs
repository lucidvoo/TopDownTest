using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldButtonHandler : MonoBehaviour
{
    [SerializeField] private ClickAction clickAction;
    [SerializeField] private AreaController areaController;

    private enum ClickAction { Spawn, Clear, Save, Load }

    private void OnMouseDown()
    {
        switch (clickAction)
        {
            case ClickAction.Spawn: areaController.SpawnRandomly();
                break;
            case ClickAction.Clear: areaController.DestroyObjects();
                break;
            case ClickAction.Save: areaController.SaveObjects();
                break;
            case ClickAction.Load: areaController.LoadObjects();
                break;
            default:
                Debug.LogError("Something wrong");
                break;
        }
    }

}
