using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject infoPanel;
    public GameObject infoPanel2;

    public void DisableAllUI()
    {
        infoPanel.SetActive(false);
        infoPanel2.SetActive(false);
    }
    public void DisplayInfo()
    {
        infoPanel.SetActive(true);
    }

    public void DisplayInfo2()
    {
        infoPanel2.SetActive(true);
    }
}
