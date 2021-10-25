using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText, _timerText;
    [SerializeField] private Canvas _pauseMenu;

    [SerializeField] private GameManager _gameManager;

    private void Awake()
    {
        _gameManager.Paused += OpenPauseMenu;
        _gameManager.ChangedScore += OnChangedScore;
        _gameManager.ChangedGameTime += OnChangedGameTime;
    }

    private void OnChangedGameTime(int gameTime)
    {
        _timerText.text = (gameTime % 60) < 10 ? $"{gameTime / 60}:0{gameTime % 60}" : $"{gameTime/60}:{gameTime%60}";
    }

    private void OnChangedScore(int score)
    {
        _scoreText.text = $"Score: {score}";
    }

    private void OpenPauseMenu() => _pauseMenu.enabled = !_pauseMenu.enabled;
}
