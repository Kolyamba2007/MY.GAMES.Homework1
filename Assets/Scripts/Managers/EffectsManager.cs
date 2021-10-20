using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    [SerializeField] private GameObject _explosionEffect;

    private static GameObject ExplosionEffect { get; set; }

    private void OnEnable()
    {
        ExplosionEffect = _explosionEffect;
    }

    public static void Explosion(Transform transform)
    {
        GameObject obj = Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        Destroy(obj, 2);
    }
}
