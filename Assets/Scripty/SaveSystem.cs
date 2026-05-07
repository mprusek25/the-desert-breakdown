using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    // Tady definuj, co všechno chceš ukládat
    public Transform playerTransform;

    public void SavePlayerData()
    {
        int slot = PlayerPrefs.GetInt("CurrentSlot", 1); // Zjistíme, v jakém slotu hrajeme
        string prefix = "Slot" + slot + "_";

        // Uložení pozice (X, Y, Z)
        PlayerPrefs.SetFloat(prefix + "PosX", playerTransform.position.x);
        PlayerPrefs.SetFloat(prefix + "PosY", playerTransform.position.y);
        PlayerPrefs.SetFloat(prefix + "PosZ", playerTransform.position.z);

        // Tady můžeš přidat další věci, např. HP:
        // PlayerPrefs.SetInt(prefix + "Health", currentHealth);

        PlayerPrefs.Save(); // Tohle to fyzicky zapíše na disk
        Debug.Log("Data uložena do " + prefix);
    }

    public void LoadPlayerData()
    {
        int slot = PlayerPrefs.GetInt("CurrentSlot", 1);
        string prefix = "Slot" + slot + "_";

        // Kontrola, jestli už vůbec nějaká uložená pozice existuje
        if (PlayerPrefs.HasKey(prefix + "PosX"))
        {
            float x = PlayerPrefs.GetFloat(prefix + "PosX");
            float y = PlayerPrefs.GetFloat(prefix + "PosY");
            float z = PlayerPrefs.GetFloat(prefix + "PosZ");

            // Teleportujeme postavu na uložené souřadnice
            playerTransform.position = new Vector3(x, y, z);
            Debug.Log("Data načtena ze " + prefix);
        }
    }

    void Start()
    {
        // Automaticky načteme pozici při startu scény
        LoadPlayerData();
    }
}