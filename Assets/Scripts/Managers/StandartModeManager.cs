using System.Collections;
using UnityEngine;

public class StandartModeManager : GameManager
{
    [Header("Точка появления персонажей")]
    [SerializeField] private Transform SpawnPoint;
    [Header("Время между появлением персонажей")]
    [SerializeField, Range(1, 5)] private float _spawnTime;
    [Space(20f)]
    [SerializeField] private GameObject PotWithoutLid;
    [SerializeField] private GameObject PotLid;

    private void OnEnable()
    {
        Destroy(PotWithoutLid, 5.1f);
        Destroy(PotLid, 5.1f);
    }

    private IEnumerator Spawning(float spawnTime)
    {
        while (GameTime > 0)
        {
            CreateCharacter(_characterSet[Random.Range(0, _characterSet.Length)], SpawnPoint.position);

            yield return new WaitForSeconds(spawnTime);
        }

        yield break;
    }

    protected override void OnGameStart() => StartCoroutine(Spawning(_spawnTime));
}
