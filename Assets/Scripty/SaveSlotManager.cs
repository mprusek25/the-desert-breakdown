using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveSlotManager : MonoBehaviour
{
    [System.Serializable]
    public class SlotUI
    {
        [Header("Přiřaď objekty z Hierarchy")]
        public GameObject emptyBtn;      // Tlačítko "Available Slot"
        public GameObject activeGroup;   // Objekt "ActiveSlot" (obsahuje Play/Delete)
    }

    [Header("Konfigurace Slotů")]
    public SlotUI slot1;
    public SlotUI slot2;

    [Header("Názvy Scén")]
    public string introScenaName = "IntroScene";     // Scéna s komiksem
    public string loadingScenaName = "LoadingScene"; // Scéna "Načítání..."
    public string herniScenaName = "SampleScene";   // Samotná hra

    void OnEnable()
    {
        RefreshSlots();
    }

    public void RefreshSlots()
    {
        UpdateUI(1, slot1);
        UpdateUI(2, slot2);
    }

    void UpdateUI(int num, SlotUI ui)
    {
        if (ui.emptyBtn == null || ui.activeGroup == null) return;

        bool isActive = PlayerPrefs.HasKey("Slot" + num + "_Active");
        ui.emptyBtn.SetActive(!isActive);
        ui.activeGroup.SetActive(isActive);
    }

    // NOVÁ HRA -> LOADING -> INTRO
    public void CreateNewGame(int num)
    {
        Debug.Log("Vytvářím novou hru ve slotu: " + num);
        
        PlayerPrefs.SetInt("Slot" + num + "_Active", 1);
        PlayerPrefs.SetInt("CurrentSlot", num);
        
        // Resetujeme stará data pozice
        string prefix = "Slot" + num + "_";
        PlayerPrefs.DeleteKey(prefix + "PosX");
        PlayerPrefs.DeleteKey(prefix + "PosY");
        PlayerPrefs.DeleteKey(prefix + "PosZ");

        // Klíč pro Loading scenerii: Jdi do Intra
        PlayerPrefs.SetString("AfterLoadingGoTo", introScenaName);
        PlayerPrefs.Save();

        SceneManager.LoadScene(loadingScenaName);
    }

    // POKRAČOVAT -> LOADING -> HRA
    public void PlayGame(int num)
    {
        Debug.Log("Pokračuji ve hře ve slotu: " + num);
        PlayerPrefs.SetInt("CurrentSlot", num);
        
        // Klíč pro Loading scenerii: Jdi rovnou do Hry
        PlayerPrefs.SetString("AfterLoadingGoTo", herniScenaName);
        PlayerPrefs.Save();

        SceneManager.LoadScene(loadingScenaName);
    }

    public void DeleteSlot(int num)
    {
        Debug.Log("Mažu slot: " + num);
        string prefix = "Slot" + num + "_";

        PlayerPrefs.DeleteKey("Slot" + num + "_Active");
        PlayerPrefs.DeleteKey(prefix + "PosX");
        PlayerPrefs.DeleteKey(prefix + "PosY");
        PlayerPrefs.DeleteKey(prefix + "PosZ");
        PlayerPrefs.DeleteKey(prefix + "HP"); 
        
        PlayerPrefs.Save();
        RefreshSlots();
    }
}