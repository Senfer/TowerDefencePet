using Assets.Scripts;
using System;

public class GameplayManager : Singleton<GameplayManager>
{
    public int CurrentScore;
    public event Action<int> ScoreChanged;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseScore(int value)
    {
        if (CurrentScore > 0)
        {
            CurrentScore += value;
            ScoreChanged(CurrentScore);
        }

        if (CurrentScore <= 0 && GameUIController.InstanceExists)
        {
            GameUIController.Instance.SetGameOver();           
        }
    }
}
