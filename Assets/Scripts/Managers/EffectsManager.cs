using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    [SerializeField] private GameObject _explosionEffect;

    [SerializeField] private GameObject PotWithoutLid, PotLid;

    private void Start()
    {
        Destroy(PotWithoutLid, 5.1f);
        Destroy(PotLid, 5.1f);
    }

    public void PlayEffect(Transform transform)
    {
        GameObject obj = Instantiate(_explosionEffect, transform.position, Quaternion.identity);
        Destroy(obj, 2);
    }
}
