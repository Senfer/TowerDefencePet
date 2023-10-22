using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    private bool _stateChangedThisFrame = false;

    public Canvas PauseMenuCanvas;
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

        if(Input.GetKeyDown(KeyCode.Escape) && GameUIController.Instance.CurrentState == UIState.Pause)
        {
            Unpause();
        }
    }

    public void Unpause()
    {
        GameUIController.Instance.Unpause();
    }

    public void MainMenuButtonClicked()
    {
        SceneManager.LoadScene(MainMenuSceneName);
    }

    private void OnStateChanged(UIState previousState, UIState currentState)
    {
        _stateChangedThisFrame = true;
        if (currentState == UIState.Pause)
        {
            ShowPauseMenu();
        }
        if (previousState == UIState.Pause)
        {
            HidePauseMenu();
        }
    }

    private void ShowPauseMenu()
    {
        SetPauseMenuCanvas(true);
    }

    private void HidePauseMenu()
    {
        SetPauseMenuCanvas(false);
    }

    private void SetPauseMenuCanvas(bool enabled)
    {
        PauseMenuCanvas.enabled = enabled;
    }
}
