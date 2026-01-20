using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameState currentState = GameState.Scanning;
    public static GameManager Instance;
    private UIManager uiManager;
    [SerializeField] private ObjectPlacer catPlacer;
    float timeCounter = 0f;
    float lastTimeChecked;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] SpawnManager spawnManager;

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
                catPlacer.ResetPlacement();
                uiManager.DisplayScanningUI();
                break;
            case GameState.Placement:
                uiManager.DisplayPlacementUI();
                break;
            case GameState.Gameplay:
                Time.timeScale = 1f;
                spawnManager.SetSpawning(true);
                timeCounter = 0f;
                uiManager.DisplayGameplayUI();
                break;
            case GameState.Gameover:
                spawnManager.SetSpawning(false);
                uiManager.DisplayGameoverUI();
                Time.timeScale = 0f;
                break;
        }
    }

    private void Update()
    {
        timeCounter += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timeCounter / 60);
        int seconds = Mathf.FloorToInt(timeCounter % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
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

    public void Restart()
    {
        HandleStateChange(GameState.Gameplay);
    }
}

public enum GameState
{
    Scanning,
    Placement,
    Gameplay,
    Gameover
}
