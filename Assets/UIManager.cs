using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject infoPanel;

    public void DisableAllUI()
    {
        infoPanel.SetActive(false);
    }
    public void DisplayInfo()
    {
        infoPanel.SetActive(true);
    }
}
