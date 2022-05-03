using UnityEngine;

// ������� ����� ��� �������. ��� ����� ������ ������ ���������� �� UI.
// �������� ������� ����� ����������� ������������� ��������.

public class InteractiveObjectBase : MonoBehaviour
{
    // ���������, ������� �� ����� ���������� � ������� ��� ����� ������ � ���� ���������������
    [SerializeField] private GameObject interactionIndicator;
    // �������� ��� UI
    public string description;
    
    [HideInInspector] public bool isInteractable;
    // ������ ������ �� ������, ������� ����� � ���� ������� ���������������
    [HideInInspector] public PlayerController playerNearby;

    public bool IsInteractable { get => isInteractable; set => isInteractable = value; }


    private void Start()
    {
        if (string.IsNullOrEmpty(description))
        {
            description = "<��� �������� ��� ������� " + gameObject.name + ">";
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


    // ����� ������� �������� ��� �����. ���������������� � ��������
    public virtual void Interact(InteractiveObjectBase objClicked)
    {
        Events.onInteractiveObjectClicked.Invoke(objClicked);
    }


    // ����� ���������/�������� ���������� ��� ����������� ������ � ������� �������
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


    // ��������� ����������� ������ ������� � ������������� ���������.
    public virtual ObjSaveData GetSaveData()
    {
        return new ObjSaveData(transform.position, GetType());
    }
}
