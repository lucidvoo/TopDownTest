using UnityEngine;

// ќбработчик кнопок, лежащих на полу. «апускает методы контроллера области.

public class WorldButtonHandler : MonoBehaviour
{
    [SerializeField] private ClickAction clickAction;
    [SerializeField] private AreaController areaController;

    // действие кнопки выбираетс€ в инспекторе из выпадающего списка
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
                Debug.LogError("Something wrong with world button");
                break;
        }
    }
}
