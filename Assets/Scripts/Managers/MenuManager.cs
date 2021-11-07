using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Canvas _levelPanel;

    public void LoadScene(int SceneId) => SceneManager.LoadSceneAsync(SceneId);

    public void LevelPanel(bool mode)
    {
        _levelPanel.enabled = mode;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
