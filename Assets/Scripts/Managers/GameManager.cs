using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        Time.timeScale = 1;
        UIManager.OnChangedGameTime(GameTime);
        StartCoroutine(Countdown());
    }

    private IEnumerator GameTimer()
    {
        while (GameTime > 0)
        {
            GameTime--;
            UIManager.OnChangedGameTime(GameTime);
            yield return new WaitForSeconds(1);
        }

        Time.timeScale = 0;
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

    private IEnumerator Countdown()
    {
        for(int i = 0; i < 5; i++) yield return new WaitForSeconds(1);

        StartCoroutine(GameTimer());
        StartCoroutine(Spawn(SpawnTime));

        yield break;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadScene(int SceneId)
    {
        SceneManager.LoadSceneAsync(SceneId);
    }

    public static void ChangingScore(int score)
    {
        Score += score;
        if(Score < 0) Score = 0;

        UIManager.OnChangedScore(Score);
    }
}
