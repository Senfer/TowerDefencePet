using Assets.Scripts;
using Assets.Scripts.UI;
using System;
using UnityEngine;

public class GameUIController : Singleton<GameUIController>
{
    public UIState CurrentState;

    public event Action<UIState, UIState> StateChanged = delegate { };

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (CurrentState == UIState.Normal)
            {
                Pause();
            }            
        }
    }

    public void Pause()
    {
        SetState(UIState.Pause);
    }

    public void Unpause()
    {
        SetState(UIState.Normal);
    }

    public void SetGameOver()
    {
        SetState(UIState.GameOver);
    }

    private void SetState(UIState state)
    {
        if (CurrentState == state)
        {
            return;
        }

        var previousState = CurrentState;
        CurrentState = state;
        StateChanged(previousState, state);
    }
}
