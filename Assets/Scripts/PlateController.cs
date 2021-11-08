using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlateController : MonoBehaviour
{
    private PlateControls controls;
    private InputAction _movement, _rotation;

    [SerializeField, Range(5, 15)] private float _movementSpeed;
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

    private void OnDisable()
    {
        controls.PlateActions.Disable();
    }

    private IEnumerator Move()
    {
        while (_movement.IsPressed())
        {
            float vertical = controls.PlateActions.Movement.ReadValue<float>();
            transform.position += new Vector3(0, vertical * _movementSpeed, 0) * Time.deltaTime;

            yield return null;
        }

        yield break;
    }

    private IEnumerator Rotate()
    {
        while (_rotation.IsPressed())
        {
            float angle = controls.PlateActions.Rotation.ReadValue<float>();
            transform.Rotate(new Vector3(0, 0, angle * _rotationSpeed) * Time.deltaTime);

            yield return null;
        }

        yield break;
    }
}
