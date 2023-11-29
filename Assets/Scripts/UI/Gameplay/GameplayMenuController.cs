using Assets.Scripts.UI;
using UnityEngine;

public class GameplayMenuController : MonoBehaviour
{ 
    private bool _stateChangedThisFrame = false;

    public Canvas GameplayMenuCanvas;

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
    public void StartButtonClicked()
    {
        GameplayManager.Instance.StartWaves();
    }

    private void OnStateChanged(UIState previousState, UIState currentState)
    {
        _stateChangedThisFrame = true;
        if (currentState == UIState.Normal)
        {
            ShowGameplayMenu();
        }
        if (previousState == UIState.Normal)
        {
            HideGameplayMenu();
        }
    }
    

    private void ShowGameplayMenu()
    {
        SetMenuCanvas(true);
    }

    private void HideGameplayMenu()
    {
        SetMenuCanvas(false);
    }

    private void SetMenuCanvas(bool enabled)
    {
        GameplayMenuCanvas.enabled = enabled;
    }
}
