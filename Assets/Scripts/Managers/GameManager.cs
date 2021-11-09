using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] protected AudioManager _audioManager;
    [SerializeField] protected EffectsManager _effectsManager;

    [Header("Время игры в секундах")]
    [SerializeField, Range(120, 300)] private int _gameTime;
    [Header("Персонаж")]
    [SerializeField] protected BaseUnit[] _characterSet;
    [Header("Очки, которые надо набрать для получения каждой звезды")]
    [SerializeField, Range(100, 500)] private int[] _starScore;

    protected int GameTime;
    private int Score { get; set; } = 0;
    private LinkedList<BaseUnit> _characters = new LinkedList<BaseUnit>();

    public event Action Paused;
    public event Action<int> ChangedScore;
    public event Action<int> ChangedGameTime;
    public event Action<int> EndGame;

    protected event Action StartGame;

    void Start()
    {
        GameTime = _gameTime;
        ChangedGameTime?.Invoke(GameTime);
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

        FinishGame();

        yield break;
    }

    private void FinishGame()
    {
        Time.timeScale = 0;
        EndGame?.Invoke(StarCount());
        _effectsManager.PlayEffect(EffectsManager.Effects.Confetti);
    }

    private int StarCount()
    {
        int count = 0;

        foreach(int score in _starScore)
        {
            if (Score >= score) count++;
        }

        return count;
    }

    private IEnumerator Countdown()
    {
        for(int i = 0; i < 5; i++) yield return new WaitForSeconds(1);

        StartCoroutine(GameTimer());
        StartGame?.Invoke();

        yield break;
    }

    protected BaseUnit CreateCharacter(BaseUnit character, Vector3 spawnPosition)
    {
        BaseUnit unit = Instantiate(character, spawnPosition, Quaternion.identity);

        unit.Exploded += OnUnitExploded;
        unit.Interacted += OnUnitInteracted;

        _characters.AddLast(unit);

        return unit;
    }

    private void OnUnitExploded(BaseUnit unit, int Score)
    {
        ChangeScore(Score);

        _audioManager.PlaySound(AudioManager.UnitAudio.Explosion);
        _effectsManager.PlayEffect(unit.gameObject.transform.position);

        RemoveCharacter(unit);
    }

    private void OnUnitInteracted(BaseUnit unit, int Score)
    {
        ChangeScore(Score);

        _audioManager.PlaySound(AudioManager.UnitAudio.Click);

        RemoveCharacter(unit);
    }

    private void RemoveCharacter(BaseUnit character)
    {
        _characters.Remove(character);
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
        foreach (var character in _characters)
        {
            character.isInteracted = true;
            Destroy(character);
        }

        SceneManager.LoadSceneAsync(SceneId);
        Time.timeScale = 1;
    }
}
