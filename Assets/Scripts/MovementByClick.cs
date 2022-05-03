using UnityEngine;

// плавное движение игрока щелчками мыши или удерживанием кнопки мыши

[RequireComponent(typeof(Rigidbody))]
public class MovementByClick : MonoBehaviour
{
    [Range(0f, 100f)]
    [SerializeField] private float movementSpeed;
    [SerializeField] private new Camera camera;
    [SerializeField] private string groundTag = "Ground";
    [Tooltip("Расстояние между игроком и его целью\nпри котором он перестает двигаться к ней")]
    [SerializeField] private float targetingInaccuracy = 0.3f;

    private Rigidbody rigidBody;
    private RaycastHit raycastHit;
    // точка, выбираемая щелчком мыши, к которой стремится игрок
    private Vector3 moveTarget;


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        // мы будем использовать квадрат этой переменной в рассчетах
        targetingInaccuracy *= targetingInaccuracy;
        moveTarget = transform.position;
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
        // для оптимизации сравниваем не дистанцию до цели, а квадрат этой дистанции.
        if (distanceToTarget.sqrMagnitude <= targetingInaccuracy)
        {
            return;
        }

        rigidBody.AddForce(distanceToTarget * movementSpeed * Time.fixedDeltaTime * 200f);
    }
}
