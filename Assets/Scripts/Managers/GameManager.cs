using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private EffectsManager _effectsManager;

    [Header("Время игры в секундах")]
    [SerializeField, Range(120, 300)] private int GameTime;
    [Header("Точка появления персонажей")]
    [SerializeField] private Transform SpawnPoint;
    [Header("Персонаж")]
    [SerializeField] private BaseUnit Egg, Sausage, Tomato;
    [Header("Время между появлением персонажей")]
    [SerializeField, Range(1, 5)] private float SpawnTime;
    [Header("Очки, которые надо набрать для получения каждой звезды")]
    [SerializeField, Range(100, 500)] private int[] StarScore;

    private int Score { get; set; } = 0;
    private LinkedList<BaseUnit> characters = new LinkedList<BaseUnit>();

    public event Action Paused;
    public event Action<int> ChangedScore, ChangedGameTime, EndGame;

    void Start()
    {
        ChangedGameTime(GameTime);
        StartCoroutine(Countdown());
    }

    private IEnumerator GameTimer()
    {
        while (GameTime > 0)
        {
            GameTime--;
            ChangedGameTime?.Invoke(GameTime);
            yield return new WaitForSeconds(1);
        }

        Time.timeScale = 0;
        EndGame?.Invoke(StarCount());
    }

    private int StarCount()
    {
        int count = 0;

        foreach(int _score in StarScore)
        {
            if (Score >= _score) count++;
        }

        return count;
    }

    private IEnumerator Spawning(float spawnTime)
    {
        while (true)
        {
            switch (UnityEngine.Random.Range(0, 3))
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

        StartCoroutine(GameTimer());
        StartCoroutine(Spawning(SpawnTime));

        yield break;
    }

    private void CreateCharacter(BaseUnit character)
    {
        BaseUnit unit = Instantiate(character, SpawnPoint.position, Quaternion.identity);

        unit.Exploded += OnUnitExploded;
        unit.Clicked += OnUnitClicked;

        characters.AddLast(unit);
    }

    private void OnUnitExploded(BaseUnit unit, int Score)
    {
        ChangeScore(Score);

        _audioManager.PlaySound(AudioManager.UnitAudio.Explosion);
        _effectsManager.PlayEffect(unit.gameObject.transform);

        RemoveCharacter(unit);
    }

    private void OnUnitClicked(BaseUnit unit, int Score)
    {
        ChangeScore(Score);

        _audioManager.PlaySound(AudioManager.UnitAudio.Click);

        RemoveCharacter(unit);
    }

    private void RemoveCharacter(BaseUnit character)
    {
        characters.Remove(character);
    }

    private void ChangeScore(int score)
    {
        Score += score;
        if(Score < 0) Score = 0;

        ChangedScore?.Invoke(Score);
    }

    public void PauseGame()
    {
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
        Paused?.Invoke();
    }

    public void LoadScene(int SceneId)
    {
        foreach (var character in characters)
        {
            character.isClicked = true;
            Destroy(character);
        }

        SceneManager.LoadSceneAsync(SceneId);
        Time.timeScale = 1;
    }
}
