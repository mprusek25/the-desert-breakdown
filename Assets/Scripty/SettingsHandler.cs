using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsHandler : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider volumeSlider;
    public Toggle fullscreenToggle;
    public Toggle vsyncToggle;
    public TMP_Dropdown fpsDropdown;

    [Header("Panels")]
    public GameObject hlavniMenu; // Objekt s hlavními 4 tlačítky

    void Start()
    {
        // Nastavení výchozích hodnot UI
        volumeSlider.value = AudioListener.volume;
        fullscreenToggle.isOn = Screen.fullScreen;
        vsyncToggle.isOn = QualitySettings.vSyncCount > 0;

        SetupFPSDropdown();
    }

    // --- LOGIKA TLAČÍTKA ZPĚT ---
    public void Zpet()
    {
        // Vypne tento panel (Settings) a zapne hlavní menu
        this.gameObject.SetActive(false);
        if (hlavniMenu != null)
        {
            hlavniMenu.SetActive(true);
        }
    }

    // --- OSTATNÍ NASTAVENÍ ---
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetVSync(bool isVsync)
    {
        QualitySettings.vSyncCount = isVsync ? 1 : 0;
    }

    public void SetFPS(int index)
    {
        switch (index)
        {
            case 0: Application.targetFrameRate = 60; break;
            case 1: Application.targetFrameRate = 120; break;
            case 2: Application.targetFrameRate = 144; break;
            case 3: Application.targetFrameRate = 160; break;
            case 4: Application.targetFrameRate = -1; break;
        }
    }

    private void SetupFPSDropdown()
    {
        fpsDropdown.ClearOptions();
        var options = new System.Collections.Generic.List<string> { "60 FPS", "120 FPS", "144 FPS", "160 FPS", "Unlimited" };
        fpsDropdown.AddOptions(options);
        fpsDropdown.RefreshShownValue();
    }
}