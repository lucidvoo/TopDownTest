using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractiveObjectBase : MonoBehaviour
{
    [SerializeField] private GameObject interactionIndicator;
    public string description;
    
    [HideInInspector] public bool isInteractable;
    [HideInInspector] public PlayerController playerNearby;

    private void Start()
    {
        if (string.IsNullOrEmpty(description))
        {
            description = "<��� �������� ��� ������� " + gameObject.name + ">";
        }
    }

    public bool IsInteractable { get => isInteractable; set => isInteractable = value; }

    private void OnMouseDown()
    {
        if (!isInteractable)
        {
            return;
        }

        Interact(this);
    }

    // ����� ������� �������� ��� �����
    public virtual void Interact(InteractiveObjectBase objClicked)
    {
        Events.onInteractiveObjectClicked.Invoke(objClicked);
    }

    // ����� ��������� ���������� ��� ����������� ������
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

    public virtual ObjSaveData GetSaveData()
    {
        return new ObjSaveData(transform.position, GetType());
    }

}
