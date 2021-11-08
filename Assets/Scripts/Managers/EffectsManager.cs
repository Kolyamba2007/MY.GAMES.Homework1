using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    public enum Effects { Confetti }

    [SerializeField] private GameObject _explosionEffect;
    [SerializeField] private ParticleSystem _particleEffect;

    public void PlayEffect(Vector2 position)
    {
        GameObject obj = Instantiate(_explosionEffect, position, Quaternion.identity);
        Destroy(obj, 2);
    }

    public void PlayEffect(Effects effect)
    {
        switch (effect)
        {
            case Effects.Confetti:
                _particleEffect.Play();
                break;
        }
    }
}
