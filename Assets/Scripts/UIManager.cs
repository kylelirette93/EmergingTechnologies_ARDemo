using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject scanningPanel;
    public GameObject placementPanel;
    public GameObject gameplayPanel;

    public void DisableAllUI()
    {
        scanningPanel.SetActive(false);
        placementPanel.SetActive(false);
        gameplayPanel.SetActive(false);
    }
    public void DisplayScanningUI()
    {
        DisableAllUI();
        scanningPanel.SetActive(true);
    }
    public void DisplayPlacementUI()
    {
        DisableAllUI();
        placementPanel.SetActive(true);
    }

    public void DisplayGameplayUI()
    {
        DisableAllUI();
        gameplayPanel.SetActive(true);
    }
}
