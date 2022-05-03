using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSphere : MonoBehaviour
{
    [SerializeField] InteractiveObjectBase interObjectScript;
    [SerializeField] private string playerTag = "Player";

    private void Start()
    {
        interObjectScript.SetInteractivityTo(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            interObjectScript.SetInteractivityTo(true);
            interObjectScript.playerNearby = other.gameObject.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            interObjectScript.SetInteractivityTo(false);
            interObjectScript.playerNearby = null;
        }
    }
}
