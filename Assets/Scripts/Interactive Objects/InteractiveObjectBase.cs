using UnityEngine;

// Базовый класс для объекта. При клике только выводи информацию на UI.
// содержит большую часть функционала интерактивных объектов.

public class InteractiveObjectBase : MonoBehaviour
{
    // индикатор, который мы будем показывать и прятать при входе игрока в зону интерактивности
    [SerializeField] private GameObject interactionIndicator;
    // описание для UI
    public string description;
    
    [HideInInspector] public bool isInteractable;
    // храним ссылку на игрока, который вошел в нашу область интерактивности
    [HideInInspector] public PlayerController playerNearby;

    public bool IsInteractable { get => isInteractable; set => isInteractable = value; }


    private void Start()
    {
        if (string.IsNullOrEmpty(description))
        {
            description = "<нет описания для объекта " + gameObject.name + ">";
        }
    }


    private void OnMouseDown()
    {
        if (!isInteractable)
        {
            return;
        }

        Interact(this);
    }


    // метод запуска действия при клике. Переопределяется в потомках
    public virtual void Interact(InteractiveObjectBase objClicked)
    {
        Events.onInteractiveObjectClicked.Invoke(objClicked);
    }


    // метод появления/прятанья индикатора при приближении игрока и запуска событий
    public virtual void SetInteractivityTo(bool state)
    {
        isInteractable = state;

        interactionIndicator.SetActive(state);

        if (state)
        {
            Events.onObjectBecomeInteractive.Invoke(this);
        }
        else
        {
            Events.onObjectBecomeNotInteractive.Invoke(this);
        }
    }


    // сохранить необходимые данные объекта в сериализуемый контейнер.
    public virtual ObjSaveData GetSaveData()
    {
        return new ObjSaveData(transform.position, GetType());
    }
}
