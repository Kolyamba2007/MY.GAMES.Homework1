using System.Collections;
using System.Collections.Generic;
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
    [SerializeField]
    private GameObject PotWithoutLid, PotLid;

    public static int Score { get; set; } = 0;
    private LinkedList<GameObject> characters = new LinkedList<GameObject>();

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

    private IEnumerator Spawning(float spawnTime)
    {
        while (true)
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    CreateCharacter(Egg);
                    break;
                case 1:
                    CreateCharacter(Tomato);
                    break;
                case 2:
                    CreateCharacter(Sausage);
                    break;
            }
            yield return new WaitForSeconds(spawnTime);
        }
    }

    private IEnumerator Countdown()
    {
        for(int i = 0; i < 5; i++) yield return new WaitForSeconds(1);

        Destroy(PotWithoutLid);
        Destroy(PotLid);

        StartCoroutine(GameTimer());
        StartCoroutine(Spawning(SpawnTime));

        yield break;
    }

    private void CreateCharacter(GameObject character)
    {
        characters.AddLast(Instantiate(character, SpawnPoint.position, Quaternion.identity));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadScene(int SceneId)
    {
        foreach (var character in characters) Destroy(character);

        SceneManager.LoadSceneAsync(SceneId);
    }

    public static void ChangingScore(int score)
    {
        Score += score;
        if(Score < 0) Score = 0;

        UIManager.OnChangedScore(Score);
    }
}
