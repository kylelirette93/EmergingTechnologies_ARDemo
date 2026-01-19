using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject infoPanel;
    public GameObject gameplayPanel;

    public void DisableAllUI()
    {
        infoPanel.SetActive(false);
    }
    public void DisplayInfoUI()
    {
        infoPanel.SetActive(true);
    }

    public void DisplayGameplayUI()
    {
        gameplayPanel.SetActive(true);
    }
}
