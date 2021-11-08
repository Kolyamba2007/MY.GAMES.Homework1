using UnityEngine;

public class CannonModeManager : GameManager
{
    [Header("Пушки, находящиеся на сцене")]
    [SerializeField] private CannonComponent[] _cannons;

    private void Awake()
    {
        for(int i = 0; i < _cannons.Length; i++)
            _cannons[i].Fire += OnFire;
    }

    private void OnFire(Vector2 spawnPosition, Vector2 force)
    {
        CreateCharacter(_characterSet[Random.Range(0, _characterSet.Length)], spawnPosition).gameObject.GetComponent<Rigidbody2D>().AddForce(force);

        _audioManager.PlaySound(AudioManager.UnitAudio.Explosion);
        _effectsManager.PlayEffect(spawnPosition);
    }

    protected override void StartGame()
    {
        for (int i = 0; i < _cannons.Length; i++)
            _cannons[i].StartCoroutine("FireTimer");
    }
}
