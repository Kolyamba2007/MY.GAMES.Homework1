using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _explosionClip, _clickClip;

    private static AudioSource AudioSource { get; set; }
    public static AudioClip ExplosionClip { get; set; }
    public static AudioClip ClickClip { get; set; }

    private void OnEnable()
    {
        AudioSource = _audioSource;
        ExplosionClip = _explosionClip;
        ClickClip = _clickClip;
    }

    public static void PlaySound(AudioClip clip)
    {
        AudioSource.PlayOneShot(clip);
    }
}
