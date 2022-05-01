using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovementByClick : MonoBehaviour
{
    [Range(0f, 100f)]
    [SerializeField] private float movementSpeed;
    [SerializeField] private new Camera camera;
    [SerializeField] private string groundTag = "Ground";
    [Tooltip("–ассто€ние между игроком и его целью\nпри котором он перестает двигатьс€ к ней")]
    [SerializeField] private float targetingInaccuracy = 0.1f;

    private Rigidbody rigidBody;
    private RaycastHit raycastHit;
    private Vector3 moveTarget;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        targetingInaccuracy *= targetingInaccuracy;
    }

    private void Update()
    {
        MouseClickHandling();
    }

    private void MouseClickHandling()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.CompareTag(groundTag))
                {
                    moveTarget = raycastHit.point;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        Vector3 distanceToTarget = new Vector3(moveTarget.x - transform.position.x, 0, moveTarget.z - transform.position.z);
        // дл€ оптимизации сравниваем не дистанцию до цели, а квадрат этой дистанции.
        Debug.Log(distanceToTarget);
        if (distanceToTarget.sqrMagnitude <= targetingInaccuracy)
        {
            Debug.Log("Exit moving");
            return;
        }

        rigidBody.AddForce(distanceToTarget * movementSpeed * Time.fixedDeltaTime * 200f);
        //rigidBody.velocity = distanceToTarget.normalized * movementSpeed * Time.fixedDeltaTime * 10f;
    }
}
