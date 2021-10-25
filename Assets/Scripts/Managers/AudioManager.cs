using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum UnitAudio { Click, Explosion }

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _explosionClip, _clickClip;

    public void PlaySound(UnitAudio audio)
    {
        switch (audio)
        {
            case UnitAudio.Click:
                _audioSource.PlayOneShot(_clickClip);
                break;
            case UnitAudio.Explosion:
                _audioSource.PlayOneShot(_explosionClip);
                break;
        }
    }
}
