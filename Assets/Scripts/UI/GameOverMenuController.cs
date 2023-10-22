using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuController : MonoBehaviour
{
    private bool _stateChangedThisFrame = false;

    public Canvas GameOverMenuCanvas;
    public string MainMenuSceneName;

    // Start is called before the first frame update
    void Start()
    {
        GameUIController.Instance.StateChanged += OnStateChanged;
    }

    void Update()
    {
        if (_stateChangedThisFrame)
        {
            _stateChangedThisFrame = false;
            return;
        }
    }

    public void MainMenuButtonClicked()
    {
        SceneManager.LoadScene(MainMenuSceneName);
    }

    private void OnStateChanged(UIState previousState, UIState currentState)
    {
        _stateChangedThisFrame = true;
        if (currentState == UIState.GameOver)
        {
            ShowGameOverMenu();
        }
    }

    private void ShowGameOverMenu()
    {
        SetPauseMenuCanvas(true);
    }

    private void SetPauseMenuCanvas(bool enabled)
    {
        GameOverMenuCanvas.enabled = enabled;
    }
}
