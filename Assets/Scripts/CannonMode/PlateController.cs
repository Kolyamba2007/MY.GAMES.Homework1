using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlateController : MonoBehaviour
{
    private PlateControls controls;
    private InputAction _movement, _rotation;

    [SerializeField] private Transform plateBody;
    [SerializeField] private Transform P0;
    [SerializeField] private Transform P1;
    [SerializeField, Range(0, 1)] private float _t;
    private float t;

    [SerializeField, Range(1, 5)] private float _movementSpeed;
    [SerializeField, Range(50, 150)] private float _rotationSpeed;

    private void Awake()
    {
        controls = new PlateControls();
    }

    private void OnEnable()
    {
        controls.PlateActions.Enable();

        controls.PlateActions.Movement.started += (x) => StartCoroutine(Move());
        controls.PlateActions.Rotation.started += (x) => StartCoroutine(Rotate());

        _movement = controls.PlateActions.Movement;
        _rotation = controls.PlateActions.Rotation;
    }

    private void Start()
    {
        t = _t;
        transform.position = Vector3.Lerp(P0.position, P1.position, t);
    }

    private void OnDisable()
    {
        controls.PlateActions.Disable();
    }

    private IEnumerator Move()
    {
        while (_movement.IsPressed())
        {
            float vertical = controls.PlateActions.Movement.ReadValue<float>();

            t = Mathf.Clamp01(t + vertical * _movementSpeed * Time.deltaTime);
            transform.position = Vector3.Lerp(P0.position, P1.position, t);

            yield return null;
        }

        yield break;
    }

    private IEnumerator Rotate()
    {
        while (_rotation.IsPressed())
        {
            float angle = controls.PlateActions.Rotation.ReadValue<float>();
            plateBody.Rotate(new Vector3(0, 0, angle) * _rotationSpeed * Time.deltaTime);

            yield return null;
        }

        yield break;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 255);
        Gizmos.DrawLine(P0.position, P1.position);
    }
}
