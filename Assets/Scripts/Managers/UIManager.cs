using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText, _timerText;
    [SerializeField] private Canvas _pauseMenu, _resultsTable;
    [SerializeField] private Button _pauseButton;

    [SerializeField] private GameManager _gameManager;

    [SerializeField] private Sprite _starSprite;
    [SerializeField] private Image[] _starImages;

    private void OnEnable()
    {
        _gameManager.Paused += PauseMenu;
        _gameManager.ChangedScore += OnChangedScore;
        _gameManager.ChangedGameTime += OnChangedGameTime;
        _gameManager.EndGame += OpenResultsTable;
    }

    private void OnChangedGameTime(int gameTime)
    {
        _timerText.text = (gameTime % 60) < 10 ? $"{gameTime / 60}:0{gameTime % 60}" : $"{gameTime/60}:{gameTime%60}";
    }

    private void OnChangedScore(int score)
    {
        _scoreText.text = $"Score: {score}";
    }

    private void PauseMenu() => _pauseMenu.enabled = !_pauseMenu.enabled;

    private void OpenResultsTable(int starCount)
    {
        _pauseButton.enabled = false;

        for (int i = 0; i < starCount; i++)
        {
            _starImages[i].sprite = _starSprite;
            _starImages[i].color = new Color(255, 255, 255, 255);
        }

        _resultsTable.enabled = true;
    }
}
