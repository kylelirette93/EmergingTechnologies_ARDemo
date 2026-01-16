using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameState currentState = GameState.HowToPlace;
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

    public void HandleStateChange(GameState newState)
    {
        EnterState(newState);
    }

    public void EnterState(GameState state)
    {
        currentState = state;
        uiManager.DisableAllUI();
        switch (currentState)
        {
            case GameState.HowToPlace:
                uiManager.DisplayInfo();
                break;
        }
    } 

    public void Confirm()
    {
        uiManager.DisableAllUI();
        catPlacer.SetCanPlace(true);
    }
}

public enum GameState
{
    HowToPlace,
    Gameplay
}
