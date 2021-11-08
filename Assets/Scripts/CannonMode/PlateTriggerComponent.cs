using UnityEngine;

public class PlateTriggerComponent : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider) => collider.GetComponent<CannonUnit>().HandleCatch();
}
