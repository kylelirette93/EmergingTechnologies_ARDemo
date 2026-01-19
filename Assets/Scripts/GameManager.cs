using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameState currentState = GameState.Scanning;
    public static GameManager Instance;
    private UIManager uiManager;
    [SerializeField] private CatPlacer catPlacer;

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
        uiManager = GetComponentInChildren<UIManager>();
    }

    private void OnEnable()
    {
        catPlacer.OnCatPlaced += TransitionToGameplay;
    }

    private void OnDisable()
    {
        catPlacer.OnCatPlaced -= TransitionToGameplay;
    }

    public void HandleStateChange(GameState newState)
    {
        EnterState(newState);
    }

    public void EnterState(GameState state)
    {
        currentState = state;
        uiManager.DisplayScanningUI();
        switch (currentState)
        {
            case GameState.Scanning:
                uiManager.DisplayScanningUI();
                break;
            case GameState.Placement:
                uiManager.DisplayPlacementUI();
                break;
            case GameState.Gameplay:
                uiManager.DisplayGameplayUI();
                break;
        }
    } 

    public void Confirm()
    {
        uiManager.DisplayScanningUI();
        catPlacer.SetCanPlace(true);
        HandleStateChange(GameState.Placement);
    }

    public void TransitionToGameplay()
    {
        HandleStateChange(GameState.Gameplay);
    }
}

public enum GameState
{
    Scanning,
    Placement,
    Gameplay
}
