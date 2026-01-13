using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameState currentState = GameState.HowToPlace;
    public static GameManager Instance;
    private UIManager uiManager;
    [SerializeField] private PlaceDartboard dartboardPlacer;
    [SerializeField] private GameObject dartPrefab;
    private int dartCount = 0;
    private int thrownCount = 3;

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
        switch (currentState)
        {
            case GameState.HowToPlace:
                uiManager.DisableAllUI();
                uiManager.DisplayInfo();
                break;
            case GameState.HowToThrow:
                uiManager.DisableAllUI();
                uiManager.DisplayInfo2();
                break;
            case GameState.Playing:
                Instantiate(dartPrefab, dartPrefab.transform.position, Quaternion.identity);
                break;
            case GameState.Scoring:
                break;
        }
    } 

    public void Update()
    {
        switch (currentState)
        {
            case GameState.HowToPlace:
                if (dartboardPlacer.HasPlaced)
                {
                    HandleStateChange(GameState.HowToThrow);
                }
                break;
            case GameState.HowToThrow:
                // TODO: Player will be able to throw 3 darts.
                break;
            case GameState.Playing:
                if (Input.GetMouseButtonDown(0))
                {
                    if (dartCount < thrownCount)
                    {
                        Instantiate(dartPrefab, dartPrefab.transform.position, Quaternion.identity);
                    }
                    dartCount++;
                }
                break;
            case GameState.Scoring:
                // TODO: Get total of 3 darts thrown and display it.
                break;
        }
    }

    public void Confirm()
    {
        uiManager.DisableAllUI();
        dartboardPlacer.canPlace = true;
    }

    public void PlayGame()
    {
        uiManager.DisableAllUI();
        EnterState(GameState.Playing);
    }
}

public enum GameState
{
    HowToPlace,
    HowToThrow,
    Playing,
    Scoring
}
