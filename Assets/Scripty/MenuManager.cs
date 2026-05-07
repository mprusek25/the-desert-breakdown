using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("Panely")]
    public GameObject hlavniMenuContent;
    public GameObject settingsPanel;
    public GameObject comingSoonPanel;
    public GameObject savesPanel; // Tady je náš nový hráč

    // Funkce pro tlačítko SINGLEPLAYER
    public void OtevriSingleplayer()
    {
        hlavniMenuContent.SetActive(false);
        savesPanel.SetActive(true);
    }

    // Funkce pro tlačítko SETTINGS
    public void OtevriSettings()
    {
        hlavniMenuContent.SetActive(false);
        settingsPanel.SetActive(true);
    }

    // Funkce pro tlačítko MULTIPLAYER
    public void OtevriComingSoon()
    {
        hlavniMenuContent.SetActive(false);
        comingSoonPanel.SetActive(true);
    }

    // Funkce pro všechna tlačítka ZPĚT
    public void ZpetDoMenu()
    {
        settingsPanel.SetActive(false);
        comingSoonPanel.SetActive(false);
        savesPanel.SetActive(false); // Vypne i sloty
        hlavniMenuContent.SetActive(true);
    }

    public void UkoncitHru()
    {
        Debug.Log("Hra se vypíná...");
        Application.Quit();
    }
}