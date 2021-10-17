using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText, _timerText;
    private static Text ScoreText { get; set; }
    private static Text TimerText { get; set; }

    private void OnEnable()
    {
        ScoreText = _scoreText;
        TimerText = _timerText;
    }

    public static void OnChangedGameTime(int gameTime)
    {
        TimerText.text = $"{gameTime/60}:{gameTime%60}";
    }

    public static void OnChangedScore(int score)
    {
        ScoreText.text = $"Score: {score}";
    }
}
