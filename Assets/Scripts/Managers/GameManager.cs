using System.Collections;
using UnityEngine;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    [Header("Время игры в секундах")]
    [SerializeField, Range(120, 300)]
    private int GameTime;
    [Header("Точка появления персонажей")]
    [SerializeField]
    private Transform SpawnPoint;
    [Header("Персонаж")]
    [SerializeField]
    private GameObject Egg, Sausage, Tomato;
    [Header("Время между появлением персонажей")]
    [SerializeField, Range(1, 5)]
    private float SpawnTime;
    public static int Score { get; set; } = 0;

    void Start()
    {
        StartCoroutine(GameTimer());
        StartCoroutine(Spawn(SpawnTime));
    }

    private IEnumerator GameTimer()
    {
        while (GameTime > 0)
        {
            GameTime--;
            UIManager.OnChangedGameTime(GameTime);
            yield return new WaitForSeconds(1);
        }

        EditorApplication.isPlaying = false;
    }

    private IEnumerator Spawn(float spawnTime)
    {
        while (true)
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    Instantiate(Egg, SpawnPoint.position, Quaternion.identity);
                    break;
                case 1:
                    Instantiate(Tomato, SpawnPoint.position, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(Sausage, SpawnPoint.position, Quaternion.identity);
                    break;
            }
            yield return new WaitForSeconds(spawnTime);
        }
    }

    public static void ChangingScore(int score)
    {
        Score += score;
        if(Score < 0) Score = 0;

        UIManager.OnChangedScore(Score);
    }
}
