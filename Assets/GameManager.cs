using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameState currentState = GameState.HowToPlay;
    public static GameManager Instance;
    private int throws = 3;

    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion
    }

    public void HandleChangeState(GameState newState)
    {
        currentState = newState;
    }

    public void Update()
    {
        switch (currentState)
        {
            case GameState.HowToPlay:
                // TODO: Tutorial will be displayed.
                break;
            case GameState.Playing:
                // TODO: Player will be able to throw 3 darts.
                break;
            case GameState.Scoring:
                // TODO: Get total of 3 darts thrown and display it.
                break;
        }
    }


}

public enum GameState
{
    HowToPlay,
    Playing,
    Scoring
}
